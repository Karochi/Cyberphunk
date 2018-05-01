using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberShooter
{
    public class StartMenu
    {
        public Button newGame, loadGame, quitGame;
        Vector2 newGamePos, loadGamePos, quitGamePos;
        SoundEffect hoverSound, clickSound;
        Texture2D crosshair;
        Rectangle mouseRect;
        public StartMenu(ContentManager content)
        {
            newGamePos = new Vector2(190, 140);
            loadGamePos = new Vector2(170, 230);
            quitGamePos = new Vector2(300, 400);
            hoverSound = content.Load<SoundEffect>("tone1");
            clickSound = content.Load<SoundEffect>("threeTone2");
            newGame = new Button(content.Load<Texture2D>("newGameButton"), newGamePos,50,400 ,hoverSound,clickSound);
            loadGame = new Button(content.Load<Texture2D>("loadGameButton"), loadGamePos, 50, 450, hoverSound, clickSound);
            quitGame = new Button(content.Load<Texture2D>("quitGameButton"), quitGamePos, 50, 150, hoverSound, clickSound);
            mouseRect = new Rectangle(((int)KeyMouseReader.mousePosition.X-25 ), ((int)KeyMouseReader.mousePosition.Y-25), 50, 50);
            crosshair = content.Load<Texture2D>("crosshair");
        }
        public void LoadContent()
        {
        }
        public void UnLoadContent()
        {

        }
        public void Update(GameTime gameTime, GameStates gamestate)
        {
            mouseRect = new Rectangle(((int)KeyMouseReader.mousePosition.X - 25), ((int)KeyMouseReader.mousePosition.Y - 25), 50, 50);
            newGame.Update();
            loadGame.Update();
            quitGame.Update();
        }
        public void Draw()
        {
            newGame.Draw();
            loadGame.Draw();
            quitGame.Draw();
            Game1.spriteBatch.Draw(crosshair, mouseRect, Color.White);
        }
    }
}
