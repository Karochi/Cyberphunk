using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapEditor.Content
{
    public partial class NewMapForm : Form
    {
        public int mapHeight;
        public int mapWidth;
        public int tileHeight;
        public int tileWidth;

        public string mapName;

        public NewMapForm()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            mapName = mapNameText.Text;
            mapHeight = Convert.ToInt32(mapHeightBox.Value);
            mapWidth = Convert.ToInt32(mapWidthBox.Value);
            tileHeight = Convert.ToInt32(tileHeightBox.Value);
            tileWidth = Convert.ToInt32(tileWidthBox.Value);

            this.DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
