using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberShooter
{
    public class MovingGameObject : AnimatedGameObject
    {
        public Vector2 Speed { get; set; }
        public Vector2 OldPosition { get; set; }

        public int CurrHealth { get; set; }
        public int MaxHealth { get; set; }

        public float DamageCooldown { get; set; }

        public bool IsDamaged { get; set; }
        public bool IsDead { get; set; }

        public Rectangle leftRect, rightRect, topRect, bottomRect;
        public bool leftRectCollision, rightRectCollision, topRectCollision, bottomRectCollision;

        public MovingGameObject() : base()
        {
            IsDead = false;
            IsDamaged = false;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (CurrHealth <= 0)
            {
                IsDead = true;
            }
            DamageCooldown -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            MovementUpdate();
            DirectionalHitBoxes();
            base.Update();
        }
        public void MovementUpdate()
        {
            OldPosition = Position;
            Position += Speed;
        }
        public void DirectionalHitBoxes()
        {
            leftRect = new Rectangle((int)Position.X - 10, (int)Position.Y, 10, TexHeight);
            rightRect = new Rectangle((int)Position.X + TexWidth, (int)Position.Y, 10, TexHeight);
            topRect = new Rectangle((int)Position.X, (int)Position.Y - 10, TexWidth, 10);
            bottomRect = new Rectangle((int)Position.X, (int)Position.Y + TexHeight, TexWidth, 10);
        }

    }
}
