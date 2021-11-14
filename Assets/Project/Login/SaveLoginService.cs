using JetBrains.Annotations;
using UnityEngine;

namespace Project.Login
{
    [UsedImplicitly]
    public static class SaveLoginService
    {
        private const string UsernameKey = "Username";
        private const string PasswordKey = "Password";

        public static void Save(string username, string password)
        {
            PlayerPrefs.SetString(UsernameKey, username);
            PlayerPrefs.SetString(PasswordKey, password);
        }

        public static (string username, string password) Load() =>
            (PlayerPrefs.GetString(UsernameKey), PlayerPrefs.GetString(PasswordKey));

        public static void Clear()
        {
            PlayerPrefs.DeleteKey(UsernameKey);
            PlayerPrefs.DeleteKey(PasswordKey);
        }
    }
}