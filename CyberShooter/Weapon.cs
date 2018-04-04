using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberShooter
{
    public enum WeaponNames { unarmed, knife, handgun, rifle };
    public enum WeaponTypes { melee, semiAuto, auto };

    class Weapon
    {
        public int damage, cooldown, originCooldown, projectileSpeed;
        public float range;
        public WeaponTypes weaponType;
        public WeaponNames weaponName;
        public PickUpTypes pickUpType;

        public Weapon(WeaponNames weaponName)
        {
            this.weaponName = weaponName;
            if (weaponName == WeaponNames.handgun)
                GunDefinition();
            if (weaponName == WeaponNames.rifle)
                RifleDefinition();
        }
        public void Update(GameTime gameTime, GameBoard gameBoard)
        {
            cooldown -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
        }
        public void GunDefinition()
        {
            weaponType = WeaponTypes.semiAuto;
            damage = 5;
            range = 200;
            projectileSpeed = 10;
            originCooldown = 500;
            pickUpType = PickUpTypes.handgun;
        }
        public void RifleDefinition()
        {
            weaponType = WeaponTypes.auto;
            damage = 1;
            range = 500;
            projectileSpeed = 15;
            originCooldown = 200;
            pickUpType = PickUpTypes.rifle;
        }
    }
}
