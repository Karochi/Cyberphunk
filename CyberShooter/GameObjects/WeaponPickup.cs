using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberShooter
{
    public class WeaponPickup : Pickup
    {
        public WeaponPickup(Vector2 position, PickUpTypes type) : base(position, type)
        {
            pickUpType = type;
            radius = 60;
        }
        public void PickedUp(Player player)
        {
            if (pickUpType == PickUpTypes.handgun)
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
