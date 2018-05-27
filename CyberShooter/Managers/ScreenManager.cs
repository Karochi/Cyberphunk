using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberShooter
{
    public class ScreenManager
    {
        private static ScreenManager instance;
        public Vector2 dimensions { private set; get; }
        public ContentManager content { private set; get; }
        XmlManager<GameScreen> xmlGameScreenManager;

        GameScreen currentScreen;
        public GraphicsDevice graphicsDevice;
        public SpriteBatch spriteBatch;

        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();

                return instance;
            }
        }
        public ScreenManager()
        {
            dimensions = new Vector2(1920, 1080);
            currentScreen = new SplashScreen();
            xmlGameScreenManager = new XmlManager<GameScreen>();
            xmlGameScreenManager.type = currentScreen.type;
            currentScreen = xmlGameScreenManager.Load("Load/SplashScreen.xml");
        }
        public void LoadContent (ContentManager Content)
        {
            this.content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen.LoadContent();

        }
        public void UnLoadContent()
        {
            currentScreen.UnLoadContent();
        }
        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
        }
        public void Draw(SpriteBatch spritebatch)
        {
            currentScreen.Draw(spritebatch);
        }
    }
}
