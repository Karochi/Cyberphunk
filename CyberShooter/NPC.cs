using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberShooter
{
    class NPC : MovingGameObject
    {
        Vector2 origin;
        bool hostile, right, up;
        float distanceX, distanceY, 
            oldDistanceX, oldDistanceY, playerDistanceX, playerDistanceY;


        public NPC(Vector2 position, float newDistanceX, float newDistanceY) : base()
        {
            this.position = position;
            distanceX = newDistanceX;
            distanceY = newDistanceY;
            texHeight = 40;
            texWidth = 30;

            oldDistanceX = distanceX;
            oldDistanceY = distanceY;
        }
        public override void Update()
        {
            hostile = true;

            if (hostile = true)
                HostileMove();


            base.Update();
        }
        public void GetPlayerDistance(Player p)
        {
            playerDistanceX = p.position.X - position.X;
            playerDistanceY = p.position.Y - position.Y;
        }

        public void HostileMove()
        {
            if (distanceX >= 0)
            {
                right = true;
                speed.X = 1;
            }
            else if (distanceX <= oldDistanceX)
            {
                right = false;
                speed.X = -1f;
            }

            if (distanceY >= 0)
            {
                up = true;
                speed.Y = 1f;
            }
            if (distanceY <= oldDistanceY)
            {
                up = false;
                speed.Y = -1f;
            }

            if (right)
                distanceX += 1;
            else
                distanceX -= 1;

            if (up)
                distanceY += 1;
            else
                distanceY -= 1;

            if (playerDistanceX >= -200 && playerDistanceX <= 200)
            {
                if (playerDistanceX < -1)
                    speed.X = -1f;
                else if (playerDistanceX > 1)
                    speed.X = 1f;
                else if (playerDistanceX == 0)
                    speed.X = 0f;
            }

            if (playerDistanceY >= -200 && playerDistanceY <= 200)
            {
                if (playerDistanceY < -1)
                    speed.Y = -1f;
                else if (playerDistanceY > 1)
                    speed.Y = 1f;
                else if (playerDistanceY == 0)
                    speed.Y = 0f;
            }
        }
    }
}
