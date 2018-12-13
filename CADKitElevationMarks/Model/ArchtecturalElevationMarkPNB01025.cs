﻿using CADKitCore.Util;
using CADKitElevationMarks.Contract;
using System;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;

namespace CADKitElevationMarks.Model
{
    public class ArchtecturalElevationMarkPNB01025 : BaseElevationMark
    {
        private Polyline[] plines = new Polyline[2] { new Polyline(), new Polyline() };
        private bool IsZero = false;

        public ArchtecturalElevationMarkPNB01025(IElevationMarkConfig config) : base(config)
        {
        }

        protected override void Draw()
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acDatabase = acDoc.Database;
            using (Transaction transaction = acDoc.TransactionManager.StartTransaction())
            {
                DBDictionary groupDictionary = (DBDictionary)transaction.GetObject(acDatabase.GroupDictionaryId, OpenMode.ForWrite);
                Group group = new Group();
                groupDictionary.SetAt("*", group);

                BlockTableRecord record = Extensions.GetBlockTableRecord(transaction, OpenMode.ForWrite);
                TextStyleTableRecord textStyle = ((TextStyleTableRecord)transaction.GetObject(acDoc.Database.Textstyle, OpenMode.ForRead));

                foreach (var item in this.texts)
                {
                    item.SetDatabaseDefaults();
                    item.VerticalMode = TextVerticalMode.TextVerticalMid;
                    item.ColorIndex = 7;
                    item.TextStyle = acDoc.Database.Textstyle;
                    item.WidthFactor = textStyle.XScale;
                    item.Height = 200;// textStyle.TextSize * scaleFactor;
                }

                texts[0].HorizontalMode = TextHorizontalMode.TextRight;
                texts[1].HorizontalMode = TextHorizontalMode.TextLeft;

                var textArea = EntityInfo.GetTextArea(texts[1]);

                texts[0].AlignmentPoint = new Point3d(point.X + 1 * scaleFactor, point.Y + 4.5 * scaleFactor, point.Z);
                texts[1].AlignmentPoint = new Point3d(point.X + 1.5 * scaleFactor , point.Y + 4.5 * scaleFactor, point.Z);

                plines[0].AddVertexAt(0, new Point2d(point.X, point.Y), 0, 0, 0);
                plines[0].AddVertexAt(0, new Point2d(point.X, point.Y + 3 * scaleFactor), 0, 0, 0);
                plines[0].AddVertexAt(0, new Point2d(point.X + (textArea[1].X - textArea[0].X + 1.5 * scaleFactor), point.Y + 3 * scaleFactor), 0, 0, 0);

                plines[1].AddVertexAt(0, new Point2d(point.X - 1 * scaleFactor, point.Y + 1 * scaleFactor), 0, 0, 0);
                plines[1].AddVertexAt(0, new Point2d(point.X, point.Y), 0, 0, 0);
                plines[1].AddVertexAt(0, new Point2d(point.X + 1 * scaleFactor, point.Y + 1 * scaleFactor), 0, 0, 0);

                Polyline boundary = new Polyline();
                boundary.AddVertexAt(0, new Point2d(point.X, point.Y), 0, 0, 0);
                boundary.AddVertexAt(0, new Point2d(point.X + 1 * scaleFactor, point.Y + 1 * scaleFactor), 0, 0, 0);
                boundary.AddVertexAt(0, new Point2d(point.X, point.Y + 1 * scaleFactor), 0, 0, 0);
                boundary.Closed = true;

                if (Math.Round(Math.Abs(point.Y) * GetElevationFactor(), 3) == 0)
                {
                    IsZero = true;
                    plines[1].Closed = true;
                }

                if (point.Y > directionPoint.Y)
                {
                    plines[0].TransformBy(Matrix3d.Mirroring(new Line3d(point, new Vector3d(1, 0, 0))));
                    plines[1].TransformBy(Matrix3d.Mirroring(new Line3d(point, new Vector3d(1, 0, 0))));
                    boundary.TransformBy(Matrix3d.Mirroring(new Line3d(point, new Vector3d(1, 0, 0))));
                    texts[0].AlignmentPoint = new Point3d(texts[0].AlignmentPoint.X, point.Y - 4.5 * scaleFactor, point.Z);
                    texts[1].AlignmentPoint = new Point3d(texts[0].AlignmentPoint.X, point.Y - 4.5 * scaleFactor, point.Z);
                }

                if(point.X > directionPoint.X)
                {
                    plines[0].TransformBy(Matrix3d.Mirroring(new Line3d(point, new Vector3d(0, 1, 0))));
                    plines[1].TransformBy(Matrix3d.Mirroring(new Line3d(point, new Vector3d(0, 1, 0))));
                    texts[0].AlignmentPoint = new Point3d(point.X + 1 * scaleFactor - (textArea[1].X - textArea[0].X + 1.5 * scaleFactor), texts[0].AlignmentPoint.Y, point.Z);
                    texts[1].AlignmentPoint = new Point3d(point.X + 1.5 * scaleFactor - (textArea[1].X - textArea[0].X + 1.5 * scaleFactor), texts[1].AlignmentPoint.Y, point.Z);
                }

                if(IsZero)
                {
                    boundary.Store(transaction, record, group, transformMatrix);
                    ObjectIdCollection collection = new ObjectIdCollection();
                    collection.Add(boundary.ObjectId);
                    using (Hatch hatch = new Hatch())
                    {
                        hatch.SetDatabaseDefaults();
                        hatch.Store(transaction, record, group, transformMatrix);
                        hatch.SetHatchPattern(HatchPatternType.PreDefined, "SOLID");
                        hatch.Associative = false;
                        hatch.AppendLoop(HatchLoopTypes.Default, collection);
                        hatch.EvaluateHatch(true);
                    }
                    boundary.Erase();
                }

                foreach (var item in texts)
                {
                    item.Store(transaction, record, group, transformMatrix);
                }

                plines[0].Store(transaction, record, group, transformMatrix);
                plines[1].Store(transaction, record, group, transformMatrix);

                transaction.AddNewlyCreatedDBObject(group, true);
                acDoc.Editor.Regen();

                transaction.Commit();
            }
        }
    }
}