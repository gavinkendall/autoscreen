namespace AutoScreenCapture
{
    using System;

    public class Version
    {
        public string Codename { get; set; }

        public string VersionString { get; set; }

        public int VersionNumber { get; }

        public Version(string appCodename, string appVersion)
        {
            Codename = appCodename;
            VersionString = appVersion;
            VersionNumber = Convert.ToInt32(appVersion.Replace(".", string.Empty));
        }
    }
}
