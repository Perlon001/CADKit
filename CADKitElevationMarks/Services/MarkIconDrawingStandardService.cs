using CADKit.Contracts;
using CADKitElevationMarks.Contracts;
using System.Collections.Generic;
using System.Drawing;

namespace CADKitElevationMarks.Services
{
    public abstract class MarkIconDrawingStandardService
    {
        protected readonly IInterfaceSchemeService service;

        public MarkIconDrawingStandardService(IInterfaceSchemeService _service)
        {
            service = _service;
        }

        public abstract DrawingStandards Standard { get; }

        public Dictionary<MarkTypes, Dictionary<IconSize, Bitmap>> GetIcons()
        {
            var result = new Dictionary<MarkTypes, Dictionary<IconSize, Bitmap>>();

            switch (service.GetScheme())
            {
                case InterfaceScheme.light:
                    result = GetIconForLightScheme();
                    break;
                case InterfaceScheme.dark:
                    result = GetIconForDarkScheme();
                    break;
            }

            return result;
        }

        protected abstract Dictionary<MarkTypes, Dictionary<IconSize, Bitmap>> GetIconForLightScheme();
        protected abstract Dictionary<MarkTypes, Dictionary<IconSize, Bitmap>> GetIconForDarkScheme();
    }
}
