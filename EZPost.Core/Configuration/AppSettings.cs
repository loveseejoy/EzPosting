namespace EZPost.Configuration
{
    public static class AppSettings
    {
        public  static class DataBase
        {
            public const string SamConnectionString = "App.DataBase.SamConnectionString";
        }
        public  static class  DhlFtp
        {
            public const string FtpAddress = "App.DhlFtp.FtpAddress";
            public const string FtpPort = "App.DhlFtp.FtpPort";
            public const string FtpUserName = "App.DhlFtp.FtpUserName";
            public const string FtpPassword = "App.DhlFtp.FtpPassword";
            public const string FtpPath = "App.DhlFtp.FtpPath";
        }
    }
}