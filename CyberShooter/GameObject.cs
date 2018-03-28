﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CyberShooter
{
    public abstract class GameObject
    {
        public Vector2 position;
        public Rectangle hitRect;
        //texWidth && texHeight is used to resize the current single square used for testing. It will need to be reworked for actual use of textures.
        public int texWidth, texHeight;
        //isActive is a low-effort way of de-spawning objects while they still remain in lists.
        public bool isActive;

        protected GameObject()
        {
            isActive = true;
            hitRect = new Rectangle((int)position.X, (int)position.Y, texWidth, texHeight);
        }
        public virtual void Update()
        {
            if (isActive)
            {
                hitRect = new Rectangle((int)position.X, (int)position.Y, texWidth, texHeight);
            }
        }
        public virtual void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            if (isActive)
            {
                spriteBatch.Draw(texture, position, new Rectangle((int)position.X, (int)position.Y, texWidth, texHeight), Color.White);
            }
        }
    }
}