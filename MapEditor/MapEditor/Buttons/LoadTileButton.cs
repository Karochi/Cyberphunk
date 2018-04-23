using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    public class LoadTileButton : Button
    {
        public bool clicked = false;

        public LoadTileButton(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
        }
        public override void Update()
        {
            clicked = base.clicked;
            base.Update();
        }
        public override void Effect()
        {
            Game1.state = State.FREEZE;

            NewTileSheet frmTileSheet = new NewTileSheet();
            frmTileSheet.ShowDialog();

            if(frmTileSheet.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                Game1.tileHeight = frmTileSheet.tileHeight;
                Game1.tileWidth = frmTileSheet.tileWidth;
                Game1.tileSheetFileName = frmTileSheet.sheetFileName;
                prevClicked = false;

                try
                {
                    using (FileStream fileStream = new FileStream(Game1.tileSheetFileName, FileMode.Open))
                    {
                        Game1.tileSheet = Texture2D.FromStream(Game1.graphics.GraphicsDevice, fileStream);
                    }
                    Game1.map.LoadTileSet(Game1.tileSheet);
                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show("There was a error loading the texture.");
                }
            }
            else
            {
                prevClicked = false;
            }
            Game1.state = State.PLAY;

            base.Effect();
        }
    }
}
