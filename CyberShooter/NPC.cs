using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberShooter
{
    public class NPC : MovingGameObject
    {
        Vector2 playerPos, direction, stop;
        int directionX, directionY, maxDirectionX, maxDirectionY, minDirectionX, minDirectionY;
        float velocity, retreatDistance, stoppingDistance;
        float movementCooldown, directionChangeCooldown;
        bool hostile;
        Rectangle leftRect, rightRect, topRect, bottomRect;

        public void SetDirectionX(int directionX)
        {
            this.directionX = directionX;
        }
        public void SetDirectionY(int directionY)
        {
            this.directionY = directionY;
        }
        public float GetDirectionChangeCooldown()
        {
            return directionChangeCooldown;
        }
        public void SetDirectionChangeCooldown(float directionChangeCooldown)
        {
            this.directionChangeCooldown = directionChangeCooldown;
        }
        public void SetMovementCooldown(float movementCooldown)
        {
            this.movementCooldown = movementCooldown;
        }
        public void SetVelocity(float velocity)
        {
            this.velocity = velocity;
        }
        public int GetMaxDirectionX()
        {
            return maxDirectionX;
        }
        public int GetMaxDirectionY()
        {
            return maxDirectionY;
        }
        public int GetMinDirectionX()
        {
            return minDirectionX;
        }
        public int GetMinDirectionY()
        {
            return minDirectionY;
        }
        public NPC(Vector2 position, bool hostile) : base()
        {
            SetPosition(position);
            SetTexHeight(40);
            SetTexWidth(30);
            SetHealth(50);
            stoppingDistance = 220;
            retreatDistance = 150;
            stop = Vector2.Zero;
            this.hostile = hostile;

            minDirectionX = -180;
            maxDirectionX = 180;
            minDirectionY = -180;
            maxDirectionY = 180;
        }
        public void Update(GameTime gameTime)
        {
            movementCooldown -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            directionChangeCooldown -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            Movement();

            leftRect = new Rectangle((int)GetPosition().X - 10, (int)GetPosition().Y, 10, GetTexHeight());
            rightRect = new Rectangle((int)GetPosition().X + GetTexWidth(), (int)GetPosition().Y, 10, GetTexHeight());
            topRect = new Rectangle((int)GetPosition().X, (int)GetPosition().Y - 10, GetTexWidth(), 10);
            bottomRect = new Rectangle((int)GetPosition().X, (int)GetPosition().Y + GetTexHeight(), GetTexWidth(), 10);

            base.Update();
        }
        public void NormalizeDirection()
        {
            direction = new Vector2(directionX, directionY);
            Vector2.Normalize(ref direction, out direction);
        }
        private void Movement()
        {
            SetSpeed(direction * velocity);
            if (movementCooldown <= 0)
            {
                SetSpeed(stop);
            }
            //    if (Vector2.Distance(GetPosition(), playerPos) > stoppingDistance)
            //    {
            //        direction = playerPos - GetPosition();
            //        velocity = 0.01f;
            //        SetSpeed(direction * velocity);
            //    }
            //    else if (Vector2.Distance(GetPosition(), playerPos) < stoppingDistance && Vector2.Distance(GetPosition(), playerPos) > retreatDistance)
            //    {
            //        SetSpeed(stop);
            //    }
            //    else if (Vector2.Distance(GetPosition(), playerPos) < retreatDistance)
            //    {
            //        direction = playerPos - GetPosition();
            //        velocity = -0.02f;
            //        SetSpeed(direction * velocity);
            //    }

        }
        public void GetPlayerPos(Player p)
        {
            playerPos = p.GetPosition();
        }
        public void CollisionCheck(Rectangle collisionRect)
        {
            if (topRect.Intersects(collisionRect))
            {
                //velocity = 0f;
                //movementCooldown = 10;
                minDirectionY = 0;
            }
            if (bottomRect.Intersects(collisionRect))
            {
                //velocity = 0f;
                maxDirectionY = 0;
            }
            if (leftRect.Intersects(collisionRect))
            {
                //velocity = 0f;
                minDirectionX = 0;
            }
            if (rightRect.Intersects(collisionRect))
            {
                //velocity = 0f;
                maxDirectionX = 0;
            }
            else
            {
                minDirectionX = -180;
                maxDirectionX = 180;
                minDirectionY = -180;
                maxDirectionY = 180;
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
