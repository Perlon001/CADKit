using CADKit.Contracts;
using CADKitElevationMarks.Contracts;
using System.Collections.Generic;
using System.Drawing;

namespace CADKitElevationMarks.Services
{
    public class MarkIconPNB01025Service : MarkIconDrawingStandardService
    {
        public override DrawingStandards Standard { get { return DrawingStandards.PNB01025; } }

        public MarkIconPNB01025Service(IInterfaceSchemeService _service) : base(_service) { }

        protected override Dictionary<MarkTypes, Dictionary<IconSize, Bitmap>> GetIconForLightScheme()
        {
            Dictionary<MarkTypes, Dictionary<IconSize, Bitmap>> result = new Dictionary<MarkTypes, Dictionary<IconSize, Bitmap>>();

            result.Add(MarkTypes.universal, new Dictionary<IconSize, Bitmap>()
            {
                [IconSize.small] = Properties.Resources.mark01_16,
                [IconSize.medium] = Properties.Resources.mark01_32,
                [IconSize.large] = Properties.Resources.mark01_48,
            });
            result.Add(MarkTypes.area, new Dictionary<IconSize, Bitmap>()
            {
                [IconSize.small] = Properties.Resources.mark02_16,
                [IconSize.medium] = Properties.Resources.mark02_32,
                [IconSize.large] = Properties.Resources.mark02_48,
            });

            return result;
        }

        protected override Dictionary<MarkTypes, Dictionary<IconSize, Bitmap>> GetIconForDarkScheme()
        {
            Dictionary<MarkTypes, Dictionary<IconSize, Bitmap>> result = new Dictionary<MarkTypes, Dictionary<IconSize, Bitmap>>();

            result.Add(MarkTypes.universal, new Dictionary<IconSize, Bitmap>()
            {
                [IconSize.small] = Properties.Resources.mark01_16,
                [IconSize.medium] = Properties.Resources.mark01_32_dark,
                [IconSize.large] = Properties.Resources.mark01_48,
            });
            result.Add(MarkTypes.area, new Dictionary<IconSize, Bitmap>()
            {
                [IconSize.small] = Properties.Resources.mark02_16,
                [IconSize.medium] = Properties.Resources.mark02_32_dark,
                [IconSize.large] = Properties.Resources.mark02_48,
            });

            return result;
        }
    }
}
