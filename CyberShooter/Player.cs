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
    public enum WeaponStates { unarmed, gun };

    class Player : MovingGameObject
    {
        public WeaponStates weaponState;
        public List<Projectile> projectileList;
        Projectile projectile;
        public int damage, cooldown, originCooldown, projectileSpeed;
        public float range;
        public Vector2 target;

        public Player(Vector2 position) : base()
        {
            projectileList = new List<Projectile>();
            weaponState = WeaponStates.unarmed;
            this.position = position;
            texHeight = 40;
            texWidth = 30;
        }
        public void Update(GameTime gameTime, Vector2 target)
        {
            base.Update();
            this.target = target;
            cooldown -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            //you could call on this in the pickup class so that you only change the values once and not in every update
            if(weaponState == WeaponStates.gun)
            {
                GunDefinition();
            }
            Moving();
            StoppingX();
            StoppingY();
            Shooting();
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
        public void Shooting()
        {
            if (KeyMouseReader.mouseState.LeftButton == ButtonState.Pressed)
            {
                if (cooldown <= 0 && weaponState != WeaponStates.unarmed)
                {
                    projectile = new Projectile(position, target, damage, range, projectileSpeed);
                    projectileList.Add(projectile);
                    cooldown = originCooldown;
                }
            }
            foreach (Projectile projectile in projectileList)
            {
                projectile.Update();
            }
        }
        public void GunDefinition()
        {
            damage = 1;
            range = 150;
            projectileSpeed = 10;
            originCooldown = 500;
        }
    }
}
