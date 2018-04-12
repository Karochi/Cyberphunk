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
        Song menuSong;
        public Image image;       

        public override void LoadContent()
        {
            base.LoadContent();
            menuSong = content.Load<Song>("Electro Zombies");
            MediaPlayer.Play(menuSong);
            MediaPlayer.IsRepeating = true;
            image.LoadContent();
        }
        public override void UnLoadContent()
        {
            base.UnLoadContent();
            image.UnloadContent();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            image.Update(gameTime);
            if (KeyMouseReader.KeyPressed(Keys.Enter))
                MediaPlayer.Stop();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            image.Draw(spriteBatch);
        }
    }
}
