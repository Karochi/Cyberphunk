using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberShooter
{
    public enum WeaponStates { unarmed, melee, semiAuto, auto };

    public class Player : MovingGameObject
    {
        Weapon firstWeapon, secondWeapon;
        Vector2 target, playerCenter;
        public int handgunAmmo { get; set; }
        public int rifleAmmo { get; set; }

        Projectile projectile;

        public Weapon GetFirstWeapon()
        {
            return firstWeapon;
        }
        public void SetFirstWeapon(Weapon weapon)
        {
            firstWeapon = weapon;
        }
        public Weapon GetSecondWeapon()
        {
            return secondWeapon;
        }
        public void SetSecondWeapon(Weapon weapon)
        {
            secondWeapon = weapon;
        }
        public Vector2 GetTarget()
        {
            return target;
        }
        public Vector2 GetPlayerCenter()
        {
            return playerCenter;
        }

        public Player(Vector2 position) : base()
        {
            firstWeapon = new Weapon(WeaponNames.unarmed);
            secondWeapon = new Weapon(WeaponNames.unarmed);
            this.Position = (position);
            TexWidth = 30;
            TexHeight = 40;
            handgunAmmo = 60;
            rifleAmmo = 30;
            MaxHealth = 8;
            CurrHealth = MaxHealth;
        }
        public void Update(GameTime gameTime, Vector2 target, List<Rectangle> collisionRects, List<WeaponPickup> weaponPickupList, List<NPC> NPCs)
        {
            base.Update(gameTime, collisionRects);
            this.target = target;
            playerCenter = new Vector2(Position.X + TexWidth / 2, Position.Y + TexHeight / 2);

            firstWeapon.Update(gameTime);
            secondWeapon.Update(gameTime);

            Shooting();
            ProjectileNPCCollision(NPCs);

            Moving();
            StoppingX();
            StoppingY();
            if (KeyMouseReader.KeyPressed(Keys.Space))
                WeaponSwap();
            if (KeyMouseReader.KeyPressed(Keys.X) && firstWeapon.GetWeaponName() != WeaponNames.unarmed)
                WeaponDrop(weaponPickupList);
        }
        public bool WeaponFullCheck()
        {
            if (GetFirstWeapon().GetWeaponName() != WeaponNames.unarmed && GetSecondWeapon().GetWeaponName() != WeaponNames.unarmed)
                return true;
            else return false;
        }
        public void WeaponDrop(List<WeaponPickup> weaponPickupList)
        {
            weaponPickupList.Add(new WeaponPickup(Position, GetFirstWeapon().GetPickUpType()));
            GetFirstWeapon().SetWeaponName(WeaponNames.unarmed);

        }
        public void WeaponSwap()
        {
            Weapon swapWeapon;
            swapWeapon = firstWeapon;
            firstWeapon = secondWeapon;
            secondWeapon = swapWeapon;
        }
        public bool Damage()
        {
            if (DamageCooldown <= 0)
            {
                DamageCooldown = 200;
                IsDamaged = false;
                return true;
            }
            else return false;
        }
        public void Moving()
        {
            if (Speed.X >= (-3) && KeyMouseReader.KeyHeld(Keys.A))
            {
                Speed = new Vector2(Speed.X - 0.2f, Speed.Y);
            }
            else if (Speed.X <= 3 && KeyMouseReader.KeyHeld(Keys.D))
            {
                Speed = new Vector2(Speed.X + 0.2f, Speed.Y);
            }
            else if (Speed.Y >= (-3) && KeyMouseReader.KeyHeld(Keys.W))
            {
                Speed = new Vector2(Speed.X, Speed.Y - 0.2f);
            }
            else if (Speed.Y <= 3 && KeyMouseReader.KeyHeld(Keys.S))
            {
                Speed = new Vector2(Speed.X, Speed.Y + 0.2f);
            }
        }
        public void StoppingX()
        {
            if (!KeyMouseReader.KeyHeld(Keys.D) && !KeyMouseReader.KeyHeld(Keys.A))
            {
                if (Speed.X < 0.2f && Speed.X > (-0.2f))
                {
                    Speed = new Vector2(0, Speed.Y);
                }
                if (Speed.X > 0)
                {
                    Speed = new Vector2(Speed.X - 0.2f, Speed.Y);
                }
                if (Speed.X < 0)
                {
                    Speed = new Vector2(Speed.X + 0.2f, Speed.Y);
                }
            }
        }
        public void StoppingY()
        {
            if(!KeyMouseReader.KeyHeld(Keys.W) && !KeyMouseReader.KeyHeld(Keys.S))
            {
                if (Speed.Y < 0.2f && Speed.Y > (-0.2f))
                {
                    Speed = new Vector2(Speed.X, 0);
                }
                if (Speed.Y > 0)
                {
                    Speed = new Vector2(Speed.X, Speed.Y - 0.2f);
                }
                if (Speed.Y < 0)
                {
                    Speed = new Vector2(Speed.X, Speed.Y + 0.2f);
                }
            }
        }
        public void Shooting()
        {
            if(firstWeapon.GetWeaponName() == WeaponNames.handgun)
            {
                if (handgunAmmo > 0)
                {
                    if(SemiAuto())
                        handgunAmmo--;
                }
            }
            if(firstWeapon.GetWeaponName() == WeaponNames.rifle)
            {
                if(rifleAmmo > 0)
                {
                    if (Auto())
                        rifleAmmo--;
                }
            }
        }
        public void ProjectileNPCCollision(List<NPC> NPCs)
        {
            foreach (Projectile projectile in ProjectileList)
            {
                foreach (NPC npc in NPCs)
                {
                    if (projectile.HitRect.Intersects(npc.HitRect))
                    {
                        npc.CurrHealth -= projectile.GetDamage();
                        npc.IsDamaged = true;
                        npc.DamageCooldown = 100;
                        ProjectileList.Remove(projectile);
                        return;
                    }
                }
            }
        }
        public bool SemiAuto()
        {
            if (KeyMouseReader.LeftClick() && firstWeapon.GetWeaponType() == WeaponTypes.semiAuto)
            {
                if (firstWeapon.GetCooldown() <= 0)
                {
                    projectile = new Projectile(playerCenter, target, firstWeapon.GetDamage(), firstWeapon.GetRange(), firstWeapon.GetProjectileSpeed());
                    ProjectileList.Add(projectile);
                    firstWeapon.SetCooldown(firstWeapon.GetOriginCooldown());
                    return true;
                }
            }
            return false;
        }
        public bool Auto()
        {
            if (KeyMouseReader.mouseState.LeftButton == ButtonState.Pressed && firstWeapon.GetWeaponType() == WeaponTypes.auto)
            {
                if (firstWeapon.GetCooldown() <= 0)
                {
                    projectile = new Projectile(playerCenter, target, firstWeapon.GetDamage(), firstWeapon.GetRange(), firstWeapon.GetProjectileSpeed());
                    ProjectileList.Add(projectile);
                    firstWeapon.SetCooldown(firstWeapon.GetOriginCooldown());
                    return true;
                }
            }
            return false;
        }

        public override void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            if (IsDead)
            {
                spriteBatch.Draw(texture, HitRect, Color.Gray);
            }
            else if (DamageCooldown > 0)
            {
                spriteBatch.Draw(texture, HitRect, Color.LightYellow);
            }
            else
            {
                base.Draw(spriteBatch, texture);
            }
        }
    }
}
