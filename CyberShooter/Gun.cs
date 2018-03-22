using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Cyberphunk
{
    class Gun : Weapon
    {
        public float range;
        public int speed;
        public List<Projectile> projectileList;

        public Gun(Vector2 position) : base(position)
        {
            projectileList = new List<Projectile>();
            damage = 1;
            //How long the projectile will travel before it becomes inactive
            range = 150;
            //<\>
            speed = 3;
            originCooldown = 500;
            base.Update();
        }
        public override void Update(GameTime gameTime)
        {
            Shooting();
            base.Update(gameTime);
        }
        public virtual void Shooting()
        {
            if (KeyMouseReader.mouseState.LeftButton == ButtonState.Pressed)
            {
                if (cooldown <= 0)
                {
                    Projectile projectile = new Projectile(position, target);
                    projectile = new Projectile(new Vector2(position.X - (projectile.texWidth / 2), position.Y - (projectile.texHeight / 2)), target);
                    projectile.damage = damage;
                    cooldown = originCooldown;
                    projectile.ProjectileDefinition(target, range, speed);
                    projectileList.Add(projectile);
                }
            }
            foreach (Projectile projectile in projectileList)
            {
                projectile.Update();
            }
        }
        public override void Draw(SpriteBatch spriteBatch, Texture2D squareTex)
        {
            base.Draw(spriteBatch, squareTex);
            foreach (Projectile projectile in projectileList)
            {
                projectile.Draw(spriteBatch, squareTex);
            }
        }
    }
}