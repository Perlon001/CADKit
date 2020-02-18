using Microsoft.Win32;

namespace CADKitBasic.Utils
{
    public static class WindowsRegistry
    {
        public static void SetKeyToRegister(string appName, string name, object value)
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software", true);
            RegistryKey subKey = regKey.CreateSubKey(appName);
            subKey.SetValue(name, value);
        }

        public static object GetKeyFromRegister(string appName, string name, object defaultValue)
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software", true);
            RegistryKey subKey = regKey.CreateSubKey(appName);
            return subKey.GetValue(name, defaultValue);
        }

        //public static void SaveSettingsForm(string appName, Form appForm)
        //{
        //    SetKeyToRegister(appName, "WindowState", (int)appForm.WindowState);
        //    if (appForm.WindowState == FormWindowState.Normal)
        //    {
        //        SetKeyToRegister(appName, "FormLeft", appForm.Left);
        //        SetKeyToRegister(appName, "FormTop", appForm.Top);
        //        SetKeyToRegister(appName, "FormWidth", appForm.Width);
        //        SetKeyToRegister(appName, "FormHeight", appForm.Height);
        //    }
        //    else
        //    {
        //        SetKeyToRegister(appName, "FormLeft", appForm.RestoreBounds.Left);
        //        SetKeyToRegister(appName, "FormTop", appForm.RestoreBounds.Top);
        //        SetKeyToRegister(appName, "FormWidth", appForm.RestoreBounds.Width);
        //        SetKeyToRegister(appName, "FormHeight", appForm.RestoreBounds.Height);
        //    }
        //}

        //public static void RestoreSettingsForm(string appName, Form appForm)
        //{
        //    appForm.SetBounds(
        //        (int)GetKeyFromRegister(appName, "FormLeft", appForm.Left),
        //        (int)GetKeyFromRegister(appName, "FormTop", appForm.Top),
        //        (int)GetKeyFromRegister(appName, "FormWidth", appForm.Width),
        //        (int)GetKeyFromRegister(appName, "FormHeight", appForm.Height));
        //    appForm.WindowState = (FormWindowState)GetKeyFromRegister(appName, "WindowState", appForm.WindowState);
        //}
    }
}
