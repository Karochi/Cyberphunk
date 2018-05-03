using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace CyberShooter
{
    public enum GameStates { splash, startMenu, loadingLevel, gameOn, gameOver };
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public GameStates currentGameState;
        public static Texture2D square, crosshairTex,tileSheet, healthBarTex, handgunTex, rifleTex, unarmedTex, friendlyProTex,enemyProTex, charTex, crateTex,
            lvl1Screen;
        public static SpriteFont spriteFont;
        Vector2 target, crosshairPos;
        Camera camera;
        HUD hud; 
        int screenWidth, screenHeight, crosshairWidth, crosshairHeight;
        GameBoard gameBoard;
        StartMenu startMenu;
        SoundEffect clickedSound;
        Song gameSong;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            screenWidth = (int)ScreenManager.Instance.dimensions.X;
            screenHeight = (int)ScreenManager.Instance.dimensions.Y;
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
            startMenu = new StartMenu(Content);
            ScreenManager.Instance.graphicsDevice = GraphicsDevice;
            ScreenManager.Instance.spriteBatch = spriteBatch;
            ScreenManager.Instance.LoadContent(Content);
            square = Content.Load<Texture2D>("plattform");
            tileSheet = Content.Load<Texture2D>("useLvl1sheet");
            spriteFont = Content.Load<SpriteFont>("spriteFont");
            crosshairTex = Content.Load<Texture2D>("crosshair");
            healthBarTex = Content.Load<Texture2D>("healthbartex");
            rifleTex = Content.Load<Texture2D>("rifleTex");
            charTex = Content.Load<Texture2D>("Texture_Pack_Characters");
            handgunTex = Content.Load<Texture2D>("handgunTex");
            unarmedTex = Content.Load<Texture2D>("unarmedTex");
            lvl1Screen = Content.Load<Texture2D>("lvl1screen");
            crateTex = Content.Load<Texture2D>("crate");
            friendlyProTex = Content.Load<Texture2D>("fpro");
            clickedSound = Content.Load<SoundEffect>("threeTone2");
            gameSong = Content.Load<Song>("Gesaffelstein - Viol");
            Viewport view = GraphicsDevice.Viewport;
            camera = new Camera(view);
            currentGameState = GameStates.splash;
            gameBoard = new GameBoard(screenWidth, screenHeight);
            hud = new HUD(gameBoard.Player);      
        }
        protected override void UnloadContent()
        {
            ScreenManager.Instance.UnLoadContent();         
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)|| startMenu.quitGame.clicked)
                Exit();
            KeyMouseReader.Update();
            ScreenManager.Instance.Update(gameTime);
            GameStateUpdate(gameTime);
            CameraUpdate();
            hud.Update(gameBoard.Player);
            base.Update(gameTime);
        }
        protected void GameStateUpdate(GameTime gameTime)
        {
            if (currentGameState == GameStates.splash)
            {
                if (KeyMouseReader.KeyPressed(Keys.Enter))
                {
                    clickedSound.Play();
                    currentGameState = GameStates.startMenu;
                }
            }
            if(currentGameState == GameStates.startMenu)
            {
                startMenu.Update(gameTime, currentGameState);

                if (startMenu.newGame.clicked)
                {
                    currentGameState = GameStates.loadingLevel;
                }
            }
            if(currentGameState == GameStates.loadingLevel)
            {
                if(KeyMouseReader.KeyPressed(Keys.Enter))
                {
                    clickedSound.Play();
                    currentGameState = GameStates.gameOn;
                    MediaPlayer.Play(gameSong);
                }
            }
            if (gameBoard.Player.IsDead)
            {
                currentGameState = GameStates.gameOver;
            }
            if (currentGameState == GameStates.gameOn)
            {
                gameBoard.Update(gameTime, target);
            }
        }
        protected void CameraUpdate()
        {
            if (currentGameState == GameStates.gameOn)
            {
                camera.SetPosition(gameBoard.Player.Position);
                camera.GetPosition();
            }
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            if (currentGameState == GameStates.gameOn)
            {
                Crosshair();
                spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetTransform());
                gameBoard.Draw(spriteBatch, square);
                spriteBatch.Draw(crosshairTex, new Rectangle((int)crosshairPos.X, (int)crosshairPos.Y, crosshairWidth, crosshairHeight), Color.White);
                spriteBatch.End();
                spriteBatch.Begin();
                hud.Draw(gameBoard.Player);
            }
            if (currentGameState == GameStates.splash)
            {
                ScreenManager.Instance.Draw(spriteBatch);
            }
            if (currentGameState == GameStates.loadingLevel)
                spriteBatch.Draw(lvl1Screen, Vector2.Zero, Color.White);
            if (currentGameState == GameStates.startMenu)
            {
               startMenu.Draw();
            }
            if (currentGameState == GameStates.gameOver)
            {
                spriteBatch.DrawString(spriteFont, "GAME OVER", new Vector2(screenWidth / 2, screenHeight / 2), Color.Red);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        private void Crosshair()
        {
            target = Vector2.Transform(new Vector2(KeyMouseReader.mousePosition.X, KeyMouseReader.mousePosition.Y), Matrix.Invert(camera.GetTransform()));
            crosshairWidth = 32;
            crosshairHeight = 32;
            crosshairPos = new Vector2(target.X - crosshairWidth / 2, target.Y - crosshairHeight / 2);
            spriteBatch.End();
        }
    }
}
