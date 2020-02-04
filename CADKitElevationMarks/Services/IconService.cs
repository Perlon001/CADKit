using CADKit.Contracts;
using CADKit.Models;
using CADKitBasic.Models;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.Models;
using System.Drawing;

namespace CADKitElevationMarks.Services
{
    public abstract class IconService : IIconService
    {
        protected readonly IInterfaceSchemeService colorSchemeService;
        protected Bitmap DefaultIcon 
        { 
            get 
            {
                switch (colorSchemeService.GetScheme())
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

        public IconService(IInterfaceSchemeService _service)
        {
            colorSchemeService = _service;
        }

        public Bitmap GetIcon(MarkTypes _type)
        {
            return GetIcon(_type, IconSize.small);
        }

        public Bitmap GetIcon(MarkTypes _type, IconSize _size)
        {
            switch (colorSchemeService.GetScheme())
            {
                case InterfaceScheme.light:
                    return GetIconForLightScheme(_type, _size);
                case InterfaceScheme.dark:
                    return GetIconForDarkScheme(_type, _size);
            }

            return DefaultIcon;
        }

        internal abstract Bitmap GetIconForLightScheme(MarkTypes type, IconSize size);
        internal abstract Bitmap GetIconForDarkScheme(MarkTypes type, IconSize size);
    }
}
