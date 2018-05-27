using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public ResourcePickup(Vector2 position, PickupTypes type) : base(position, type)
        {
            healthPickUpAmount = 2;
            radius = 20;
        }
        public bool PickedUp(Player player)
        {
            if (pickupType == PickupTypes.handgunAmmo)
            {
                ammoPickUpAmount = 20;
                player.handgunAmmo += ammoPickUpAmount;
                return true;
            }
            if(pickupType == PickupTypes.rifleAmmo)
            {
                ammoPickUpAmount = 20;
                player.rifleAmmo += ammoPickUpAmount;
                return true;
            }
            if(pickupType == PickupTypes.health)
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
        public override void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            if (pickupType == PickupTypes.handgunAmmo || pickupType == PickupTypes.rifleAmmo)
                spriteBatch.Draw(Game1.ammoTex, HitRect, Color.White);
            else if(pickupType == PickupTypes.health)
                spriteBatch.Draw(Game1.healthTex, HitRect, Color.White);
        }
    }
}
