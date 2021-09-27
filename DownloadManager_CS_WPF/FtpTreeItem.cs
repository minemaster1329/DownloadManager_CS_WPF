using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManager_CS_WPF
{
    class FtpTreeItem
    {
        public int Key { get; set; }
        public string Name { get; set; }

        public IList<FtpTreeItem> SubTreeItems { get; set; }
        public IList<FtpTreeItemEntry> Entries { get; set; }

        public IList<object> Items
        {
            get
            {
                IList<object> chilldren = new List<object>();
                foreach (var group in this.SubTreeItems)
                {
                    chilldren.Add(group);
                }

                foreach (var entry in this.Entries)
                {
                    chilldren.Add(entry);
                }
                return chilldren;
            }
        }

        public FtpTreeItem()
        {
            SubTreeItems = new List<FtpTreeItem>();
            Entries = new List<FtpTreeItemEntry>();
        }
    }

    class FtpTreeItemEntry
    {
        public int Key { get; set; }
        public string Name { get; set; }
    }
}
