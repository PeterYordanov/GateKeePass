using KeePassLib;

namespace GateKeePass.Database
{
    public class EntryInfo
    {
        public string Title { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string URL { get; set; }
        public string Notes { get; set; }
        public PwIcon IconId { get; set; }
    }
}
