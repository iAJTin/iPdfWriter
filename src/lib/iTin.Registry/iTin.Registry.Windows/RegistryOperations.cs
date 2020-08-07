
namespace iTin.Registry.Windows
{
    using Microsoft.Win32;

    /// <summary>
    /// This class contains basic operations for handles a <b>Microsoft Windows Registry</b>.
    /// </summary>
    public static class RegistryOperations
    {
        /// <summary>
        /// Performs a test that checks if the specified key exists in the classes root branch of the registry. If the specified key is a blank string it returns <b>false</b>.
        /// </summary>
        /// <param name="subkey">Subkey to check</param>
        /// <returns>
        /// <b>true</b> if the specified key exists in the registry; otherwise <b>false</b>.
        /// </returns>
        public static bool CheckClassesRootKey(string subkey) => !string.IsNullOrEmpty(subkey) && Registry.ClassesRoot.OpenSubKey(subkey, false) != null;

        /// <summary>
        /// Performs a test that checks if the specified key exists in the current config branch of the registry. If the specified key is a blank string it returns <b>false</b>.
        /// </summary>
        /// <param name="subkey">Subkey to check</param>
        /// <returns>
        /// <b>true</b> if the specified key exists in the registry; otherwise <b>false</b>.
        /// </returns>
        public static bool CheckCurrentConfigKey(string subkey) => !string.IsNullOrEmpty(subkey) && Registry.CurrentConfig.OpenSubKey(subkey, false) != null;

        /// <summary>
        /// Performs a test that checks if the specified key exists in the current user branch of the registry. If the specified key is a blank string it returns <b>false</b>.
        /// </summary>
        /// <param name="subkey">Subkey to check</param>
        /// <returns>
        /// <b>true</b> if the specified key exists in the registry; otherwise <b>false</b>.
        /// </returns>
        public static bool CheckCurrentUserKey(string subkey) => !string.IsNullOrEmpty(subkey) && Registry.CurrentUser.OpenSubKey(subkey, false) != null;

        /// <summary>
        /// Performs a test that checks if the specified key exists in the machine branch of the registry. If the specified key is a blank string it returns <b>false</b>.
        /// </summary>
        /// <param name="subkey">Subkey to check</param>
        /// <returns>
        /// <b>true</b> if the specified key exists in the registry; otherwise <b>false</b>.
        /// </returns>
        public static bool CheckMachineKey(string subkey) => !string.IsNullOrEmpty(subkey) && Registry.LocalMachine.OpenSubKey(subkey, false) != null;

        /// <summary>
        /// Performs a test that checks if the specified key exists in the users branch of the registry. If the specified key is a blank string it returns <b>false</b>.
        /// </summary>
        /// <param name="subkey">Subkey to check</param>
        /// <returns>
        /// <b>true</b> if the specified key exists in the registry; otherwise <b>false</b>.
        /// </returns>
        public static bool CheckUsersKey(string subkey) => !string.IsNullOrEmpty(subkey) && Registry.Users.OpenSubKey(subkey, false) != null;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subkey"></param>
        /// <param name="keyName"></param>
        /// <returns>
        /// </returns>
        public static T GetClassesRootKeyValue<T>(string subkey, string keyName)
        {
            var existSubkey = CheckClassesRootKey(subkey);
            if (!existSubkey)
            {
                return default(T);
            }

            if (string.IsNullOrEmpty(keyName))
            {
                return default(T);
            }

            return (T)Registry.GetValue($@"HKEY_CLASSES_ROOT\{subkey}", keyName, default(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subkey"></param>
        /// <param name="keyName"></param>
        /// <returns>
        /// </returns>
        public static T GetCurrentConfigKeyValue<T>(string subkey, string keyName)
        {
            var existSubkey = CheckCurrentConfigKey(subkey);
            if (!existSubkey)
            {
                return default(T);
            }

            if (string.IsNullOrEmpty(keyName))
            {
                return default(T);
            }

            return (T)Registry.GetValue($@"HKEY_CURRENT_CONFIG\{subkey}", keyName, default(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subkey"></param>
        /// <param name="keyName"></param>
        /// <returns>
        /// </returns>
        public static T GetCurrentUserKeyValue<T>(string subkey, string keyName)
        {
            var existSubkey = CheckCurrentUserKey(subkey);
            if (!existSubkey)
            {
                return default(T);
            }

            if (string.IsNullOrEmpty(keyName))
            {
                return default(T);
            }

            return (T)Registry.GetValue($@"HKEY_CURRENT_USER\{subkey}", keyName, default(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subkey"></param>
        /// <param name="keyName"></param>
        /// <param name="defaultValue"></param>
        /// <returns>
        /// </returns>
        public static T GetKeyValue<T>(string subkey, string keyName, T defaultValue)
        {
            if (string.IsNullOrEmpty(subkey))
            {
                return defaultValue;
            }

            if (string.IsNullOrEmpty(keyName))
            {
                return defaultValue;
            }

            return (T)Registry.GetValue(subkey, keyName, defaultValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subkey"></param>
        /// <param name="keyName"></param>
        /// <returns>
        /// </returns>
        public static T GetMachineKeyValue<T>(string subkey, string keyName)
        {
            var existSubkey = CheckMachineKey(subkey);
            if (!existSubkey)
            {
                return default(T);
            }

            if (string.IsNullOrEmpty(keyName))
            {
                return default(T);
            }

            return (T)Registry.GetValue($@"HKEY_LOCAL_MACHINE\{subkey}", keyName, default(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subkey"></param>
        /// <param name="keyName"></param>
        /// <returns>
        /// </returns>
        public static T GetUsersKeyValue<T>(string subkey, string keyName)
        {
            var existSubkey = CheckUsersKey(subkey);
            if (!existSubkey)
            {
                return default(T);
            }

            if (string.IsNullOrEmpty(keyName))
            {
                return default(T);
            }

            return (T)Registry.GetValue($@"HKEY_USERS\{subkey}", keyName, default(T));
        }
    }
}
