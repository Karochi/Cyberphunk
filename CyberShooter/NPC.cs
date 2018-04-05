using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberShooter
{
    class NPC : MovingGameObject
    {
        Vector2 playerPos, direction, stop;
        float velocity, retreatDistance, stoppingDistance;
        bool hostile;
        Rectangle leftRect, rightRect, topRect, bottomRect;

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

            leftRect = new Rectangle((int)GetPosition().X - 10, (int)GetPosition().Y, 10, GetTexHeight());
            rightRect = new Rectangle((int)GetPosition().X + GetTexWidth(), (int)GetPosition().Y, 10, GetTexHeight());
            topRect = new Rectangle((int)GetPosition().X, (int)GetPosition().Y - 10, GetTexWidth(), 10);
            bottomRect = new Rectangle((int)GetPosition().X, (int)GetPosition().Y + GetTexHeight(), GetTexWidth(), 10);

            base.Update();
        }
        private void HostileMove()
        {
            if (Vector2.Distance(GetPosition(), playerPos) > stoppingDistance)
            {
                direction = playerPos - GetPosition();
                velocity = 0.01f;
                SetSpeed(direction * velocity);
            }
            else if (Vector2.Distance(GetPosition(), playerPos) < stoppingDistance && Vector2.Distance(GetPosition(), playerPos) > retreatDistance)
            {
                SetSpeed(stop);
            }
            else if (Vector2.Distance(GetPosition(), playerPos) < retreatDistance)
            {
                direction = playerPos - GetPosition();
                velocity = -0.02f;
                SetSpeed(direction * velocity);
            }
        }
        public void GetPlayerPos(Player p)
        {
            playerPos = p.GetPosition();
        }
        public void CollisionCheck(Rectangle collisionRect)
        {
            if (topRect.Intersects(collisionRect))
            {
                SetSpeed(new Vector2(0, 0));
            }
            else if (collisionRect.Intersects(bottomRect))
            {
                SetSpeed(new Vector2(0, 0));
            }
            if(collisionRect.Intersects(leftRect))
            {
                SetSpeed(new Vector2(0,0));
            }
            else if (collisionRect.Intersects(rightRect))
            {
                SetSpeed(new Vector2(0, 0));
            }
        }
        public override void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, leftRect, Color.Red);
            spriteBatch.Draw(texture, rightRect, Color.Red);
            spriteBatch.Draw(texture, topRect, Color.Red);
            spriteBatch.Draw(texture, bottomRect, Color.Red);

            base.Draw(spriteBatch, texture);
        }
    }
}
