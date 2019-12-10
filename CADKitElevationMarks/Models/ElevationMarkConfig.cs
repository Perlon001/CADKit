using CADKit.Contracts;
using CADKit.Contracts.Entities;
using CADKitElevationMarks.Contracts;

namespace CADKitElevationMarks.Models
{
    public class ElevationMarkConfig : IElevationMarkConfig
    {
        public string Layer { get; set; }

        public ElevationMarkConfig()
        {
            // Create fields and read default value from AppSettings
        }

    }
}
