using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    public class LoadMapButton : Button
    {
        public bool clicked = false;

        public LoadMapButton(Texture2D tex, Vector2 pos) : base(tex, pos)
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

            LoadMapForm loadMap = new LoadMapForm();
            loadMap.ShowDialog();

            if (loadMap.DialogResult == System.Windows.Forms.DialogResult.OK)
                prevClicked = false;
            else
                prevClicked = false;

            Game1.state = State.PLAY;

            base.Effect();
        }
    }
}
