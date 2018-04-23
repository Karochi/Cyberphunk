using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CyberShooter
{
    class Camera
    {
        /// spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetTransform());
        private Matrix transform;
        private Vector2 position;
        private Viewport view;

        public Camera(Viewport view)
        {
            this.view = view;
        }
        public void SetPosition(Vector2 position)
        {
            this.position = position;
            Vector2 lockedMousePosition = KeyMouseReader.mousePosition;
            if (KeyMouseReader.mousePosition.X > view.Width)
                lockedMousePosition.X = view.Width;
            if(KeyMouseReader.mousePosition.X < 0)
                lockedMousePosition.X = 0;
            if (KeyMouseReader.mousePosition.Y > view.Height)
                lockedMousePosition.Y = view.Height;
            if (KeyMouseReader.mousePosition.Y < 0)
                lockedMousePosition.Y = 0;
            transform = Matrix.CreateTranslation(-(int)position.X + view.Width / 2 - (int)lockedMousePosition.X / 4 + view.Width / 8,
                -(int)position.Y + (view.Height / 2) - (int)lockedMousePosition.Y / 4 + view.Height / 8, 0);
        }
        public Vector2 GetPosition()
        {
            return position;
        }
        public Matrix GetTransform()
        {
            return transform;
        }
    }
}
