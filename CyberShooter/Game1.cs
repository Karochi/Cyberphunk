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
        public static Texture2D square, crosshairTex,tileSheet, healthBarTex, handgunTex, rifleTex, unarmedTex, dialoghitbox, friendlyProTex,enemyProTex;
        public static SpriteFont spriteFont;
        Vector2 target, crosshairPos;
        Rectangle target_rect, dialoghitbox_rect;
        Camera camera;
        HUD hud; 
        int screenWidth, screenHeight;
        GameBoard gameBoard;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            screenWidth = (int)ScreenManager.Instance.Dimensions.X;
            screenHeight = (int)ScreenManager.Instance.Dimensions.Y;
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
            ScreenManager.Instance.GraphicsDevice = GraphicsDevice;
            ScreenManager.Instance.SpriteBatch = spriteBatch;
            ScreenManager.Instance.LoadContent(Content);
            square = Content.Load<Texture2D>("plattform");
            tileSheet = Content.Load<Texture2D>("32tilesheet");
            spriteFont = Content.Load<SpriteFont>("spriteFont");
            crosshairTex = Content.Load<Texture2D>("crosshair");
            healthBarTex = Content.Load<Texture2D>("healthbartex");
            rifleTex = Content.Load<Texture2D>("rifleTex");
            handgunTex = Content.Load<Texture2D>("handgunTex");
            unarmedTex = Content.Load<Texture2D>("unarmedTex");
            dialoghitbox = Content.Load<Texture2D>("dialoghitbox");
            friendlyProTex = Content.Load<Texture2D>("fpro");
            Viewport view = GraphicsDevice.Viewport;
            camera = new Camera(view);
            gameState = GameStates.start;
            gameBoard = new GameBoard(screenWidth, screenHeight);
            hud = new HUD(gameBoard.GetPlayer());
        }
        protected override void UnloadContent()
        {
            ScreenManager.Instance.UnLoadContent();         
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyMouseReader.Update();
            ScreenManager.Instance.Update(gameTime);
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

            target_rect = new Rectangle((int)target.X, (int)target.Y, 1, 1);
            dialoghitbox_rect = new Rectangle(40, 30, 100, 100);
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
            spriteBatch.Begin();

            if (gameState == GameStates.gameOn)
            {
                target = Vector2.Transform(new Vector2(KeyMouseReader.mousePosition.X, KeyMouseReader.mousePosition.Y), Matrix.Invert(camera.GetTransform()));
                int crosshairWidth = 32;
                int crosshairHeight = 32;
                crosshairPos = new Vector2(target.X - crosshairWidth / 2, target.Y - crosshairHeight / 2);

                //This spriteBatch implies everything in the batch is centered on the player-plane.
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetTransform());
                gameBoard.Draw(spriteBatch, square);

                foreach (Pickup pickUp in gameBoard.GetPickUpList())
                {
                    if (pickUp.GetIsInteractable())
                    {
                        spriteBatch.DrawString(spriteFont, "E", new Vector2(pickUp.GetPosition().X + pickUp.GetTexWidth() / 2, pickUp.GetPosition().Y - 10), Color.Black);
                    }
                }
                spriteBatch.Draw(dialoghitbox, dialoghitbox_rect, Color.Red);
                spriteBatch.Draw(crosshairTex, new Rectangle((int)crosshairPos.X, (int)crosshairPos.Y, crosshairWidth, crosshairHeight), Color.White);

                if (target_rect.Intersects(dialoghitbox_rect))
                {
                    spriteBatch.DrawString(spriteFont, "HEY THERE!", new Vector2(50, 50), Color.Black);

                }
                spriteBatch.End();
                spriteBatch.Begin();
                hud.Draw(gameBoard.GetPlayer());
            }
            if (gameState == GameStates.start)
            {
                ScreenManager.Instance.Draw(spriteBatch);
                spriteBatch.DrawString(spriteFont, "PRESS ENTER", new Vector2(screenWidth / 2 - 100, screenHeight / 2), Color.Purple);
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
