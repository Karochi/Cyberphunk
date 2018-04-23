using MapEditor.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapEditor
{
    public class NewMapButton : Button
    {
        public bool clicked = false;

        public NewMapButton(Texture2D tex, Vector2 pos) : base(tex, pos)
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

            NewMapForm newMap = new NewMapForm();
            newMap.ShowDialog();

            if (newMap.DialogResult == DialogResult.OK)
            {
                Game1.mapHeight = newMap.mapHeight;
                Game1.mapWidth = newMap.mapWidth;
                Game1.tileHeight = newMap.tileHeight;
                Game1.tileWidth = newMap.tileWidth;
                Game1.mapName = newMap.mapName;

                Game1.selectedTileNumb = 0;
                Game1.drawOffset = Vector2.Zero;

                Game1.map = new Map(Game1.mapWidth, Game1.mapHeight, Game1.tileWidth, Game1.tileHeight);

                if (Game1.tileSheet != null)
                    Game1.map.LoadTileSet(Game1.tileSheet);

                prevClicked = false;
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
