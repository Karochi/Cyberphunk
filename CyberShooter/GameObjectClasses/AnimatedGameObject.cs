using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberShooter
{
    public class AnimatedGameObject : GameObject
    {

        protected int frame;
        int timer;


        public void Animation() {

          
           



        }
        public AnimatedGameObject() : base()

        {
            timer = 0;
            frame = 0;
        }
        public override void Update()
        {
            if (timer >= 30) {
                
                if (frame == 2)
                {
                    frame = 0;

                }
                else {
                    frame++;
                }

                timer = 0;
            }
            timer++;

            base.Update();
        }
    }
}
