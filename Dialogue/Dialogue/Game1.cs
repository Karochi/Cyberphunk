using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Dialogue
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int screenWidth, screenHeight;
        SpriteFont spriteFont;
        int texWidth = 40;
        int texHeight = 30;
        public Texture2D DialogueBox, NPC, Player, dialoghitbox;
        Rectangle Player_rect, dialoghitbox_rect, heytherehitbox_rect;
        Vector2 PlayerPosition, NPCPosition;
        Vector2 NPCTextPosition;
        float speed = 0.8f;
        
        private void DrawText()
        {
            StreamReader file = new StreamReader("Content/textFile.txt");
            string line1 = File.ReadLines("Content/textFile.txt").ElementAtOrDefault(0);
            spriteBatch.DrawString(spriteFont, line1, new Vector2 (NPCPosition.X-10,NPCPosition.Y-70), Color.Black);
            
        }
        private void DrawText2()
        {
            StreamReader file = new StreamReader("Content/textFile.txt");
            string line2 = File.ReadLines("Content/textFile.txt").ElementAtOrDefault(1);
            spriteBatch.DrawString(spriteFont, line2, new Vector2(NPCPosition.X - 10, NPCPosition.Y - 55), Color.Black);


        }

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
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            IsMouseVisible = true;
            spriteFont = Content.Load<SpriteFont>("dialogue");
            NPC = Content.Load<Texture2D>("plattform");
            Player = Content.Load<Texture2D>("plattform");
            dialoghitbox = Content.Load<Texture2D>("dialoghitbox");
            PlayerPosition = new Vector2(25, 250);
            NPCPosition = new Vector2(100, 150);
            NPCTextPosition = new Vector2(100, 100);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState key = Keyboard.GetState();
        
            if (key.IsKeyDown(Keys.W))
            {
                PlayerPosition.Y -= speed;
            }
            if (key.IsKeyDown(Keys.S))
            {
                PlayerPosition.Y += speed;
            }
            if (key.IsKeyDown(Keys.D))
            {
                PlayerPosition.X += speed;
            }
            if (key.IsKeyDown(Keys.A))
            {
                PlayerPosition.X -= speed;
            }
            
            Player_rect = new Rectangle((int)PlayerPosition.X, (int)PlayerPosition.Y, texWidth / 2, texHeight);
            dialoghitbox_rect = new Rectangle(90, 150, 70, 50);
            heytherehitbox_rect = new Rectangle(50, 200, 150, 100);
            // TODO: Add your update logic here
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            MouseState MouseState = Mouse.GetState();
            KeyboardState key = Keyboard.GetState();
            
            spriteBatch.Begin();
            spriteBatch.Draw(NPC, new Rectangle((int)NPCPosition.X,(int)NPCPosition.Y, texWidth, texHeight), Color.White);

            spriteBatch.Draw(dialoghitbox, heytherehitbox_rect, Color.Transparent);
            spriteBatch.Draw(Player, Player_rect, Color.Black);
            
            if (key.IsKeyDown(Keys.Space))
            {
                spriteBatch.Draw(dialoghitbox, heytherehitbox_rect, Color.Red);
                spriteBatch.Draw(dialoghitbox, dialoghitbox_rect, Color.Black);
            }
            if (Player_rect.Intersects(heytherehitbox_rect))
            {
                DrawText();
            }
            
            if (Player_rect.Intersects(dialoghitbox_rect))
            {
                spriteBatch.DrawString(spriteFont, "Hold E to Talk",new Vector2(PlayerPosition.X+30,PlayerPosition.Y), Color.Black);
            }

            if (Player_rect.Intersects(dialoghitbox_rect) && key.IsKeyDown(Keys.E))
            {
                DrawText2();
            }

            spriteBatch.End();
            
            
            base.Draw(gameTime);
        }
    }
}
