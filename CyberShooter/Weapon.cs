﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyberphunk
{
    abstract class Weapon : GameObject
    {
        public int damage, cooldown, originCooldown;
        public Vector2 target;

        public Weapon(Vector2 position) : base()
        {
            this.position = position;
        }
        public virtual void Update(GameTime gameTime)
        {
            //All weapons should have an attack cooldown
            cooldown -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            base.Update();
        }
        public override void Draw(SpriteBatch spriteBatch, Texture2D squareTex)
        {
            base.Draw(spriteBatch, squareTex);
        }
    }
}