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
        public void Update(GameTime gameTime, Vector2 target, GameBoard gameBoard)
        {
            base.Update(gameTime);
            this.target = target;
            playerCenter = new Vector2(Position.X + TexWidth / 2, Position.Y + TexHeight / 2);

            firstWeapon.Update(gameTime, gameBoard);
            secondWeapon.Update(gameTime, gameBoard);

            Shooting(gameBoard);

            Moving();
            StoppingX();
            StoppingY();
            if (KeyMouseReader.KeyPressed(Keys.Space))
                WeaponSwap();
            if (KeyMouseReader.KeyPressed(Keys.X) && firstWeapon.GetWeaponName() != WeaponNames.unarmed)
                gameBoard.WeaponDrop();
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
        public void Shooting(GameBoard gameBoard)
        {
            if(firstWeapon.GetWeaponName() == WeaponNames.handgun)
            {
                if (handgunAmmo > 0)
                {
                    if(SemiAuto(gameBoard))
                        handgunAmmo--;
                }
            }
            if(firstWeapon.GetWeaponName() == WeaponNames.rifle)
            {
                if(rifleAmmo > 0)
                {
                    if (Auto(gameBoard))
                        rifleAmmo--;
                }
            }
        }
        public bool SemiAuto(GameBoard gameBoard)
        {
            if (KeyMouseReader.LeftClick() && firstWeapon.GetWeaponType() == WeaponTypes.semiAuto)
            {
                if (firstWeapon.GetCooldown() <= 0)
                {
                    projectile = new Projectile(playerCenter, target, firstWeapon.GetDamage(), firstWeapon.GetRange(), firstWeapon.GetProjectileSpeed());
                    gameBoard.projectileList.Add(projectile);
                    firstWeapon.SetCooldown(firstWeapon.GetOriginCooldown());
                    return true;
                }
            }
            return false;
        }
        public bool Auto(GameBoard gameBoard)
        {
            if (KeyMouseReader.mouseState.LeftButton == ButtonState.Pressed && firstWeapon.GetWeaponType() == WeaponTypes.auto)
            {
                if (firstWeapon.GetCooldown() <= 0)
                {
                    projectile = new Projectile(playerCenter, target, firstWeapon.GetDamage(), firstWeapon.GetRange(), firstWeapon.GetProjectileSpeed());
                    gameBoard.projectileList.Add(projectile);
                    firstWeapon.SetCooldown(firstWeapon.GetOriginCooldown());
                    return true;
                }
            }
            return false;
        }
        public void WeaponSwap()
        {
            Weapon swapWeapon;
            swapWeapon = firstWeapon;
            firstWeapon = secondWeapon;
            secondWeapon = swapWeapon;
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
