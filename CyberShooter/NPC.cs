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
        public float stoppingDistance;
        public float retreatDistance;
        Vector2 playerPos, direction, stop;
        float velocity;
        bool hostile;

        public NPC(Vector2 position) : base()
        {
            SetPosition(position);
            SetTexHeight(40);
            SetTexWidth(30);
            stoppingDistance = 220;
            retreatDistance = 150;
            stop = Vector2.Zero;
            hostile = true;
        }
        public override void Update()
        {
            if (hostile)
                HostileMove();

            base.Update();
        }
        public void HostileMove()
        {
            if (Vector2.Distance(GetPosition(), playerPos) > stoppingDistance)
            {
                direction = playerPos - GetPosition();
                velocity = 0.02f;
                SetSpeed(direction * velocity);
            }
            else if (Vector2.Distance(GetPosition(), playerPos) < stoppingDistance && Vector2.Distance(GetPosition(), playerPos) > retreatDistance)
            {
                SetSpeed(stop);
            }
            else if (Vector2.Distance(GetPosition(), playerPos) < retreatDistance)
            {
                direction = playerPos - GetPosition();
                velocity = -0.03f;
                SetSpeed(direction * velocity);
            }
        }
        public void GetPlayerPos(Player p)
        {
            playerPos = p.GetPosition();
        }
    }
}
