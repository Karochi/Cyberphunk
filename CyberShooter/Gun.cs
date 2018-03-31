using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CyberShooter
{
    class Gun : Weapon
    {
        public float range;
        public int speed;
        public List<Projectile> projectileList;
        Projectile projectile;

        public Gun() : base()
        {
            projectileList = new List<Projectile>();
            damage = 1;
            //How long the projectile will travel before it becomes inactive
            range = 150;
            //How fast the projectile goes
            speed = 3;
            //Outline projectile object to create values for texWidth/texHeight
            projectile = new Projectile(Vector2.Zero, Vector2.Zero, 0);
            originCooldown = 500;
            base.Update();
        }
        public virtual void Update(GameTime gameTime, Vector2 position, Vector2 target)
        {
            this.position = position;
            Shooting();
            base.Update(gameTime, target);
        }
        public virtual void Shooting()
        {
            if (KeyMouseReader.mouseState.LeftButton == ButtonState.Pressed)
            {
                if (cooldown <= 0)
                {
                    projectile = new Projectile(new Vector2(target.X - (projectile.texWidth / 2), position.Y - (projectile.texHeight / 2)), target, damage);
                    projectile.ProjectileDefinition(target, range, speed);
                    projectileList.Add(projectile);
                    cooldown = originCooldown;
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