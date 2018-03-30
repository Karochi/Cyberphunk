﻿using Microsoft.Xna.Framework;
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
        public Gun gun;
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

        public GameBoard(int screenWidth, int screenHeight)
        {
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            player = new Player(new Vector2(100, 200));
            gun = new Gun();
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
        }
        public void Update(GameTime gameTime)
        {
            player.Update();
            testNPC.Update();
            NPCCollision();
            if (player.weaponState == WeaponStates.gun)
            {
                gun.Update(gameTime, player.position);
                gun.target = new Vector2(KeyMouseReader.mousePosition.X + (player.position.X - screenWidth / 2), KeyMouseReader.mousePosition.Y + (player.position.Y - screenHeight / 2));
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
            gun.Draw(spriteBatch, texture);
            testNPC.Draw(spriteBatch, texture);
        }
    }
}
