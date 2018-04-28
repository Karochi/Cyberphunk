using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace CyberShooter
{
    public class WeaponPickup : Pickup
    {
        public bool isDropped = false;

        public WeaponPickup(Vector2 position, PickupTypes type) : base(position, type)
        {
            pickupType = type;
            radius = 60;
        }
        public void PickedUp(Player player)
        {
            if (pickupType == PickupTypes.handgun)
                WeaponManage(player, WeaponNames.handgun);
            if (pickupType == PickupTypes.rifle)
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
        public override void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            if (!isDropped)
            {
                spriteBatch.Draw(texture, HitRect, Color.Yellow);
            }
            else if (isDropped)
            {
                if(pickupType == PickupTypes.handgun)
                {
                    spriteBatch.Draw(texture, HitRect, Color.Red);
                }
                else if(pickupType == PickupTypes.rifle)
                {
                    spriteBatch.Draw(texture, HitRect, Color.Green);
                }
            }
        }
    }
}
