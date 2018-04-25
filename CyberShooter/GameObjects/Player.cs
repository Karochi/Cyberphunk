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
        enum Direction {Left,Up,Down,Right}
        Direction currentDirection;
        Rectangle srDown = new Rectangle(0, 38 * 4, 30, 38);
        Rectangle srUp = new Rectangle(0, 38 * 7, 30, 38);
        Rectangle srLeft = new Rectangle(0, 38 * 5, 30, 38);
        Rectangle srRight = new Rectangle(0, 38 * 6, 30, 38);
        Weapon firstWeapon, secondWeapon;
        Vector2 target, playerCenter;
        Texture2D charTex;
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
            charTex = Game1.charTex;
            firstWeapon = new Weapon(WeaponNames.unarmed);
            secondWeapon = new Weapon(WeaponNames.unarmed);
            this.Position = (position);
            TexWidth = 16;
            TexHeight = 20;
            handgunAmmo = 60;
            rifleAmmo = 30;
            MaxHealth = 8;
            CurrHealth = MaxHealth;
        }
        public override void Update()
        {


            base.Update();
        }
        public void Update2(GameTime gameTime, Vector2 target, List<Rectangle> collisionRects, List<WeaponPickup> weaponPickupList, List<NPC> NPCs)
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
                currentDirection = Direction.Left;
            }
            else if (Speed.X <= 3 && KeyMouseReader.KeyHeld(Keys.D))
            {
                Speed = new Vector2(Speed.X + 0.2f, Speed.Y);
                currentDirection = Direction.Right;
                
            }
            else if (Speed.Y >= (-3) && KeyMouseReader.KeyHeld(Keys.W))
            {
                Speed = new Vector2(Speed.X, Speed.Y - 0.2f);
                currentDirection = Direction.Up;
            }
            else if (Speed.Y <= 3 && KeyMouseReader.KeyHeld(Keys.S))
            {
                Speed = new Vector2(Speed.X, Speed.Y + 0.2f);
                currentDirection = Direction.Down;
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
                switch (currentDirection)
                {

                    case Direction.Down:
                        spriteBatch.Draw(charTex, HitRect, new Rectangle(srDown.X + 38 * frame, srDown.Y, srDown.Width, srDown.Height), Color.Gray);
                        break;
                    case Direction.Up:
                        spriteBatch.Draw(charTex, HitRect, new Rectangle(srUp.X + 38 * frame, srUp.Y, srUp.Width, srUp.Height), Color.Gray);
                        break;
                    case Direction.Left:
                        spriteBatch.Draw(charTex, HitRect, new Rectangle(srLeft.X + 38 * frame, srLeft.Y, srLeft.Width, srLeft.Height), Color.Gray);
                        break;
                    case Direction.Right:
                        spriteBatch.Draw(charTex, HitRect, new Rectangle(srRight.X + 38 * frame, srRight.Y, srRight.Width, srRight.Height), Color.Gray);
                        break;
                }
                //spriteBatch.Draw(texture, HitRect, Color.Gray);
            }
            else if (DamageCooldown > 0)
            {
                switch (currentDirection)
                {

                    case Direction.Down:
                        spriteBatch.Draw(charTex, HitRect, new Rectangle(srDown.X + 38 * frame, srDown.Y, srDown.Width, srDown.Height), Color.LightYellow);
                        break;
                    case Direction.Up:
                        spriteBatch.Draw(charTex, HitRect, new Rectangle(srUp.X + 38 * frame, srUp.Y, srUp.Width, srUp.Height), Color.LightYellow);
                        break;
                    case Direction.Left:
                        spriteBatch.Draw(charTex, HitRect, new Rectangle(srLeft.X + 38 * frame, srLeft.Y, srLeft.Width, srLeft.Height), Color.LightYellow);
                        break;
                    case Direction.Right:
                        spriteBatch.Draw(charTex, HitRect, new Rectangle(srRight.X + 38 * frame, srRight.Y, srRight.Width, srRight.Height), Color.LightYellow);
                        break;
                }
                //spriteBatch.Draw(texture, HitRect, Color.LightYellow);
            }
            else
            {
                switch (currentDirection)
                {

                    case Direction.Down:
                        spriteBatch.Draw(charTex, HitRect, new Rectangle(srDown.X + 38 * frame, srDown.Y, srDown.Width, srDown.Height), Color.White);
                        break;
                    case Direction.Up:
                        spriteBatch.Draw(charTex, HitRect, new Rectangle(srUp.X + 38 * frame, srUp.Y, srUp.Width, srUp.Height), Color.White);
                        break;
                    case Direction.Left:
                        spriteBatch.Draw(charTex, HitRect, new Rectangle(srLeft.X + 38 * frame, srLeft.Y, srLeft.Width, srLeft.Height), Color.White);
                        break;
                    case Direction.Right:
                        spriteBatch.Draw(charTex, HitRect, new Rectangle(srRight.X + 38 * frame, srRight.Y, srRight.Width, srRight.Height), Color.White);
                        break;
                }
                //base.Draw(spriteBatch, texture);
            }
        }
    }
}
