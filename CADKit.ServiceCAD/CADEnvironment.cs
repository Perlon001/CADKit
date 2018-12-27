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
                if (instance == null)
                {
                    instance = new CADEnvironment();
                    instance.platform = CADPlatforms.ZwCAD;
                }
                return instance;
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
