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
