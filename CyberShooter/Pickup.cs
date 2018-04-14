using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberShooter
{
    public enum PickUpTypes { ammo, health, handgun, rifle};

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
            SetPosition(position);
            pickUpType = type;
            isInteractable = false;
            SetTexHeight(10);
            SetTexWidth(10);
            pickUpCenter = new Vector2(position.X + GetTexWidth() / 2, position.Y + GetTexHeight() / 2);
            base.Update();
        }


    }
}
