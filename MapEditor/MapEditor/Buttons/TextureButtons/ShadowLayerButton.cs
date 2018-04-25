using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    class ShadowLayerButton : Button
    {
        public bool clicked = false;

        public ShadowLayerButton(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            height = 50;
            width = 50;
        }
        public override void Update()
        {
            clicked = base.clicked;
            base.Update();
        }
        public override void Effect()
        {
            Game1.drawableLayer = 2;
            base.prevClicked = false;

            base.Effect();
        }
    }
}
