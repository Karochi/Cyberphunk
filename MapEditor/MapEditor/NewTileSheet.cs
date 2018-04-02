using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapEditor
{
    public partial class NewTileSheet : Form
    {
        public string sheetFileName;

        public int tileHeight = 32;
        public int tileWidth = 32;


        public NewTileSheet()
        {
            InitializeComponent();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\";

            openFileDialog1.Title = "Select a Map File";
            openFileDialog1.FileName = "";

            openFileDialog1.Filter = "Image Files (*.png)| *.png";
            openFileDialog1.FilterIndex = 1;

            if(openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                sheetFileName = openFileDialog1.FileName;
            }
            else
            {
                sheetFileName = "";
            }

            fileNameTextBox.Text = sheetFileName;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            tileHeight = Convert.ToInt32(TileHeightBox.Value);
            tileWidth = Convert.ToInt32(tileWidthBox.Value);
            DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
