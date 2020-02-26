using System;
using System.Collections.Generic;
using System.ComponentModell;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestToolsBase.MeasureEnvironment.IpThermo;

namespace TestToolsBase.GUI
{
    public partial class WorkplaceSelectionForm : Form
    {
        public WorkplaceSelectionForm()
        {
            InitializeComponent();

            List<ListViewItem> rooms = new List<ListViewItem>();
            foreach (KeyValuePair<int, string> pair in IpThermo.RoomNames)
            {
                ListViewItem i = new ListViewItem(pair.Value);
                i.Tag = pair.Key;
                i.Selected = RoomId == pair.Key;//select default
                rooms.Add(i);
            }

            _listView.Items.AddRange(rooms.ToArray());
        }

        public int RoomId { get; set; }

        private void DoubleClickHandler(object sender, MouseEventArgs e)
        {
            if (_listView.SelectedItems.Count == 1)
            {
                RoomId = (int)_listView.SelectedItems[0].Tag;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
