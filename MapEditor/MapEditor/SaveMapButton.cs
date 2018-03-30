using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    public class SaveMapButton : Button
    {
        public bool clicked = false;

        public SaveMapButton(Texture2D tex, Vector2 pos) : base(tex, pos)
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

            SaveMapForm saveMap = new SaveMapForm();
            saveMap.ShowDialog();

            if (saveMap.DialogResult == System.Windows.Forms.DialogResult.OK)
                prevClicked = false;
            else
                prevClicked = false;

            Game1.state = State.PLAY;

            base.Effect();
        }
    }
}
