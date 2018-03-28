using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberShooter
{
    class MovingGameObject : AnimatedGameObject
    {
        public Vector2 speed, oldPosition;

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
            oldPosition = position;
            position = position + speed;
        }
    }
}
