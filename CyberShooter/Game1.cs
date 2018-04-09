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
        public static Texture2D square, crosshairTex,tileSheet, healthBarTex, handgunTex, rifleTex, unarmedTex;
        public static SpriteFont spriteFont;
        Vector2 target, crosshairPos;
        Camera camera;
        HUD hud; 
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
            healthBarTex = Content.Load<Texture2D>("healthbartex");
            rifleTex = Content.Load<Texture2D>("rifleTex");
            handgunTex = Content.Load<Texture2D>("handgunTex");
            unarmedTex = Content.Load<Texture2D>("unarmedTex");
            Viewport view = GraphicsDevice.Viewport;
            camera = new Camera(view);

            gameState = GameStates.start;
            gameBoard = new GameBoard(screenWidth, screenHeight);
            hud = new HUD(gameBoard.GetPlayer());
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
            hud.Update(gameBoard.GetPlayer());
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
            GraphicsDevice.Clear(Color.Black);

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
            hud.Draw(gameBoard.GetPlayer());
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
