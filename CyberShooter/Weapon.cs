using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CyberShooter
{
    abstract class Weapon : GameObject
    {
        public int damage, cooldown, originCooldown;
        public Vector2 target;

        public Weapon() : base()
        {
        }
        public virtual void Update(GameTime gameTime, Vector2 target)
        {
            //All weapons should have an attack cooldown
            this.target = target;
            cooldown -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            base.Update();
        }
        public override void Draw(SpriteBatch spriteBatch, Texture2D squareTex)
        {
            base.Draw(spriteBatch, squareTex);
        }
    }
}