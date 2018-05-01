using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberShooter
{
    public class Button
    {
        public bool clicked = false;
        public bool prevClicked = false;
        bool hover = false;
        Texture2D texture;
        public Vector2 position;
        Rectangle collisionRect;
        public int width, height;
        SoundEffect hoverSound, clickSound;
        bool played;

        public Rectangle CollisionRect { get => collisionRect; set => collisionRect = value; }

        public Button(Texture2D tex, Vector2 pos, int height, int width, SoundEffect hoverSound, SoundEffect clickSound)
        {
            texture = tex;
            position = pos;
            this.hoverSound = hoverSound;
            this.clickSound = clickSound;
            this.width = width;
            this.height = height;
        }
        public void Update()
        {
            CollisionRect = new Rectangle((int)position.X, (int)position.Y, width, height);

            if (CollisionRect.Contains(KeyMouseReader.mousePosition.X, KeyMouseReader.mousePosition.Y))
            {
                if (!played)
                {
                    hoverSound.Play();
                    played = true;
                }
                hover = true;
            }
            else
            {
                hover = false;
                played = false;
            }

            if (CollisionRect.Contains(KeyMouseReader.mousePosition.X, KeyMouseReader.mousePosition.Y) && KeyMouseReader.mouseState.LeftButton == ButtonState.Pressed && !prevClicked)
            {
                clickSound.Play();
                clicked = true;
            }
            else
                clicked = false;

            prevClicked = prevClicked || clicked;
        }
        public void Draw()
        {
            if (hover)
            {
                Game1.spriteBatch.Draw(texture, CollisionRect, Color.White);
            }
            else
                Game1.spriteBatch.Draw(texture, CollisionRect, Color.Purple);
        }
    }
}
