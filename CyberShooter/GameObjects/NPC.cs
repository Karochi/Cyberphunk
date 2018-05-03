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
        Vector2 direction;
        ResourcePickup pickup;
        public int DirectionX, DirectionY, MaxDirectionX, MaxDirectionY, MinDirectionX, MinDirectionY;
        public int Radius, Range, Damage;
        int projectileSpeed;
        public float Velocity, MovementCooldown, DirectionChangeCooldown, ShootingCooldown;
        bool hostile, hasDropped;

        public NPC(Vector2 position, bool hostile) : base()
        {
            Position = position;
            TexWidth = 16;
            TexHeight = 20;
            MaxHealth = 50;
            CurrHealth = MaxHealth;

            Radius = 220;
            projectileSpeed = 1;
            Range = 200;
            Damage = 1;

            MinDirectionX = -180;
            MaxDirectionX = 180;
            MinDirectionY = -180;
            MaxDirectionY = 180;
            this.hostile = hostile;
        }
        public void Update(GameTime gameTime, Player player, List<Rectangle> collisionRects)
        {
            MovementCooldown -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            DirectionChangeCooldown -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            ShootingCooldown -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if(DamageCooldown <= 0)
            {
                IsDamaged = false;
            }
            Movement();
            Shooting(player);
            ProjectilePlayerCollision(player);
            CollisionReset();
            base.Update(gameTime, collisionRects);
        }
        public void NormalizeDirection()
        {
            direction = new Vector2(DirectionX, DirectionY);
            Vector2.Normalize(ref direction, out direction);
        }
        public void DropCheck(int lootRoll, Player player, List<ResourcePickup> resourcePickupList)
        {
            if (!hasDropped)
            {
                if(lootRoll <= (100 - player.handgunAmmo))
                {
                    pickup = new ResourcePickup(Position, PickupTypes.handgunAmmo);
                    resourcePickupList.Add(pickup);
                }
                if(lootRoll <= (100 - player.rifleAmmo))
                {
                    pickup = new ResourcePickup(Position, PickupTypes.rifleAmmo);
                    resourcePickupList.Add(pickup);
                }
                if(lootRoll <= ((player.MaxHealth - player.CurrHealth) * 10))
                {
                    pickup = new ResourcePickup(Position, PickupTypes.health);
                    resourcePickupList.Add(pickup);
                }
                hasDropped = true;
            }
        }
        private void Movement()
        {
            Speed = direction * Velocity;
            MovementUpdate();
            if (MovementCooldown <= 0)
            {
                Speed = Vector2.Zero;
            }
        }
        public void Shooting(Player Player)
        {
            if (Vector2.Distance(Player.Position, Position) <= Radius && ShootingCooldown <= 0)
            {
                Projectile projectile = new Projectile(Position, Player.GetPlayerCenter(), Damage, Range, projectileSpeed);
                ShootingCooldown = 1000;
                ProjectileList.Add(projectile);
            }
        }
        public void CollisionCheck(Rectangle collisionRect)
        {
            if (topRect.Intersects(collisionRect))
                MinDirectionY = 0;
            if (bottomRect.Intersects(collisionRect))
                MaxDirectionY = 0;
            if (leftRect.Intersects(collisionRect))
                MinDirectionX = 0;
            if (rightRect.Intersects(collisionRect))
                MaxDirectionX = 0;
        }
        public void CollisionReset()
        {
            MinDirectionX = -180;
            MaxDirectionX = 180;
            MinDirectionY = -180;
            MaxDirectionY = 180;
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
