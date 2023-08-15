using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GateKeePass.Database
{
    public class GroupInfo
    {
        public string Name { get; set; }
        public List<EntryInfo> Entries { get; set; }
        public List<GroupInfo> SubGroups { get; set; }

        public GroupInfo()
        {
            Entries = new List<EntryInfo>();
            SubGroups = new List<GroupInfo>();
        }
    }
}
