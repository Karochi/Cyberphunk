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
        Texture2D tex;

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
        public Projectile(Texture2D tex,Vector2 position, Vector2 target, int damage, float range, int speed) : base()
        {
            this.damage = damage;
            this.target = target;
            this.range = range;
            this.speed = speed;
            Position = position;
            this.tex = tex;
            TexWidth = 16;
            TexHeight = 16;
            originPosition = position;
            direction = target - position;
            updatedDirection = Vector2.Normalize(direction);
        }

        public override void Update()
        {
            Position += updatedDirection * speed;
            base.Update();
        }
        public override void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(tex, HitRect, Color.White);
        }
    }
}