using CADKit.Models;
using CADKit.Utils;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.DTO;
using CADKitElevationMarks.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;

namespace CADKitElevationMarks.Services
{
    public class MarkTypeService : IMarkTypeService
    {
        public struct markItem
        {
            public int id;
            public string name;
            public DrawingStandards standard;
            public Type markClass;
            public Bitmap picture16;
            public Bitmap picture32;
        }

        private IList<markItem> markCollection;

        public MarkTypeService()
        {
            int i = 0;
            markCollection = new List<markItem>();
            markCollection.Add(new markItem()
            {
                id = i++,
                name = "Kota wysokościowa",
                standard = DrawingStandards.PNB01025,
                markClass = typeof(ElevationMarkPNB01025),
                picture16 = new Bitmap(16, 16),
                picture32 = new Bitmap(32, 32)
            });
            markCollection.Add(new markItem()
            {
                id = i++,
                name = "Rzędna obszaru",
                standard = DrawingStandards.PNB01025,
                markClass = typeof(PlaneElevationMarkPNB01025),
                picture16 = new Bitmap(16, 16),
                picture32 = new Bitmap(32, 32)
            });
            markCollection.Add(new markItem()
            {
                id = i++,
                name = "Kota wysokościowa wykończenia",
                standard = DrawingStandards.CADKit,
                markClass = typeof(FinishElevationMarkCADKit),
                picture16 = new Bitmap(16, 16),
                picture32 = new Bitmap(32, 32)
            });
            markCollection.Add(new markItem()
            {
                id = i++,
                name = "Kota wysokościowa konstrukcji",
                standard = DrawingStandards.CADKit,
                markClass = typeof(ConstructionElevationMarkCADKit),
                picture16 = new Bitmap(16, 16),
                picture32 = new Bitmap(32, 32)
            });
            markCollection.Add(new markItem()
            {
                id = i++,
                name = "Rzędna obszaru",
                standard = DrawingStandards.CADKit,
                markClass = typeof(PlaneElevationMarkCADKit),
                picture16 = new Bitmap(16, 16),
                picture32 = new Bitmap(32, 32)
            });
        }

        public IList<MarkButtonDTO> GetMarks()
        {
            return markCollection
                .Select(y => { return new MarkButtonDTO() { id = y.id, name = y.name, picture = y.picture32 }; })
                .ToList();
        }

        public IList<MarkButtonDTO> GetMarks(DrawingStandards standard)
        {
            return markCollection
                .Where(x => x.standard == standard)
                .Select(y => { return new MarkButtonDTO() { id = y.id, name = y.name, picture = y.picture32 }; })
                .ToList();
        }

        public IList<MarkButtonDTO> GetMarks(DrawingStandards standard, MarkTypes[] types)
        {
            throw new NotImplementedException();
        }

        public Type GetMarkType(int markNumber)
        {
            return markCollection
                .First(x => x.id == markNumber).markClass;
        }

        public IList<MarkButtonDTO> GetMarks(MarkTypes[] types)
        {
            throw new NotImplementedException();
        }
    }
}
