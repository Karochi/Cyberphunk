using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberShooter
{
    public enum PickUpTypes { handgunAmmo, rifleAmmo, health, handgun, rifle};

    abstract public class Pickup : AnimatedGameObject
    {
        Vector2 pickUpCenter;
        protected PickUpTypes pickUpType;
        protected float radius;
        bool isInteractable;

        public bool GetIsInteractable()
        {
            return isInteractable;
        }
        public void SetIsInteractable(bool isInteractable)
        {
            this.isInteractable = isInteractable;
        }
        public Vector2 GetPickUpCenter()
        {
            return pickUpCenter;
        }
        public float GetRadius()
        {
            return radius;
        }

        public Pickup(Vector2 position, PickUpTypes type) : base()
        {
            Position = position;
            pickUpType = type;
            isInteractable = false;
            TexWidth = 10;
            TexHeight = 10;
            pickUpCenter = new Vector2(position.X + TexWidth / 2, position.Y + TexHeight / 2);
            base.Update();
        }


    }
}
