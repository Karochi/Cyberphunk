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
        Vector2 playerPos, direction;
        int directionX, directionY, maxDirectionX, maxDirectionY, minDirectionX, minDirectionY, radius, range, damage, projectileSpeed;
        float velocity, retreatDistance, stoppingDistance;
        float movementCooldown, directionChangeCooldown, shootingCooldown;
        bool hostile;

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
        public int GetRadius()
        {
            return radius;
        }
        public int GetDamage()
        {
            return damage;
        }
        public int GetRange()
        {
            return range;
        }
        public int GetProjectileSpeed()
        {
            return projectileSpeed;
        }
        public float GetShootingCooldown()
        {
            return shootingCooldown;
        }
        public void SetShootingCooldown(float shootingCooldown)
        {
            this.shootingCooldown = shootingCooldown;
        }
        public NPC(Vector2 position, bool hostile) : base()
        {
            Position = position;
            TexWidth = 30;
            TexHeight = 40;
            MaxHealth = 50;
            CurrHealth = MaxHealth;

            radius = 220;
            projectileSpeed = 1;
            range = 200;
            damage = 1;

            stoppingDistance = 220;
            retreatDistance = 150;
            this.hostile = hostile;

            minDirectionX = -180;
            maxDirectionX = 180;
            minDirectionY = -180;
            maxDirectionY = 180;
        }
        public void Update(GameTime gameTime, Player player, List<Rectangle> collisionRects)
        {
            movementCooldown -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            directionChangeCooldown -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            shootingCooldown -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if(DamageCooldown <= 0)
            {
                IsDamaged = false;
            }
            Movement();
            Shooting(player);
            ProjectilePlayerCollision(player);
            base.Update(gameTime, collisionRects);
        }
        public void NormalizeDirection()
        {
            direction = new Vector2(directionX, directionY);
            Vector2.Normalize(ref direction, out direction);
        }
        private void Movement()
        {
            Speed = direction * velocity;
            MovementUpdate();
            if (movementCooldown <= 0)
            {
                Speed = Vector2.Zero;
            }
        }
        public void GetPlayerPos(Player p)
        {
            playerPos = p.Position;
        }
        public void Shooting(Player Player)
        {
            if (Vector2.Distance(Player.Position, Position) <= GetRadius() && GetShootingCooldown() <= 0)
            {
                Projectile projectile = new Projectile(Position, Player.GetPlayerCenter(), GetDamage(), GetRange(), GetProjectileSpeed());
                SetShootingCooldown(1000);
                ProjectileList.Add(projectile);
            }
        }
        public void CollisionCheck(Rectangle collisionRect)
        {
            if (topRect.Intersects(collisionRect))
            {
                minDirectionY = 0;
            }
            if (bottomRect.Intersects(collisionRect))
            {
                maxDirectionY = 0;
            }
            if (leftRect.Intersects(collisionRect))
            {
                minDirectionX = 0;
            }
            if (rightRect.Intersects(collisionRect))
            {
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
        public void ProjectilePlayerCollision(Player player)
        {
            foreach (Projectile projectile in ProjectileList)
            {
                if (projectile.HitRect.Intersects(player.HitRect))
                {
                    if (player.Damage())
                    {
                        player.CurrHealth = (player.CurrHealth - projectile.GetDamage());
                    }
                    ProjectileList.Remove(projectile);
                    return;
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            if (IsDead)
            {
                spriteBatch.Draw(texture, HitRect, Color.Gray);
            }
            else if (IsDamaged)
            {
                spriteBatch.Draw(texture, HitRect, Color.Red);
            }
            else
            {
                base.Draw(spriteBatch, texture);
            }
        }
    }
}
