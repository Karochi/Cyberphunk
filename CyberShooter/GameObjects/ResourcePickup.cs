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
            healthPickUpAmount = 2;
            radius = 20;
        }
        public bool PickedUp(Player player)
        {
            if (pickUpType == PickUpTypes.handgunAmmo)
            {
                ammoPickUpAmount = 20;
                player.handgunAmmo += ammoPickUpAmount;
                return true;
            }
            if(pickUpType == PickUpTypes.rifleAmmo)
            {
                ammoPickUpAmount = 10;
                player.rifleAmmo += ammoPickUpAmount;
                return true;
            }
            if(pickUpType == PickUpTypes.health)
            {
                if(player.CurrHealth < player.MaxHealth)
                {
                    player.CurrHealth += healthPickUpAmount;
                    if (player.CurrHealth > player.MaxHealth)
                        player.CurrHealth = player.MaxHealth;
                    return true;
                }
                else return false;
            }
            else return false;
        }
    }
}
