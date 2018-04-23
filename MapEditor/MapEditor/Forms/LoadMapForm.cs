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
    public partial class LoadMapForm : Form
    {
        string loadFileName;

        public LoadMapForm()
        {
            InitializeComponent();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\";

            openFileDialog1.Title = "Select a Map File";
            openFileDialog1.FileName = "";

            openFileDialog1.Filter = "Text Files (*.txt) | *.txt";
            openFileDialog1.FilterIndex = 1;

            if(openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                loadFileName = openFileDialog1.FileName;
                fileName.Text = loadFileName;
            }
            else
            {
                loadFileName = "";
            }

        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if(Game1.tileSheetFileName != null)
            {
                Game1.map.LoadMap(loadFileName);
            }
            else
            {
                MessageBox.Show("Please load a title set before loading a map");
            }

            Game1.mapHeight = Game1.map.mapHeight;
            Game1.mapWidth = Game1.map.mapWidth;
            Game1.tileHeight = Game1.map.tileHeight;
            Game1.tileWidth = Game1.map.tileWidth;

            Game1.map = new Map(Game1.mapWidth, Game1.mapHeight, Game1.tileWidth, Game1.tileHeight);

            Game1.map.LoadMap(loadFileName);

            Game1.map.LoadTileSet(Game1.tileSheet);

            DialogResult = DialogResult.OK;
        }
    }
}
