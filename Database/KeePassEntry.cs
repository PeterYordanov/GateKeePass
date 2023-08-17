namespace GateKeePass.Database
{
    public enum EntryType
    {
        Password,
        Group
    }

    public class KeePassEntry
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Uuid { get; set; }
        public EntryType Type { get; set; }
    }
}
