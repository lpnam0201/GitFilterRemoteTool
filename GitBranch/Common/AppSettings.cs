namespace GitBranch.Common
{
    public class AppSettings
    {
        public string RemotePrefix { get; set; }
        public string SourceDirectory { get; set; }
        public bool CallGitFetchBeforeSearch { get; set; }
    }
}
