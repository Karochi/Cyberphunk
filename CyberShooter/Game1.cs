using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CyberShooter
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Can be moved to an 'initialize' class
        public static Texture2D square, tileSheet;
        Camera camera;
        int screenWidth, screenHeight;
        //<\>
        GameState gameState;
        GameBoard gameBoard;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            screenWidth = 1600;
            screenHeight = 900;
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            IsMouseVisible = true;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            square = Content.Load<Texture2D>("plattform");
            tileSheet = Content.Load<Texture2D>("roguelikeSheet_transparent");
            Viewport view = GraphicsDevice.Viewport;
            camera = new Camera(view);

            gameState = new GameState();
            gameBoard = new GameBoard(screenWidth, screenHeight);
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyMouseReader.Update();
            gameBoard.Update(gameTime);
            ActiveTesting();

            CameraUpdate();
            base.Update(gameTime);
        }
        protected void CameraUpdate()
        {
            if (gameState.gameState == GameStates.start)
            {
                camera.SetPosition(gameBoard.player.position);
                camera.GetPosition();
            }
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            //This spriteBatch implies everything in the batch is centered on the player-plane.
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetTransform());
            gameBoard.Draw(spriteBatch, square);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public void ActiveTesting()
        {
            if (KeyMouseReader.LeftClick())
            {
                Console.WriteLine("target location" + gameBoard.gun.target);
                Console.WriteLine("position" + gameBoard.player.position);
            }
            if (KeyMouseReader.RightClick())
            {
                gameBoard.player.weaponState = WeaponStates.gun;
            }
        }
    }
}
