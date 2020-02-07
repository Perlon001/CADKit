using CADKit.Contracts;
using CADKit.Services;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Contracts.Services;
using System.Collections.Generic;
using System.Drawing;

namespace CADKitElevationMarks.Services
{
    public class MarkIconService : IMarkIconService
    {
        private readonly IInterfaceSchemeService colorSchemeService;
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

        public MarkIconService(IInterfaceSchemeService _service)
        {
            data = new Dictionary<DrawingStandards, Dictionary<MarkTypes, Dictionary<IconSize, Bitmap>>>();
            colorSchemeService = _service;

            MarkIconServicePNB01025 servicePNB01025 = new MarkIconServicePNB01025(_service);
            data.Add(DrawingStandards.PNB01025, servicePNB01025.GetIcons());

            MarkIconServiceStd01 serviceStd01 = new MarkIconServiceStd01(_service);
            data.Add(DrawingStandards.Std01, serviceStd01.GetIcons());
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
