using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberShooter
{
    public class ImageEffect
    {
        protected Image image;
        public bool IsActive;

        public ImageEffect()
        {
            IsActive = true;
        }
        public virtual void LoadContent(ref Image Image)
        {
            this.image = Image;
        }
        public virtual void UnloadContent()
        {

        }
        public virtual void Update(GameTime gameTime)
        {

        }
    }
}
