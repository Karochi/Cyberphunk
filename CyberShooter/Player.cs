using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberShooter
{
    public enum WeaponStates { unarmed, gun };

    class Player : MovingGameObject
    {
        public WeaponStates weaponState;
        public Vector2 mapPosition;

        public Player(int screenWidth, int screenHeight) : base()
        {
            weaponState = WeaponStates.unarmed;
            hitBoxHeight = 30;
            hitBoxWidth = 30;
            mapPosition.X = ((float)screenWidth / 2) - (hitBoxWidth / 2);
            mapPosition.Y = ((float)screenHeight / 2) - (hitBoxHeight / 2);
        }
        public override void Update()
        {
            base.Update();
            Moving();
            StoppingX();
            StoppingY();
        }
        public void Moving()
        {
            if (speed.X >= (-3) && KeyMouseReader.KeyHeld(Keys.A))
            {
                speed.X -= 0.2f;
            }
            else if (speed.X <= 3 && KeyMouseReader.KeyHeld(Keys.D))
            {
                speed.X += 0.2f;
            }
            else if (speed.Y >= (-3) && KeyMouseReader.KeyHeld(Keys.W))
            {
                speed.Y -= 0.2f;
            }
            else if (speed.Y <= 3 && KeyMouseReader.KeyHeld(Keys.S))
            {
                speed.Y += 0.2f;
            }
        }
        public void StoppingX()
        {
            if (!KeyMouseReader.KeyHeld(Keys.D) && !KeyMouseReader.KeyHeld(Keys.A))
            {
                if (speed.X < 0.2f && speed.X > (-0.2f))
                {
                    speed.X = 0;
                }
                if (speed.X > 0)
                {
                    speed.X -= 0.2f;
                }
                if (speed.X < 0)
                {
                    speed.X += 0.2f;
                }
            }
        }
        public void StoppingY()
        {
            if(!KeyMouseReader.KeyHeld(Keys.W) && !KeyMouseReader.KeyHeld(Keys.S))
            {
                if (speed.Y < 0.2f && speed.Y > (-0.2f))
                {
                    speed.Y = 0;
                }
                if (speed.Y > 0)
                {
                    speed.Y -= 0.2f;
                }
                if (speed.Y < 0)
                {
                    speed.Y += 0.2f;
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, hitRect, Color.White);
        }
    }
}
