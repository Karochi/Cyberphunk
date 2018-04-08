using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MapEditor
{
    public enum State
    {
        PLAY,
        FREEZE
    }

    public class Game1 : Game
    {
        public static State state = State.PLAY;
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static Vector2 clientBounds;

        public static bool quit = false;
        public static Vector2 drawOffset = Vector2.Zero;

        public static string mapName;
        public static string fileName;
        public static string loadFileName;
        public static string tileSheetFileName;

        public static int drawableLayer = 0;

        public static Map map;

        public static int mapHeight = 15;
        public static int mapWidth = 20;
        public static int tileHeight = 32;
        public static int tileWidth = 32;

        public static int selectedTileNumb = 0;

        public static Texture2D tileSheet;
        public static Texture2D solid;
        public static Texture2D humanTex;

        Texture2D pixel;

        MouseState curState;
        KeyboardState prevState;

        SpriteFont basic;

        HUD hud;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            map = new Map(mapWidth, mapHeight, tileWidth, tileHeight);
        }
        protected override void Initialize()
        {
            IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = 700;
            graphics.PreferredBackBufferWidth = 800;
            graphics.ApplyChanges();

            clientBounds = new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height);

            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            humanTex = Content.Load<Texture2D>("humanTex");
            solid = Content.Load<Texture2D>("plattform");
            pixel = Content.Load<Texture2D>("plattform");

            basic = Content.Load<SpriteFont>("Basic");

            hud = new HUD(Content);
        }
        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            if (quit)
                Exit();

            Window.Title = "Map Editor - " + mapName;

            KeyboardState keyState = Keyboard.GetState();
            if(selectedTileNumb < map.tileSet.Count - 1)
            {
                if (keyState.IsKeyDown(Keys.Up) && !prevState.IsKeyDown(Keys.Down))
                    selectedTileNumb++;
            }
            if(selectedTileNumb > 0)
            {
                if (keyState.IsKeyDown(Keys.Down) && !prevState.IsKeyDown(Keys.Down))
                    selectedTileNumb--;
            }

            if (state == State.PLAY && tileSheet != null)
            {
                if (drawableLayer == 0)
                    map.tileLayer1.SetTiles(selectedTileNumb + 1);
                else if (drawableLayer == 1)
                    map.tileLayer2.SetTiles(selectedTileNumb + 1);
                else if (drawableLayer == 2)
                    map.solidLayer.SetTiles(1);
                else if (drawableLayer == 3)
                    map.hostileHumanLayer.SetTiles(1);
                else if (drawableLayer == 4)
                    map.friendlyHumanLayer.SetTiles(1);
            }

            curState = Mouse.GetState();
            prevState = keyState;

            map.UpdateUserInput();

            hud.Update();

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(pixel, new Rectangle(-(int)drawOffset.X * map.tileWidth, -(int)drawOffset.Y * map.tileHeight, map.mapWidth * map.tileWidth, map.mapHeight * map.tileHeight), Color.Black);

            map.DrawMap();
            hud.Draw();

            if (tileSheet != null && drawableLayer < 2)
                spriteBatch.Draw(tileSheet, new Vector2(curState.X - tileWidth / 2, curState.Y - tileHeight / 2), map.tileSet[selectedTileNumb], Color.White);

            string layerText = "";

            if (drawableLayer == 0)
                layerText = "Layer 1";
            else if (drawableLayer == 1)
                layerText = "Layer 2";
            else if (drawableLayer == 2)
                layerText = "Collision Layer";
            else if (drawableLayer == 3)
                layerText = "Hostile Human";
            else if (drawableLayer == 4)
                layerText = "Friendly Human";

            spriteBatch.DrawString(basic, layerText, new Vector2(5, 5), Color.White);

            base.Draw(gameTime);

            spriteBatch.End();
        }
    }
}
