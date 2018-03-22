using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberphunk
{
    class Player : GameObject
    {
        public Player(Vector2 position) : base()
        {
            this.position = position;
            texHeight = 10;
            texWidth = 10;
        }
        public override void Update()
        {
            if (isActive)
            {
                hitRect = new Rectangle((int)position.X, (int)position.Y, texWidth, texHeight);
                PlayerMovement();
            }
        }
        public void PlayerMovement()
        {
            if (KeyMouseReader.KeyHeld(Keys.W))
            {
                position.Y--;
            }
            if (KeyMouseReader.KeyHeld(Keys.A))
            {
                position.X--;
            }
            if (KeyMouseReader.KeyHeld(Keys.S))
            {
                position.Y++;
            }
            if (KeyMouseReader.KeyHeld(Keys.D))
            {
                position.X++;
            }
        }
    }
}
