using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    public class Button
    {
        public bool clicked = false;
        public bool prevClicked = false;
        bool hover = false;
        Texture2D texture;
        public Vector2 position;
        Rectangle collisionRect;
        int width, height;

        public Button(Texture2D tex, Vector2 pos)
        {
            this.texture = tex;
            this.position = pos;

            width = 110;
            height = 50;
        }
        public virtual void Update()
        {
            collisionRect = new Rectangle((int)position.X, (int)position.Y, width, height);

            MouseState mouse = Mouse.GetState() ;

            if (collisionRect.Contains(mouse.X, mouse.Y))
                hover = true;
            else
                hover = false;

            if (collisionRect.Contains(mouse.X, mouse.Y) && mouse.LeftButton == ButtonState.Pressed && !prevClicked)
                clicked = true;
            else
                clicked = false;

            prevClicked = prevClicked || clicked;
        }
        public virtual void Effect()
        {

        }
        public void Draw()
        {
            if(hover)
            {
                Game1.spriteBatch.Draw(texture, collisionRect, Color.White);
                Game1.spriteBatch.Draw(texture, collisionRect, new Color(255,0,0,180));
            }
            else
                Game1.spriteBatch.Draw(texture, collisionRect, Color.White);
        }
    }
}
