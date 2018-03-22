using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Cyberphunk
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Can be moved to an 'initialize' class
        Texture2D square;
        Camera camera;
        int screenWidth, screenHeight;
        //<\>
        //To be moved to a GameBoard or LevelSpawn Class
        GameState gameState;
        Gun gun;
        Player player;
        //<\>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //This decides the screen width and height.
            screenWidth = 800;
            screenHeight = 600;
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            //<\>
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
            Viewport view = GraphicsDevice.Viewport;
            camera = new Camera(view);
            //To be moved to a GameBoard or LevelSpawn Class.
            gameState = new GameState();
            player = new Player(new Vector2(0, 0));
            gun = new Gun(player.position);
            //<\>
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyMouseReader.Update();
            //To be moved to a GameBoard or LevelSpawn Class
            player.Update();
            gun.Update(gameTime);
            gun.position = player.position;
            //This messy target system is needed to recalibrate the aiming since the camera puts the player on a different plane.
            gun.target = new Vector2(KeyMouseReader.mousePosition.X + (player.position.X - screenWidth/2), KeyMouseReader.mousePosition.Y + (player.position.Y - screenHeight/2));
            //<\>
            //This is my primary means of testing calibration without a background.
            if (KeyMouseReader.LeftClick())
            {
                Console.WriteLine("target location"+gun.target);
                Console.WriteLine("position"+player.position);
            }
            CameraUpdate();
            base.Update(gameTime);
        }
        protected void CameraUpdate()
        {
            if (gameState.gameState == GameStates.start)
            {
                camera.SetPosition(player.position);
                camera.GetPosition();
            }
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //This spriteBatch implies everything in the batch is centered on the player-plane.
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetTransform());
            //to be moved to GameBoard or LevelSpawn Class
            player.Draw(spriteBatch, square);
            gun.Draw(spriteBatch, square);
            //<\>
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
