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
            SetPosition(position);
            distanceX = newDistanceX;
            distanceY = newDistanceY;
            SetTexHeight(40);
            SetTexWidth(30);

            oldDistanceX = distanceX;
            oldDistanceY = distanceY;
        }
        public override void Update()
        {
            hostile = true;

            if (hostile == true)
                HostileMove();


            base.Update();
        }
        public void GetPlayerDistance(Player p)
        {
            playerDistanceX = p.GetPosition().X - GetPosition().X;
            playerDistanceY = p.GetPosition().Y - GetPosition().Y;
        }

        public void HostileMove()
        {
            if (distanceX >= 0)
            {
                right = true;
                //speed.X = 1;
                SetSpeed(new Vector2(1, GetSpeed().Y));
            }
            else if (distanceX <= oldDistanceX)
            {
                right = false;
                //speed.X = -1f;
                SetSpeed(new Vector2(-1f, GetSpeed().Y));
            }

            if (distanceY >= 0)
            {
                up = true;
                //speed.Y = 1f;
                SetSpeed(new Vector2(GetSpeed().X, 1f));
            }
            if (distanceY <= oldDistanceY)
            {
                up = false;
                //speed.Y = -1f;
                SetSpeed(new Vector2(GetSpeed().X, -1f));
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
                    SetSpeed(new Vector2(-1f, GetSpeed().Y));
                else if (playerDistanceX > 1)
                    SetSpeed(new Vector2(1f, GetSpeed().Y));
                else if (playerDistanceX == 0)
                    SetSpeed(new Vector2(0, GetSpeed().Y));
            }

            if (playerDistanceY >= -200 && playerDistanceY <= 200)
            {
                if (playerDistanceY < -1)
                    SetSpeed(new Vector2(GetSpeed().X, -1f));
                else if (playerDistanceY > 1)
                    SetSpeed(new Vector2(GetSpeed().X, 1f));
                else if (playerDistanceY == 0)
                    SetSpeed(new Vector2(GetSpeed().X, 0));
            }
        }
    }
}
