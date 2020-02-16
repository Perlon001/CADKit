using Autofac;
using CADKit;
using CADKit.Contracts;
using CADKit.Models;
using CADKit.Proxy;
using CADKit.Services;
using CADKit.UI;
using CADKit.Utils;
using CADKitElevationMarks.Contract.Services;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Contracts.Presenters;
using CADKitElevationMarks.Contracts.Views;
using CADKitElevationMarks.Events;
using System.Drawing;
using CADKit.Internal;
using System;
using CADKitElevationMarks.Models;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;

#if ZwCAD
using ZwSoft.ZwCAD.ApplicationServices;
using CADGeometry = ZwSoft.ZwCAD.Geometry;
#endif

#if AutoCAD
using Autodesk.AutoCAD.ApplicationServices;
using CADGeometry = Autodesk.AutoCAD.Geometry;
#endif

namespace CADKitElevationMarks.Presenters
{
    public class ElevationMarksPresenter : Presenter<IElevationMarksView>, IElevationMarksPresenter
    {
        private readonly IMarkService markService;
        private bool isMarkCreateRunning = false;
        private int markID;

        public ElevationMarksPresenter(IElevationMarksView _view, IMarkService _markService)
        {
            View = _view;
            View.Presenter = this;
            markService = _markService;
        }

        public void CreateMark(object sender, BeginMarkCreateEventArgs args)
        {
            markID = args.ID;
            var cmdActive = Convert.ToInt32(CADProxy.GetSystemVariable("CMDACTIVE"));
            if (cmdActive > 0)
            {
                isMarkCreateRunning = true;
                CADProxy.Document.CommandCancelled += CommandCancelled;
                CADProxy.CancelRunningCommand();
            }
            else
            {
                Application.MainWindow.Focus();
                CreateMark();
            }
        }

        public Bitmap GetOptionIcon()
        {
            switch (InterfaceSchemeService.ColorScheme)
            {
                case InterfaceScheme.dark:
                    return Properties.Resources.options_dark;
                default:
                    return Properties.Resources.options;
            }
        }

        public override void OnViewLoaded()
        {
            base.OnViewLoaded();
            View.RegisterHandlers();
        }

        public void FillButtons()
        {
            using (var scope = DI.Container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IMarkService>();
                View.BindMarkButtons(service.GetMarks());
            }
        }

        private void CommandCancelled(object sender, CommandEventArgs e)
        {
            if (isMarkCreateRunning)
            {
                isMarkCreateRunning = false;
                CADProxy.Document.CommandCancelled -= CommandCancelled;
                Application.MainWindow.Focus();
                CreateMark();
            }
        }

        private void CreateMark()
        {
            using (CADProxy.Document.LockDocument())
            {
                using (var scope = DI.Container.BeginLifetimeScope())
                {
                    var markDTO = markService.GetMark(markID);
                    if (scope.IsRegistered(markDTO.markClass))
                    {
                        var mark = scope.Resolve(markDTO.markClass) as Mark;
                        try
                        {
                            mark.Build();
                            var entitiesSet = new EntitiesSetBuilder<MarkEntitiesSet>(mark.Entities)
                                .AddConverter(typeof(AttributeToDBTextConverter))
                                .SetBasePoint(mark.BasePoint)
                                .SetJig(markDTO.markJig)
                                .Build();
                            switch (View.SetType)
                            {
                                case OutputSet.group:
                                    entitiesSet.ToGroup();
                                    break;
                                case OutputSet.block:
                                    entitiesSet.SetAttributeHandler += mark.SetAttributeValue;
                                    var blockReference = entitiesSet.ToBlockReference("ElevMark" + markDTO.type.ToString() + markDTO.standard.ToString() + mark.Index);
                                    entitiesSet.SetAttributeHandler -= mark.SetAttributeValue;
                                    break;
                            }
                            Utils.FlushGraphics();
                        }
                        catch (OperationCanceledException) { }
                    }
                    else
                    {
                        throw new Exception("Brak definicji wybranej koty wysokościowej.");
                    }
                }
            }
        }
    }
}
