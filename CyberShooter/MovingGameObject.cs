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
        bool damaged;

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
        public bool GetDamaged()
        {
            return damaged;
        }
        public void SetDamaged(bool damaged)
        {
            this.damaged = damaged;
        }
        public MovingGameObject() : base()
        {
        }

        public override void Update()
        {
            MovementUpdate();
            base.Update();
        }
        public void MovementUpdate()
        {
            oldPosition = GetPosition();
            SetPosition(GetPosition() + speed);
        }
    }
}
