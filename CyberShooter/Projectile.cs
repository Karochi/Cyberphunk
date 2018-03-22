﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyberphunk
{
    public class Projectile : GameObject
    {
        public Vector2 originPosition, direction, updatedDirection, target;
        public int speed, damage;
        public float range;

        public Projectile(Vector2 position, Vector2 target) : base()
        {
            this.position = position;
            this.target = target;
            originPosition = position;
            texWidth = 3;
            texHeight = 3;
            base.Update();
        }
        public void ProjectileDefinition(Vector2 target, float range, int speed)
        {
            this.range = range;
            this.speed = speed;
            direction = target - position;
            updatedDirection = Vector2.Normalize(direction);
        }
        public override void Update()
        {
            position += updatedDirection * speed;
            if (Vector2.Distance(originPosition, position) >= range)
            {
                isActive = false;
            }
            base.Update();
        }
    }
}