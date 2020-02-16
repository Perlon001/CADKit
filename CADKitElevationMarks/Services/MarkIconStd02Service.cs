using CADKit.Contracts;
using CADKitElevationMarks.Contracts;
using System.Collections.Generic;
using System.Drawing;

namespace CADKitElevationMarks.Services
{
    public class MarkIconStd02Service : MarkIconDrawingStandardService
    {
        public override DrawingStandards Standard { get { return DrawingStandards.Std02; } }
        
        public MarkIconStd02Service(IInterfaceSchemeService _service) : base(_service) { }

        protected override Dictionary<MarkTypes, Dictionary<IconSize, Bitmap>> GetIconForLightScheme()
        {
            Dictionary<MarkTypes, Dictionary<IconSize, Bitmap>> result = new Dictionary<MarkTypes, Dictionary<IconSize, Bitmap>>();

            result.Add(MarkTypes.finish, new Dictionary<IconSize, Bitmap>()
            {
                [IconSize.small] = Properties.Resources.question,
                [IconSize.medium] = Properties.Resources.mark06_32,
                [IconSize.large] = Properties.Resources.question,
            });
            result.Add(MarkTypes.construction, new Dictionary<IconSize, Bitmap>()
            {
                [IconSize.small] = Properties.Resources.question,
                [IconSize.medium] = Properties.Resources.mark07_32,
                [IconSize.large] = Properties.Resources.question,
            });

            return result;
        }

        protected override Dictionary<MarkTypes, Dictionary<IconSize, Bitmap>> GetIconForDarkScheme()
        {
            Dictionary<MarkTypes, Dictionary<IconSize, Bitmap>> result = new Dictionary<MarkTypes, Dictionary<IconSize, Bitmap>>();

            result.Add(MarkTypes.finish, new Dictionary<IconSize, Bitmap>()
            {
                [IconSize.small] = Properties.Resources.question,
                [IconSize.medium] = Properties.Resources.mark06_32_dark,
                [IconSize.large] = Properties.Resources.question,
            });
            result.Add(MarkTypes.construction, new Dictionary<IconSize, Bitmap>()
            {
                [IconSize.small] = Properties.Resources.question,
                [IconSize.medium] = Properties.Resources.mark07_32_dark,
                [IconSize.large] = Properties.Resources.question,
            });

            return result;
        }
    }
}
