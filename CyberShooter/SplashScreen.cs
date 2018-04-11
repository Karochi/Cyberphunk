using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CyberShooter
{
    public class SplashScreen : GameScreen
    {
        public Image Image;

        public override void LoadContent()
        {
            base.LoadContent();
            Image.LoadContent();
        }
        public override void UnLoadContent()
        {
            base.UnLoadContent();
            Image.UnloadContent();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Image.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }
    }
}
