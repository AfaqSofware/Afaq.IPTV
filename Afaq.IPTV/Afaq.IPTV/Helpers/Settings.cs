// Helpers/Settings.cs

using Afaq.IPTV.Enums;
using Afaq.IPTV.Views;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Afaq.IPTV.Helpers
{
    /// <summary>
    ///     This is the Settings static class that can be used in your Core solution or in any
    ///     of your client applications. All settings are laid out the same exact way with getters
    ///     and setters.
    /// </summary>
    public static class Settings
    {
        #region Setting Constants

        private const string IsAutoLoginKey = "IsAutoLogin";
        private static readonly bool _isAutoLoginDefault = false;

        private const string IsRememberPasswordKey = "IsRememberPassword";
        private const bool IsRememberPasswordDefault = false;

        private const string LoginViewTypeSettingsKey = "LoginViewTypeSettings";
        private static readonly LoginViewType _loginViewTypeSettingsDefault = LoginViewType.ActivationCodeView;
        #endregion

        private static ISettings AppSettings
        {
            get { return CrossSettings.Current; }
        }

        public static bool IsRememberPassword
        {
            get { return AppSettings.GetValueOrDefault(IsRememberPasswordKey, IsRememberPasswordDefault); }
            set { AppSettings.AddOrUpdateValue(IsRememberPasswordKey,value); }
        }

        public static LoginViewType LoginViewType
        {
            get { return AppSettings.GetValueOrDefault(LoginViewTypeSettingsKey, _loginViewTypeSettingsDefault); }
            set { AppSettings.AddOrUpdateValue(LoginViewTypeSettingsKey, value); }
        }

        public static bool IsAutoLogin
        {
            get { return AppSettings.GetValueOrDefault(IsAutoLoginKey, _isAutoLoginDefault); }
            set { AppSettings.AddOrUpdateValue(IsAutoLoginKey, value); }
        }


    }
}