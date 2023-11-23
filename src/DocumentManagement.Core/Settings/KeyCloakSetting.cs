namespace DocumentManagement.Core.Settings
{
    public sealed class KeyCloakSetting
    {
        public string? ServerAddress { get; set; }

        public string? Realm { get; set; }

        public string? Audience { get; set; }
    }
}
