using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CyberShooter
{
    public class Projectile : GameObject
    {
        Vector2 originPosition, direction, updatedDirection, target;
        int speed, damage;
        float range;

        public Vector2 GetOriginPosition()
        {
            return originPosition;
        }
        public float GetRange()
        {
            return range;
        }
        public int GetDamage()
        {
            return damage;
        }
        public Projectile(Vector2 position, Vector2 target, int damage, float range, int speed) : base()
        {
            this.damage = damage;
            this.target = target;
            this.range = range;
            this.speed = speed;
            Position = position;
            TexWidth = 10;
            TexHeight = 10;
            originPosition = position;
            direction = target - position;
            updatedDirection = Vector2.Normalize(direction);
        }

        public override void Update()
        {
            Position += updatedDirection * speed;
            base.Update();
        }
    }
}