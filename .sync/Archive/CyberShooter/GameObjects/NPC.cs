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
        enum Direction { Left, Up, Down, Right }
        Direction currentDirection;
        Rectangle nrDown = new Rectangle(0, 38 * 1, 30, 38);
        Rectangle nrUp = new Rectangle(0, 38 * 4, 30, 38);
        Rectangle nrLeft = new Rectangle(0, 38 * 2, 30, 38);
        Rectangle nrRight = new Rectangle(0, 38 * 3, 30, 38);
        Texture2D charTex;
        Vector2 direction;
        ResourcePickup pickup;
        public int DirectionX, DirectionY, MaxDirectionX, MaxDirectionY, MinDirectionX, MinDirectionY;
        public int Radius, Range, Damage;
        int projectileSpeed;
        public float Velocity, MovementCooldown, DirectionChangeCooldown, ShootingCooldown;
        bool hostile, hasDropped;

        public NPC(Vector2 position, bool hostile) : base()
        {
            charTex = Game1.charTex;
            Position = position;
            TexWidth = 30;
            TexHeight = 36;
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
            if (DamageCooldown <= 0)
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
                if (lootRoll <= ((player.MaxHealth - player.CurrHealth) * 10))
                {
                    pickup = new ResourcePickup(Position, PickupTypes.health);
                    resourcePickupList.Add(pickup);
                    hasDropped = true;
                    return;
                }
                if (lootRoll <= (100 - player.handgunAmmo))
                {
                    pickup = new ResourcePickup(Position, PickupTypes.handgunAmmo);
                    resourcePickupList.Add(pickup);
                    hasDropped = true;
                    return;
                }
                if(lootRoll <= (100 - player.rifleAmmo))
                {
                    pickup = new ResourcePickup(Position, PickupTypes.rifleAmmo);
                    resourcePickupList.Add(pickup);
                    hasDropped = true;
                    return;
                }
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
                Projectile projectile = new Projectile(Game1.redProTex, Position, Player.GetPlayerCenter(), Damage, Range, projectileSpeed);
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
                switch (currentDirection)
                {

                    case Direction.Down:
                        spriteBatch.Draw(charTex, HitRect, new Rectangle(nrDown.X + 38 * frame, nrDown.Y, nrDown.Width, nrDown.Height), Color.Gray);
                        break;
                    case Direction.Up:
                        spriteBatch.Draw(charTex, HitRect, new Rectangle(nrUp.X + 38 * frame, nrUp.Y, nrUp.Width, nrUp.Height), Color.Gray);
                        break;
                    case Direction.Left:
                        spriteBatch.Draw(charTex, HitRect, new Rectangle(nrLeft.X + 38 * frame, nrLeft.Y, nrLeft.Width, nrLeft.Height), Color.Gray);
                        break;
                    case Direction.Right:
                        spriteBatch.Draw(charTex, HitRect, new Rectangle(nrRight.X + 38 * frame, nrRight.Y, nrRight.Width, nrRight.Height), Color.Gray);
                        break;
                }
              //  spriteBatch.Draw(texture, HitRect, Color.Gray);
            }
            else if (IsDamaged)
            {
                switch (currentDirection)
                {

                    case Direction.Down:
                        spriteBatch.Draw(charTex, HitRect, new Rectangle(nrDown.X + 38 * frame, nrDown.Y, nrDown.Width, nrDown.Height), Color.Red);
                        break;
                    case Direction.Up:
                        spriteBatch.Draw(charTex, HitRect, new Rectangle(nrUp.X + 38 * frame, nrUp.Y, nrUp.Width, nrUp.Height), Color.Red);
                        break;
                    case Direction.Left:
                        spriteBatch.Draw(charTex, HitRect, new Rectangle(nrLeft.X + 38 * frame, nrLeft.Y, nrLeft.Width, nrLeft.Height), Color.Red);
                        break;
                    case Direction.Right:
                        spriteBatch.Draw(charTex, HitRect, new Rectangle(nrRight.X + 38 * frame, nrRight.Y, nrRight.Width, nrRight.Height), Color.Red);
                        break;
                }
            //    spriteBatch.Draw(texture, HitRect, Color.Red);
            }
            else
            {
                switch (currentDirection)
                {

                    case Direction.Down:
                        spriteBatch.Draw(charTex, HitRect, new Rectangle(nrDown.X + 38 * frame, nrDown.Y, nrDown.Width, nrDown.Height), Color.Gray);
                        break;
                    case Direction.Up:
                        spriteBatch.Draw(charTex, HitRect, new Rectangle(nrUp.X + 38 * frame, nrUp.Y, nrUp.Width, nrUp.Height), Color.Gray);
                        break;
                    case Direction.Left:
                        spriteBatch.Draw(charTex, HitRect, new Rectangle(nrLeft.X + 38 * frame, nrLeft.Y, nrLeft.Width, nrLeft.Height), Color.Gray);
                        break;
                    case Direction.Right:
                        spriteBatch.Draw(charTex, HitRect, new Rectangle(nrRight.X + 38 * frame, nrRight.Y, nrRight.Width, nrRight.Height), Color.Gray);
                        break;
                }
           //     base.Draw(spriteBatch, texture);
            }
        }
    }
}
