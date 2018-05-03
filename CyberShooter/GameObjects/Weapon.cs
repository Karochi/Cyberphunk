using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberShooter
{
    public enum WeaponNames { unarmed, handgun, rifle};
    public enum WeaponTypes { melee, semiAuto, auto };

    public class Weapon
    {
        int damage, cooldown, originCooldown, projectileSpeed;
        float range;
        WeaponTypes weaponType;
        WeaponNames weaponName;
        PickupTypes pickUpType;

        public int GetDamage()
        {
            return damage;
        }
        public int GetCooldown()
        {
            return cooldown;
        }
        public void SetCooldown(int cooldown)
        {
            this.cooldown = cooldown;
        }
        public int GetOriginCooldown()
        {
            return originCooldown;
        }
        public int GetProjectileSpeed()
        {
            return projectileSpeed;
        }
        public float GetRange()
        {
            return range;
        }
        public WeaponTypes GetWeaponType()
        {
            return weaponType;
        }
        public WeaponNames GetWeaponName()
        {
            return weaponName;
        }
        public void SetWeaponName(WeaponNames weaponName)
        {
            this.weaponName = weaponName;
        }
        public PickupTypes GetPickUpType()
        {
            return pickUpType;
        }

        public Weapon(WeaponNames weaponName)
        {
            this.weaponName = weaponName;
            if (weaponName == WeaponNames.handgun)
                GunDefinition();
            if (weaponName == WeaponNames.rifle)
                RifleDefinition();
        }
        public void Update(GameTime gameTime)
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
            pickUpType = PickupTypes.handgun;
        }
        public void RifleDefinition()
        {
            weaponType = WeaponTypes.auto;
            damage = 10;
            range = 300;
            projectileSpeed = 15;
            originCooldown = 200;
            pickUpType = PickupTypes.rifle;
        }
        public void ShotgunDefinition()
        {
            weaponType = WeaponTypes.semiAuto;
            damage = 4;
            range = 200;
            projectileSpeed = 1;
            originCooldown = 500;
        }
    }
}
