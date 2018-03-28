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
        int screenWidth, screenHeight;

        public GameBoard(int screenWidth, int screenHeight)
        {
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            player = new Player(new Vector2(0, 0));
            gun = new Gun();
            testNPC = new NPC(new Vector2(100,100)); 
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
            player.Draw(spriteBatch, texture);
            gun.Draw(spriteBatch, texture);
            testNPC.Draw(spriteBatch, texture);
        }
    }
}
