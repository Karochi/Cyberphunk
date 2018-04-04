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

    class Player : MovingGameObject
    {
        public Weapon firstWeapon, secondWeapon;
        public Vector2 target, playerCenter;
        public int ammo;
        Projectile projectile;

        public Player(Vector2 position) : base()
        {
            firstWeapon = new Weapon(WeaponNames.unarmed);
            secondWeapon = new Weapon(WeaponNames.unarmed);
            this.position = position;
            texHeight = 40;
            texWidth = 30;
            ammo = 60;
        }
        public void Update(GameTime gameTime, Vector2 target, GameBoard gameBoard)
        {
            base.Update();
            this.target = target;
            playerCenter = new Vector2(position.X + texWidth / 2, position.Y + texHeight / 2);

            firstWeapon.Update(gameTime, gameBoard);
            secondWeapon.Update(gameTime, gameBoard);

            Shooting(gameBoard);

            Moving();
            StoppingX();
            StoppingY();
            if (KeyMouseReader.KeyPressed(Keys.Space))
                WeaponSwap();
            if (KeyMouseReader.KeyPressed(Keys.X) && firstWeapon.weaponName != WeaponNames.unarmed)
                gameBoard.WeaponDrop();
        }
        public void Moving()
        {
            if (speed.X >= (-3) && KeyMouseReader.KeyHeld(Keys.A))
            {
                speed.X -= 0.2f;
            }
            else if (speed.X <= 3 && KeyMouseReader.KeyHeld(Keys.D))
            {
                speed.X += 0.2f;
            }
            else if (speed.Y >= (-3) && KeyMouseReader.KeyHeld(Keys.W))
            {
                speed.Y -= 0.2f;
            }
            else if (speed.Y <= 3 && KeyMouseReader.KeyHeld(Keys.S))
            {
                speed.Y += 0.2f;
            }
        }
        public void StoppingX()
        {
            if (!KeyMouseReader.KeyHeld(Keys.D) && !KeyMouseReader.KeyHeld(Keys.A))
            {
                if (speed.X < 0.2f && speed.X > (-0.2f))
                {
                    speed.X = 0;
                }
                if (speed.X > 0)
                {
                    speed.X -= 0.2f;
                }
                if (speed.X < 0)
                {
                    speed.X += 0.2f;
                }
            }
        }
        public void StoppingY()
        {
            if(!KeyMouseReader.KeyHeld(Keys.W) && !KeyMouseReader.KeyHeld(Keys.S))
            {
                if (speed.Y < 0.2f && speed.Y > (-0.2f))
                {
                    speed.Y = 0;
                }
                if (speed.Y > 0)
                {
                    speed.Y -= 0.2f;
                }
                if (speed.Y < 0)
                {
                    speed.Y += 0.2f;
                }
            }
        }
        public void Shooting(GameBoard gameBoard)
        {
            if(ammo > 0 && firstWeapon.weaponName != WeaponNames.unarmed)
            {
                SemiAuto(gameBoard);
                Auto(gameBoard);
            }
        }
        public void SemiAuto(GameBoard gameBoard)
        {
            if (KeyMouseReader.LeftClick() && firstWeapon.weaponType == WeaponTypes.semiAuto)
            {
                if (firstWeapon.cooldown <= 0)
                {
                    projectile = new Projectile(gameBoard.player.playerCenter, gameBoard.player.target, firstWeapon.damage, firstWeapon.range, firstWeapon.projectileSpeed);
                    gameBoard.projectileList.Add(projectile);
                    firstWeapon.cooldown = firstWeapon.originCooldown;
                    ammo--;
                }
            }
        }
        public void Auto(GameBoard gameBoard)
        {
            if (KeyMouseReader.mouseState.LeftButton == ButtonState.Pressed && firstWeapon.weaponType == WeaponTypes.auto)
            {
                if (firstWeapon.cooldown <= 0)
                {
                    projectile = new Projectile(gameBoard.player.playerCenter, gameBoard.player.target, firstWeapon.damage, firstWeapon.range, firstWeapon.projectileSpeed);
                    gameBoard.projectileList.Add(projectile);
                    firstWeapon.cooldown = firstWeapon.originCooldown;
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
