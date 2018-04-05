using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberShooter
{
    public enum PickUpTypes{ handgun, rifle, ammo, health };

    class Pickup : AnimatedGameObject
    {
        Vector2 pickUpCenter;
        PickUpTypes pickUpType;
        float radius;
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
            this.pickUpType = type;
            isInteractable = false;
            radius = 60;
            SetTexHeight(10);
            SetTexWidth(10);
            pickUpCenter = new Vector2(position.X + GetTexWidth() / 2, position.Y + GetTexHeight() / 2);
            base.Update();
        }
        public void PickedUp(Player player)
        {
            if(pickUpType == PickUpTypes.handgun)
                WeaponManage(player, WeaponNames.handgun);
            if (pickUpType == PickUpTypes.rifle)
                WeaponManage(player, WeaponNames.rifle);
        }
        public void WeaponManage(Player player, WeaponNames weaponName)
        {
            if (player.GetFirstWeapon().GetWeaponName() == WeaponNames.unarmed)
                player.SetFirstWeapon(new Weapon(weaponName));

            else if (player.GetSecondWeapon().GetWeaponName() == WeaponNames.unarmed)
            {
                player.WeaponSwap();
                player.SetFirstWeapon(new Weapon(weaponName));
            }
            else if (player.GetFirstWeapon().GetWeaponName() != WeaponNames.unarmed && player.GetSecondWeapon().GetWeaponName() != WeaponNames.unarmed)
            {
                player.SetFirstWeapon(new Weapon(weaponName));
            }
        }
    }
}
