using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace CyberShooter
{
    public enum GameStates { splash, startMenu, loadingLevel, gameOn, gameOver, levelWon };
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public GameStates currentGameState;
        public static Texture2D square, crosshairTex,tileSheet, healthBarTex, handgunTex, rifleTex, unarmedTex, friendlyProTex,enemyProTex, charTex, crateTex,
            lvl1Screen, ammoTex, healthTex, greenProTex, yellowProTex, redProTex, gameOverScreen, lvl1Won;
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
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
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
            tileSheet = Content.Load<Texture2D>("theSprite");
            spriteFont = Content.Load<SpriteFont>("spriteFont");
            crosshairTex = Content.Load<Texture2D>("crosshair");
            healthBarTex = Content.Load<Texture2D>("healthbartex");
            rifleTex = Content.Load<Texture2D>("rifleTex");
            charTex = Content.Load<Texture2D>("Texture_Pack_Characters");
            healthTex = Content.Load<Texture2D>("health");
            ammoTex = Content.Load<Texture2D>("ammo");
            handgunTex = Content.Load<Texture2D>("handgunTex");
            unarmedTex = Content.Load<Texture2D>("unarmedTex");
            lvl1Screen = Content.Load<Texture2D>("lvl1screen");
            crateTex = Content.Load<Texture2D>("crate");
            friendlyProTex = Content.Load<Texture2D>("fpro");
            clickedSound = Content.Load<SoundEffect>("threeTone2");
            yellowProTex = Content.Load<Texture2D>("yellowPro");
            redProTex = Content.Load<Texture2D>("redPro");
            greenProTex = Content.Load<Texture2D>("greenPro");
            gameOverScreen = Content.Load<Texture2D>("gameOverScreen");
            lvl1Won = Content.Load<Texture2D>("Level1won");
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
                    startMenu.newGame.clicked = false;
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
            if (gameBoard.Player.levelFinish)
                currentGameState = GameStates.levelWon;

            if (currentGameState == GameStates.levelWon && KeyMouseReader.KeyPressed(Keys.Enter))
                Reload();

            if (currentGameState == GameStates.gameOn)
            {
                gameBoard.Update(gameTime, target);
            }

            if(currentGameState == GameStates.gameOver && KeyMouseReader.KeyPressed(Keys.Enter))
                Reload();
        }
        private void Reload()
        {
            currentGameState = GameStates.startMenu;
            startMenu = new StartMenu(Content);
            gameBoard = new GameBoard(screenWidth, screenHeight);
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
                spriteBatch.Draw(lvl1Screen, new Vector2(1920/2-400, 1080/2-300), Color.White);
            if (currentGameState == GameStates.startMenu)
            {
               startMenu.Draw();
            }
            if(currentGameState == GameStates.levelWon)
            {
                spriteBatch.Draw(lvl1Won, new Vector2(1920 / 2 - 400, 1080 / 2 - 300), Color.White);
            }
            if (currentGameState == GameStates.gameOver)
            {
                spriteBatch.Draw(gameOverScreen, new Vector2(1920 / 2 - 400, 1080 / 2 - 300), Color.White);
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
