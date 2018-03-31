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
            transform = Matrix.CreateTranslation(-position.X + view.Width / 2 - KeyMouseReader.mousePosition.X/4 + view.Width/8,
                -position.Y + (view.Height / 2) - KeyMouseReader.mousePosition.Y/4 +view.Height/8, 0);
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
