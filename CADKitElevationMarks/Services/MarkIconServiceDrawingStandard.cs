using CADKit.Contracts;
using CADKitElevationMarks.Contracts;
using System.Collections.Generic;
using System.Drawing;

namespace CADKitElevationMarks.Services
{
    public abstract class MarkIconServiceDrawingStandard
    {
        protected readonly IInterfaceSchemeService service;

        public MarkIconServiceDrawingStandard(IInterfaceSchemeService _service)
        {
            service = _service;
        }

        internal Dictionary<MarkTypes, Dictionary<IconSize, Bitmap>> GetIcons()
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
