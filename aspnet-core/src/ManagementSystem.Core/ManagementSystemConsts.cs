using ManagementSystem.Debugging;

namespace ManagementSystem
{
    public class ManagementSystemConsts
    {
        public const string LocalizationSourceName = "ManagementSystem";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "3ef880092c6640cb988bc25d83c684f3";
    }
}
