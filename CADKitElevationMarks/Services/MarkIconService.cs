using Autofac;
using CADKit;
using CADKit.Contracts;
using CADKit.Services;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Contracts.Services;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CADKitElevationMarks.Services
{
    public class MarkIconService : IMarkIconService
    {
        public Bitmap DefaultIcon
        {
            get
            {
                switch (InterfaceSchemeService.ColorScheme)
                {
                    case InterfaceScheme.light:
                        return Properties.Resources.question;
                    case InterfaceScheme.dark:
                        return Properties.Resources.question_dark;
                    default:
                        return new Bitmap(32, 32);
                }
            }
        }

        private Dictionary<DrawingStandards, Dictionary<MarkTypes, Dictionary<IconSize, Bitmap>>> data;

        public MarkIconService()
        {
            data = new Dictionary<DrawingStandards, Dictionary<MarkTypes, Dictionary<IconSize, Bitmap>>>();

            var types = DI.Container.ComponentRegistry.Registrations
                .Where(r => typeof(MarkIconDrawingStandardService).IsAssignableFrom(r.Activator.LimitType))
                .Select(r => r.Activator.LimitType);

            using(var scope = DI.Container.BeginLifetimeScope())
            {
                IEnumerable<MarkIconDrawingStandardService> lst = types
                    .Select(t => scope.Resolve(t) as MarkIconDrawingStandardService);
                foreach (var obj in lst)
                {
                    data.Add(obj.Standard, obj.GetIcons());
                }
            }
        }

        public Bitmap GetIcon(DrawingStandards standard, MarkTypes type)
        {
            return GetIcon(standard, type, IconSize.medium);
        }

        public Bitmap GetIcon(DrawingStandards _standard, MarkTypes _type, IconSize _size)
        {
            Dictionary<MarkTypes, Dictionary<IconSize, Bitmap>> key1;
            if(data.TryGetValue(_standard,out key1))
            {
                Dictionary<IconSize, Bitmap> key2;
                if (key1.TryGetValue(_type, out key2))
                {
                    return key2[_size];
                }
            }

            return DefaultIcon;
        }
    }
}
