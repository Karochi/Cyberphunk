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
        SpriteFont splashFont;
        public string Path;

        public override void LoadContent()
        {
            base.LoadContent();
            splashFont = content.Load<SpriteFont>(Path);
        }
        public override void UnLoadContent()
        {
            base.UnLoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(splashFont,"CYBERPHUNK", new Vector2((int)ScreenManager.Instance.Dimensions.X/2 - 200, (int)ScreenManager.Instance.Dimensions.Y / 2 - 40), Color.Purple);
        }
    }
}
