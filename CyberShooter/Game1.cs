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

        enum GameStates { start, loadingLevel, gameOn, gameOver };
        GameStates gameState;
        //Can be moved to an 'initialize' class
        public static Texture2D square;
        public static Texture2D tileSheet;
        Vector2 target, crosshair;
        Camera camera;
        int screenWidth, screenHeight;
        //<\>
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
            IsMouseVisible = true;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            square = Content.Load<Texture2D>("plattform");
            tileSheet = Content.Load<Texture2D>("roguelikeSheet_transparent");

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
            gameBoard.Update(gameTime, target);
            ActiveTesting();

            CameraUpdate();
            base.Update(gameTime);
        }
        protected void CameraUpdate()
        {
            if (gameState == GameStates.start)
            {
                camera.SetPosition(gameBoard.player.position);
                camera.GetPosition();
            }
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            target = Vector2.Transform(new Vector2(KeyMouseReader.mousePosition.X, KeyMouseReader.mousePosition.Y), Matrix.Invert(camera.GetTransform()));
            crosshair = new Vector2(target.X - square.Width / 2, target.Y - square.Height / 2);
            //This spriteBatch implies everything in the batch is centered on the player-plane.
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetTransform());
            gameBoard.Draw(spriteBatch, square);
            spriteBatch.Draw(square, crosshair, Color.Black);
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
