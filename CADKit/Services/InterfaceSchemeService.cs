using CADKit.Contracts;
using Microsoft.Win32;
using System;
using System.Drawing;

namespace CADKit.Services
{
    public class InterfaceSchemeService : IInterfaceSchemeService
    {
        public static InterfaceScheme ColorScheme
        {
            get
            {
                var schemeValue = (int)Registry.CurrentUser.OpenSubKey(@"Software\ZWSOFT\ZWCAD\2020\en-US\Profiles\Default\Config", false).GetValue("COLORSCHEME");
                switch (schemeValue)
                {
                    case 0:
                        return InterfaceScheme.dark;
                    case 1:
                        return InterfaceScheme.light;
                }
                throw new NotImplementedException("Nie rozpoznany schemat kolorów interfejsu");
            }
        }

        public Color GetBackColor()
        {
            switch (ColorScheme)
            {
                case InterfaceScheme.dark:
                    //return Color.FromArgb(66, 76, 88);
                    return Color.FromArgb(66, 76, 88);
                case InterfaceScheme.light:
                    //return Color.FromArgb(255, 255, 255);
                    return Color.FromArgb(255, 255, 255);
                default:
                    return SystemColors.Window;
            }
        }

        public Color GetForeColor()
        {
            switch (ColorScheme)
            {
                case InterfaceScheme.dark:
                    return Color.FromArgb(255, 255, 255);
                case InterfaceScheme.light:
                    return Color.FromArgb(0, 0, 0);
                default:
                    return SystemColors.Window;
            }
        }

        public InterfaceScheme GetScheme()
        {
            return ColorScheme;
        }
    }
}
