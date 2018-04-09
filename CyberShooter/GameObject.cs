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
        Vector2 position;
        Rectangle hitRect;
        int texWidth, texHeight;

        public Vector2 GetPosition()
        {
            return position;
        }
        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }
        public Rectangle GetHitRect()
        {
            return hitRect;
        }
        public int GetTexWidth()
        {
            return texWidth;
        }
        public void SetTexWidth(int texWidth)
        {
            this.texWidth = texWidth;
        }
        public int GetTexHeight()
        {
            return texHeight;
        }
        public void SetTexHeight(int texHeight)
        {
            this.texHeight = texHeight;
        }
        protected GameObject()
        {
            hitRect = new Rectangle((int)position.X, (int)position.Y, texWidth, texHeight);
        }
        public virtual void Update()
        {
            hitRect = new Rectangle((int)position.X, (int)position.Y, texWidth, texHeight);
        }
        public virtual void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, position, new Rectangle((int)position.X, (int)position.Y, texWidth, texHeight), Color.White);
        }
    }
}