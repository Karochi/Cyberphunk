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
        /// <summary>
        /// Creates an instance of the Camera class.
        /// </summary>
        /// <param name="view">A Viewport used to calculate the view transform</param>
        public Camera(Viewport view)
        {
            this.view = view;
        }
        /// <summary>
        /// Sets the position of the camera. The camera will be centered around the given vector.
        /// </summary>
        public void SetPosition(Vector2 position)
        {
            this.position = position;
            transform = Matrix.CreateTranslation(-position.X + view.Width / 2, -position.Y + view.Height / 2, 0);
        }
        /// <summary>
        /// Gets the position of the camera.
        /// </summary>
        public Vector2 GetPosition()
        {
            return position;
        }
        /// <summary>
        /// Gets the Camera transform.
        /// </summary>
        public Matrix GetTransform()
        {
            return transform;
        }
    }
}
