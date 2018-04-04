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
            //IsMouseVisible = true;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            square = Content.Load<Texture2D>("plattform");
            tileSheet = Content.Load<Texture2D>("roguelikeSheet_transparent");
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
            gameBoard.Update(gameTime, target);

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
            int crosshairWidth = 32;
            int crosshairHeight = 32;
            crosshairPos = new Vector2(target.X - crosshairWidth/2, target.Y - crosshairHeight/2);

            //This spriteBatch implies everything in the batch is centered on the player-plane.
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetTransform());
            gameBoard.Draw(spriteBatch, square);

            foreach(Pickup pickUp in gameBoard.pickUpList)
            {
                if (pickUp.isInteractable)
                {
                    spriteBatch.DrawString(spriteFont, "E", new Vector2(pickUp.position.X + pickUp.texWidth / 2, pickUp.position.Y - 10), Color.Black);
                }
            }
            spriteBatch.Draw(crosshairTex, new Rectangle((int)crosshairPos.X, (int)crosshairPos.Y, crosshairWidth, crosshairHeight), Color.White);
            spriteBatch.End();
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, "firstWeapon: " + gameBoard.player.firstWeapon.weaponName, Vector2.Zero, Color.White);
            spriteBatch.DrawString(spriteFont, "secondWeapon: " + gameBoard.player.secondWeapon.weaponName, new Vector2(0,20), Color.White);
            spriteBatch.DrawString(spriteFont, "ammo: " + gameBoard.player.ammo, new Vector2(0, 40), Color.White);
            spriteBatch.DrawString(spriteFont, "pickUpListCount: " + gameBoard.pickUpList.Count, new Vector2(0, 60), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
