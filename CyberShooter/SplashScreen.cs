using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CyberShooter
{
    public class SplashScreen : GameScreen
    {
        Song MenuSong;
        public Image Image;       

        public override void LoadContent()
        {
            base.LoadContent();
            MenuSong = content.Load<Song>("Electro Zombies");
            MediaPlayer.Play(MenuSong);
            MediaPlayer.IsRepeating = true;
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
            if (KeyMouseReader.KeyPressed(Keys.Enter))
                MediaPlayer.Stop();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }
    }
}
