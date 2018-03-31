using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberShooter
{
    class GameBoard
    {
        public Player player;
        public NPC testNPC;
        int screenWidth, screenHeight;

        public static Map map;

        public static int mapHeight = 20;
        public static int mapWidth = 20;
        public static int tileHeight = 32;
        public static int tileWidth = 32;
        public static Vector2 drawOffset = Vector2.Zero;
        public static int drawableLayer = 0;
        string loadFileName = "testest.txt";
        Vector2 collisionDist = Vector2.Zero;
        Vector2 normal;

        public GameBoard(int screenWidth, int screenHeight)
        {
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            player = new Player(new Vector2(100, 200));
            //gun = new Gun();
            testNPC = new NPC(new Vector2(100,100));
            map = new Map(mapWidth, mapHeight, tileWidth, tileHeight);
            map.LoadMap(loadFileName);
            mapHeight = map.mapHeight;
            mapWidth = map.mapWidth;
            tileHeight = map.tileHeight;
            tileWidth = map.tileWidth;

            map = new Map(mapWidth, mapHeight, tileWidth, tileHeight);

            map.LoadMap(loadFileName);

            map.LoadTileSet(Game1.tileSheet);
            map.PopulateCollisionLayer();
        }
        public void Update(GameTime gameTime, Vector2 target)
        {
            player.Update(gameTime, target);
            testNPC.Update();
            NPCCollision();
            for (int i = 0; i < map.collisionRects.Count(); i++)
            {
                if (player.hitRect.Intersects(map.collisionRects[i]))
                {
                    player.position = player.oldPosition;
                    player.speed = new Vector2(0, 0);
                }
            }
        }
        public void NPCCollision()
        {
            if(player.hitRect.Intersects(testNPC.hitRect))
            {
                player.position = player.oldPosition;
                player.speed = new Vector2(0,0);
            }
        }
        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            map.DrawMap();
            player.Draw(spriteBatch, texture);
            foreach(Projectile projectile in player.projectileList)
            {
                projectile.Draw(spriteBatch, texture);
            }
            testNPC.Draw(spriteBatch, texture);
        }
    }
}
