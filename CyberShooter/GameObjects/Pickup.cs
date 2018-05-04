using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberShooter
{
    public enum PickupTypes { handgunAmmo, rifleAmmo, health, handgun, rifle};

    abstract public class Pickup : AnimatedGameObject
    {
        Vector2 pickupCenter;
        protected PickupTypes pickupType;
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
            return pickupCenter;
        }
        public float GetRadius()
        {
            return radius;
        }

        public Pickup(Vector2 position, PickupTypes type) : base()
        {
            Position = position;
            pickupType = type;
            isInteractable = false;
            TexWidth = 32;
            TexHeight = 32;
            pickupCenter = new Vector2(position.X + TexWidth / 2, position.Y + TexHeight / 2);
            base.Update();
        }


    }
}
