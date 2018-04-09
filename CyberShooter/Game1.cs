using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CyberShooter
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        public enum GameStates { start, loadingLevel, gameOn, gameOver };
        GameStates gameState;
        public static Texture2D square, crosshairTex;
        public static Texture2D tileSheet;
        SpriteFont spriteFont;
        Vector2 target, crosshairPos;
        Camera camera;
        int screenWidth, screenHeight;
        GameBoard gameBoard;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            screenWidth = 800;
            screenHeight = 600;
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            square = Content.Load<Texture2D>("plattform");
            tileSheet = Content.Load<Texture2D>("32tilesheet");
            spriteFont = Content.Load<SpriteFont>("spriteFont");
            crosshairTex = Content.Load<Texture2D>("crosshair");
            Viewport view = GraphicsDevice.Viewport;
            camera = new Camera(view);

            gameState = GameStates.start;
            gameBoard = new GameBoard(screenWidth, screenHeight);
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyMouseReader.Update();
            if(gameState == GameStates.start)
            {
                if (KeyMouseReader.KeyPressed(Keys.Enter))
                {
                    gameState = GameStates.gameOn;
                }
            }
            if (gameBoard.GetPlayer().GetIsDead())
            {
                gameState = GameStates.gameOver;
            }
            if(gameState == GameStates.gameOn)
            {
                gameBoard.Update(gameTime, target);
            }
            CameraUpdate();
            base.Update(gameTime);
        }
        protected void CameraUpdate()
        {
            if (gameState == GameStates.gameOn)
            {
                camera.SetPosition(gameBoard.GetPlayer().GetPosition());
                camera.GetPosition();
            }
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            target = Vector2.Transform(new Vector2(KeyMouseReader.mousePosition.X, KeyMouseReader.mousePosition.Y), Matrix.Invert(camera.GetTransform()));
            int crosshairWidth = 32;
            int crosshairHeight = 32;
            crosshairPos = new Vector2(target.X - crosshairWidth/2, target.Y - crosshairHeight/2);

            //This spriteBatch implies everything in the batch is centered on the player-plane.
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetTransform());
            gameBoard.Draw(spriteBatch, square);

            foreach(Pickup pickUp in gameBoard.GetPickUpList())
            {
                if (pickUp.GetIsInteractable())
                {
                    spriteBatch.DrawString(spriteFont, "E", new Vector2(pickUp.GetPosition().X + pickUp.GetTexWidth() / 2, pickUp.GetPosition().Y - 10), Color.Black);
                }
            }
            spriteBatch.Draw(crosshairTex, new Rectangle((int)crosshairPos.X, (int)crosshairPos.Y, crosshairWidth, crosshairHeight), Color.White);
            spriteBatch.End();
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, "First Weapon: " + gameBoard.GetPlayer().GetFirstWeapon().GetWeaponName(), Vector2.Zero, Color.White);
            spriteBatch.DrawString(spriteFont, "Second Weapon: " + gameBoard.GetPlayer().GetSecondWeapon().GetWeaponName(), new Vector2(0,20), Color.White);
            spriteBatch.DrawString(spriteFont, "Ammo: " + gameBoard.GetPlayer().GetAmmo(), new Vector2(0, 40), Color.White);
            spriteBatch.DrawString(spriteFont, "Health: " + gameBoard.GetPlayer().GetHealth(), new Vector2(0, 60), Color.White);
            if (gameState == GameStates.start)
            {
                spriteBatch.DrawString(spriteFont, "PRESS ENTER", new Vector2(screenWidth / 2, screenHeight / 2), Color.Red);
            }
            if (gameState == GameStates.gameOver)
            {
                spriteBatch.DrawString(spriteFont, "GAME OVER", new Vector2(screenWidth / 2, screenHeight / 2), Color.Red);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
