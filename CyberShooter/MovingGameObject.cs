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
        Vector2 speed, oldPosition;
        int health;
        float damageCooldown;
        bool isDamaged, isDead;

        public Vector2 GetSpeed()
        {
            return speed;
        }
        public void SetSpeed(Vector2 speed)
        {
            this.speed = speed;
        }
        public Vector2 GetOldPosition()
        {
            return oldPosition;
        }
        public void SetOldPosition(Vector2 oldPosition)
        {
            this.oldPosition = oldPosition;
        }
        public int GetHealth()
        {
            return health;
        }
        public void SetHealth(int health)
        {
            this.health = health;
        }
        public bool GetIsDamaged()
        {
            return isDamaged;
        }
        public void SetIsDamaged(bool damaged)
        {
            isDamaged = damaged;
        }
        public bool GetIsDead()
        {
            return isDead;
        }
        public void SetIsDead(bool isDead)
        {
            this.isDead = isDead;
        }
        public float GetDamageCooldown()
        {
            return damageCooldown;
        }
        public void SetDamageCooldown(float damageCooldown)
        {
            this.damageCooldown = damageCooldown;
        }
        public MovingGameObject() : base()
        {
            isDead = false;
            isDamaged = false;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (GetHealth() <= 0)
            {
                SetIsDead(true);
            }
            damageCooldown -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            MovementUpdate();
            base.Update();
            if (GetIsDead())
            {

            }
        }
        public void MovementUpdate()
        {
            oldPosition = GetPosition();
            SetPosition(GetPosition() + speed);
        }
    }
}
