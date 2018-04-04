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
        public Vector2 pickUpCenter;
        public PickUpTypes pickUpType;
        public float radius;
        public bool isInteractable;

        public Pickup(Vector2 position, PickUpTypes type) : base()
        {
            this.position = position;
            this.pickUpType = type;
            isInteractable = false;
            radius = 60;
            texHeight = 10;
            texWidth = 10;
            pickUpCenter = new Vector2(position.X + texWidth / 2, position.Y + texHeight / 2);
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
            if (player.firstWeapon.weaponName == WeaponNames.unarmed)
                player.firstWeapon = new Weapon(weaponName);

            else if (player.secondWeapon.weaponName == WeaponNames.unarmed)
            {
                player.WeaponSwap();
                player.firstWeapon = new Weapon(weaponName);
            }
            else if (player.firstWeapon.weaponName != WeaponNames.unarmed && player.secondWeapon.weaponName != WeaponNames.unarmed)
            {
                player.firstWeapon = new Weapon(weaponName);
            }
        }
    }
}
