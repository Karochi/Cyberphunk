﻿using Microsoft.Xna.Framework;
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
        int ammo;
        float damageCooldown;
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
        public int GetAmmo()
        {
            return ammo;
        }
        public void SetAmmo(int ammo)
        {
            this.ammo = ammo;
        }

        public Player(Vector2 position) : base()
        {
            firstWeapon = new Weapon(WeaponNames.unarmed);
            secondWeapon = new Weapon(WeaponNames.unarmed);
            SetPosition(position);
            SetTexHeight(40);
            SetTexWidth(30);
            ammo = 60;
            SetHealth(8);
        }
        public void Update(GameTime gameTime, Vector2 target, GameBoard gameBoard)
        {
            base.Update();
            this.target = target;
            playerCenter = new Vector2(GetPosition().X + GetTexWidth() / 2, GetPosition().Y + GetTexHeight() / 2);

            damageCooldown -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;

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
        public void Damage(GameTime gameTime)
        {
            if (damageCooldown <= 0)
            {
                SetHealth(GetHealth()-1);
                damageCooldown = 1000;
                SetDamaged(false);
            }
        }
        public void Moving()
        {
            if (GetSpeed().X >= (-3) && KeyMouseReader.KeyHeld(Keys.A))
            {
                SetSpeed(new Vector2(GetSpeed().X - 0.2f, GetSpeed().Y));
            }
            else if (GetSpeed().X <= 3 && KeyMouseReader.KeyHeld(Keys.D))
            {
                SetSpeed(new Vector2(GetSpeed().X + 0.2f, GetSpeed().Y));
            }
            else if (GetSpeed().Y >= (-3) && KeyMouseReader.KeyHeld(Keys.W))
            {
                SetSpeed(new Vector2(GetSpeed().X, GetSpeed().Y - 0.2f));
            }
            else if (GetSpeed().Y <= 3 && KeyMouseReader.KeyHeld(Keys.S))
            {
                SetSpeed(new Vector2(GetSpeed().X, GetSpeed().Y + 0.2f));
            }
        }
        public void StoppingX()
        {
            if (!KeyMouseReader.KeyHeld(Keys.D) && !KeyMouseReader.KeyHeld(Keys.A))
            {
                if (GetSpeed().X < 0.2f && GetSpeed().X > (-0.2f))
                {
                    SetSpeed(new Vector2(0, GetSpeed().Y));
                }
                if (GetSpeed().X > 0)
                {
                    SetSpeed(new Vector2(GetSpeed().X - 0.2f, GetSpeed().Y));
                }
                if (GetSpeed().X < 0)
                {
                    SetSpeed(new Vector2(GetSpeed().X + 0.2f, GetSpeed().Y));
                }
            }
        }
        public void StoppingY()
        {
            if(!KeyMouseReader.KeyHeld(Keys.W) && !KeyMouseReader.KeyHeld(Keys.S))
            {
                if (GetSpeed().Y < 0.2f && GetSpeed().Y > (-0.2f))
                {
                    SetSpeed(new Vector2(GetSpeed().X, 0));
                }
                if (GetSpeed().Y > 0)
                {
                    SetSpeed(new Vector2(GetSpeed().X, GetSpeed().Y - 0.2f));
                }
                if (GetSpeed().Y < 0)
                {
                    SetSpeed(new Vector2(GetSpeed().X, GetSpeed().Y + 0.2f));
                }
            }
        }
        public void Shooting(GameBoard gameBoard)
        {
            if(ammo > 0 && firstWeapon.GetWeaponName() != WeaponNames.unarmed)
            {
                SemiAuto(gameBoard);
                Auto(gameBoard);
            }
        }
        public void SemiAuto(GameBoard gameBoard)
        {
            if (KeyMouseReader.LeftClick() && firstWeapon.GetWeaponType() == WeaponTypes.semiAuto)
            {
                if (firstWeapon.GetCooldown() <= 0)
                {
                    projectile = new Projectile(playerCenter, target, firstWeapon.GetDamage(), firstWeapon.GetRange(), firstWeapon.GetProjectileSpeed());
                    gameBoard.projectileList.Add(projectile);
                    firstWeapon.SetCooldown(firstWeapon.GetOriginCooldown());
                    ammo--;
                }
            }
        }
        public void Auto(GameBoard gameBoard)
        {
            if (KeyMouseReader.mouseState.LeftButton == ButtonState.Pressed && firstWeapon.GetWeaponType() == WeaponTypes.auto)
            {
                if (firstWeapon.GetCooldown() <= 0)
                {
                    projectile = new Projectile(playerCenter, target, firstWeapon.GetDamage(), firstWeapon.GetRange(), firstWeapon.GetProjectileSpeed());
                    gameBoard.projectileList.Add(projectile);
                    firstWeapon.SetCooldown(firstWeapon.GetOriginCooldown());
                    ammo--;
                }
            }
        }
        public void WeaponSwap()
        {
            Weapon swapWeapon;
            swapWeapon = firstWeapon;
            firstWeapon = secondWeapon;
            secondWeapon = swapWeapon;
        }
    }
}
