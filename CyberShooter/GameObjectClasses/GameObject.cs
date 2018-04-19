using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CyberShooter
{
    public abstract class GameObject
    {
        public Vector2 Position { get; set; }
        public Rectangle HitRect { get; set; }

        public int TexWidth { get; set; }
        public int TexHeight { get; set; }

        protected GameObject()
        {
            HitRect = new Rectangle((int)Position.X, (int)Position.Y, TexWidth, TexHeight);
        }
        public virtual void Update()
        {
            HitRect = new Rectangle((int)Position.X, (int)Position.Y, TexWidth, TexHeight);
        }
        public virtual void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, Position, new Rectangle((int)Position.X, (int)Position.Y, TexWidth, TexHeight), Color.White);
        }
    }
}