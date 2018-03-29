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
        public Gun gun;
        public Player player;
        public NPC testNPC;
        //public Level map;
        int screenWidth, screenHeight;

        Vector2 normal;
        Vector2 collisionDist = Vector2.Zero;

        public GameBoard(int screenWidth, int screenHeight)
        {
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            player = new Player(screenWidth, screenHeight);
            gun = new Gun();
            testNPC = new NPC(new Vector2(100,100));

            //map = new Level();
            //map.LoadMap("Content/Maps/test.txt");
            //map.LoadTileSet(Game1.tileSheet);
            //map.PopulateCollisionLayer();

        }
        public void Update(GameTime gameTime)
        {
            player.Update();

            //collisionDist = Vector2.Zero;
            testNPC.Update();
            NPCCollision();
            if (player.weaponState == WeaponStates.gun)
            {
                gun.Update(gameTime, player.position);
                gun.target = new Vector2(KeyMouseReader.mousePosition.X + (player.position.X - screenWidth / 2), KeyMouseReader.mousePosition.Y + (player.position.Y - screenHeight / 2));
            }
            //for (int i = 0; i < map.collisonRects.Count(); i++)
            //{
            //    if(IsColliding(player.hitRect, map.collisonRects[i]))
            //    {
            //        if(normal.Length() > collisionDist.Length())
            //        {
            //            collisionDist = normal;
            //        }
            //    }
            //}
            //player.position.X += collisionDist.X;
            //player.position.X += collisionDist.Y;

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
            player.Draw(spriteBatch, Game1.square);
            gun.Draw(spriteBatch, texture);
            testNPC.Draw(spriteBatch, Game1.square);
        }
        public bool IsColliding(Rectangle body1, Rectangle body2)
        {
            normal = Vector2.Zero;

            Vector2 body1Centre = new Vector2(body1.X + (body1.Width / 2), body1.Y + (body1.Height / 2));
            Vector2 body2Centre = new Vector2(body2.X + (body2.Width / 2), body2.Y + (body2.Height / 2));

            Vector2 distance, absDistance;

            float xMag, yMag;

            distance = body1Centre - body2Centre;

            float xAdd = ((body1.Width) + (body2.Width)) / 2.0f;
            float yAdd = ((body1.Height) + (body2.Height)) / 2.0f;

            absDistance.X = (distance.X < 0) ? -distance.X : distance.X;
            absDistance.Y = (distance.Y < 0) ? -distance.Y : distance.Y;

            if (!((absDistance.X < xAdd) && (absDistance.Y < yAdd)))
                return false;

            xMag = xAdd - absDistance.X;
            yMag = yAdd - absDistance.Y;

            if (xMag < yMag)
            {
                normal.X = (distance.X > 0) ? xMag : -xMag;
            }
            else
            {
                normal.Y = (distance.Y > 0) ? yMag : yMag;
            }
            return true;
        }
    }
}
