using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeePassLib.Keys;
using KeePassLib.Security;
using KeePassLib.Serialization;
using KeePassLib;
using GateKeePass.Extensions;

namespace GateKeePass.Database
{
    public class KDBXDatabase
    {
        public PwDatabase Database;
        private readonly IOConnectionInfo _connectionInfo;

        public KDBXDatabase(string databasePath, string keyPath, string password)
        {
            _connectionInfo = new IOConnectionInfo { Path = databasePath };
            var compKey = new CompositeKey();

            // Add the master password and key file to the composite key
            compKey.AddUserKey(new KcpPassword(password));
            compKey.AddUserKey(new KcpKeyFile(keyPath));

            Database = new PwDatabase();
            Database.Open(_connectionInfo, compKey, null);
        }

        public PwGroup FindGroup(string groupName)
        {
            return Database.RootGroup.FindCreateGroup(groupName, false);
        }

        public PwGroup FindGroupRecursive(PwGroup parentGroup, string groupName)
        {
            foreach (PwGroup group in parentGroup.Groups)
            {
                if (group.Name == groupName)
                {
                    return group;
                }

                PwGroup foundGroup = FindGroupRecursive(group, groupName);
                if (foundGroup != null)
                {
                    return foundGroup;
                }
            }

            return null;
        }

        public PwEntry FindEntry(string groupName, string entryTitle)
        {
            PwGroup targetGroup = Database.RootGroup.FindCreateGroup(groupName, false);
            if (targetGroup == null)
            {
                return null;
            }

            PwEntry targetEntry = null;
            foreach (PwEntry entry in targetGroup.Entries)
            {
                if (entry.Strings.ReadSafe(PwDefs.TitleField) == entryTitle)
                {
                    targetEntry = entry;
                    break;
                }
            }

            return targetEntry;
        }
            //public void AddEntry(string title, string username, string password, string url, string notes)
            //{
            //    var entry = new PwEntry(true, true);
            //    entry.Strings.Set(PwDefs.TitleField, new ProtectedString(Database.MemoryProtection.ProtectTitle, title));
            //    entry.Strings.Set(PwDefs.UserNameField, new ProtectedString(Database.MemoryProtection.ProtectUserName, username));
            //    entry.Strings.Set(PwDefs.PasswordField, new ProtectedString(Database.MemoryProtection.ProtectPassword, password));
            //    entry.Strings.Set(PwDefs.UrlField, new ProtectedString(Database.MemoryProtection.ProtectUrl, url));
            //    entry.Strings.Set(PwDefs.NotesField, new ProtectedString(Database.MemoryProtection.ProtectNotes, notes));

            //    Database.RootGroup.AddEntry(entry, true);
            //}

            public void SaveDatabase()
        {
            NullStatusLogger statusLogger = new NullStatusLogger();

            Database.Save(statusLogger);

            Console.WriteLine("Database saved. Status: " + statusLogger.ToString());
        }

        public void Lock()
        {
        }

        public void Close()
        {
            Database.Close();
        }
    }
}
