namespace EightCare.Infrastructure.Common.Configuration
{
    public class DatabaseConfiguration
    {
        public const string Key = "Database";

        public string ConnectionString { get; set; } = string.Empty;
    }
}