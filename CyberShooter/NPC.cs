using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberShooter
{
    class NPC : MovingGameObject
    {
        public NPC(Vector2 position) : base()
        {
            this.position = position;
            texHeight = 40;
            texWidth = 30;
        }
        public override void Update()
        {
            base.Update();
        }
    }
}
