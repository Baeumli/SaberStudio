namespace SaberStudio.Services.BeatMods.Models
{
    public static class Constants
    {
        public static class GameVersion
        {
            public const string Any = "";
            public const string V1_12_2= "1.12.2";
            public const string V1_11_0 = "1.11.0";
            public const string V1_8_0 = "1.8.0";
            public const string V1_6_1 = "1.6.1";
            public const string V1_6_0 = "1.6.0";
            public const string V1_5_0 = "1.5.0";
            public const string V1_3_0 = "1.3.0";
            public const string V1_1_0p1 = "1.1.0p1";
            public const string V1_1_0 = "1.1.0";
            public const string V1_0_0 = "1.0.0";
            public const string V0_13_2 = "0.13.2";
        }

        public static class SortType
        {
            public const string Category = "category_lower";
            public const string Name = "name_lower";
            public const string Status = "status_lower";
            public const string Author = "author.username_lower";
            public const string LastUpdated = "updatedDate";
        }

        public static class Status
        {
            public const string Approved = "approved";
        }
    }
}
