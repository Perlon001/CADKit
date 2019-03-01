namespace CADKit.ServiceCAD
{
    public sealed class CADEnvironment
    {
        private static CADEnvironment instance = null;
        private CADPlatforms platform;

        public static CADEnvironment Instance
        {
            get
            {
                return instance ?? new CADEnvironment() { platform = CADPlatforms.ZwCAD };
            }
        }

        public CADPlatforms Platform
        {
            get
            {
                return platform;
            }
        }

        public string PlatformName
        {
            get
            {
                return platform.ToString();
            }
        }
    }
}
