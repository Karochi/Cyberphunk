using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberShooter
{
    public class ResourcePickup : Pickup
    {
        int ammoPickUpAmount, healthPickUpAmount;

        public ResourcePickup(Vector2 position, PickUpTypes type) : base(position, type)
        {
            ammoPickUpAmount = 10;
            healthPickUpAmount = 2;
            radius = 20;
        }
        public void PickedUp(Player player)
        {
            if (pickUpType == PickUpTypes.ammo)
            {
                player.SetAmmo(player.GetAmmo() + ammoPickUpAmount);
            }
            if(pickUpType == PickUpTypes.health)
            {
                player.SetCurrHealth(player.GetCurrHealth() + healthPickUpAmount);
                if (player.GetCurrHealth() > player.GetMaxHealth())
                    player.SetCurrHealth(player.GetMaxHealth());
            }
        }
    }
}
