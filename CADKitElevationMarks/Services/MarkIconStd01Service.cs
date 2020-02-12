using CADKit.Contracts;
using CADKitElevationMarks.Contracts;
using System.Collections.Generic;
using System.Drawing;

namespace CADKitElevationMarks.Services
{
    public class MarkIconStd01Service : MarkIconDrawingStandardService
    {
        public override DrawingStandards Standard { get { return DrawingStandards.Std01; } }

        public MarkIconStd01Service(IInterfaceSchemeService _service) : base(_service) { }

        protected override Dictionary<MarkTypes, Dictionary<IconSize, Bitmap>> GetIconForLightScheme()
        {
            Dictionary<MarkTypes, Dictionary<IconSize, Bitmap>> result = new Dictionary<MarkTypes, Dictionary<IconSize, Bitmap>>();

            result.Add(MarkTypes.finish, new Dictionary<IconSize, Bitmap>()
            {
                [IconSize.small] = Properties.Resources.mark03_16,
                [IconSize.medium] = Properties.Resources.mark03_32,
                [IconSize.large] = Properties.Resources.mark03_48,
            });
            result.Add(MarkTypes.construction, new Dictionary<IconSize, Bitmap>()
            {
                [IconSize.small] = Properties.Resources.mark04_16,
                [IconSize.medium] = Properties.Resources.mark04_32,
                [IconSize.large] = Properties.Resources.mark04_48,
            });
            result.Add(MarkTypes.area, new Dictionary<IconSize, Bitmap>()
            {
                [IconSize.small] = Properties.Resources.mark05_16,
                [IconSize.medium] = Properties.Resources.mark05_32,
                [IconSize.large] = Properties.Resources.mark05_48,
            });

            return result;
        }

        protected override Dictionary<MarkTypes, Dictionary<IconSize, Bitmap>> GetIconForDarkScheme()
        {
            Dictionary<MarkTypes, Dictionary<IconSize, Bitmap>> result = new Dictionary<MarkTypes, Dictionary<IconSize, Bitmap>>();

            result.Add(MarkTypes.finish, new Dictionary<IconSize, Bitmap>()
            {
                [IconSize.small] = Properties.Resources.mark03_16,
                [IconSize.medium] = Properties.Resources.mark03_32_dark,
                [IconSize.large] = Properties.Resources.mark03_48,
            });
            result.Add(MarkTypes.construction, new Dictionary<IconSize, Bitmap>()
            {
                [IconSize.small] = Properties.Resources.mark04_16,
                [IconSize.medium] = Properties.Resources.mark04_32_dark,
                [IconSize.large] = Properties.Resources.mark04_48,
            });
            result.Add(MarkTypes.area, new Dictionary<IconSize, Bitmap>()
            {
                [IconSize.small] = Properties.Resources.mark05_16,
                [IconSize.medium] = Properties.Resources.mark05_32_dark,
                [IconSize.large] = Properties.Resources.mark05_48,
            });

            return result;
        }
    }
}
