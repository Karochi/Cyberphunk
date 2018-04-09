﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberShooter
{
    public class HUD
    {
        Rectangle currHealthRect, maxHealthRect;
        Vector2 currHealthBarPos = new Vector2(15, 16);
        Vector2 maxHealthBarPos = new Vector2(10,10);
        Vector2 healthTextPos = new Vector2(70, 10);
        Vector2 firstWeaponPos = new Vector2(10, 50);
        Vector2 secondWeaponPos = new Vector2(100, 50);
        int currHealth, maxHealth;
        int ampHealthSize = 20;
        string currHealthText, maxHealthText;

        int hudWeaponHeight = 30;
        int hudWeaponWidth = 30;
        int hudLongWeaponWidth = 40;


        public HUD(Player player)
        {
            maxHealth = player.GetMaxHealth();
            currHealth = player.GetCurrHealth();
        }
        public void Update(Player player)
        {
            maxHealth = player.GetMaxHealth();
            currHealth = player.GetCurrHealth();

            maxHealthRect = new Rectangle((int)maxHealthBarPos.X, (int)maxHealthBarPos.Y,maxHealth * ampHealthSize, 30);
            currHealthRect = new Rectangle((int)currHealthBarPos.X, (int)currHealthBarPos.Y, currHealth * ampHealthSize - 9, 17);

            maxHealthText = maxHealth.ToString();
            currHealthText = currHealth.ToString();

        }
        public void Draw(Player player)
        {
            Game1.spriteBatch.Draw(Game1.healthBarTex, maxHealthRect, Color.White);
            Game1.spriteBatch.Draw(Game1.square, currHealthRect, Color.Green);
            Game1.spriteBatch.DrawString(Game1.spriteFont, currHealthText + "/" + maxHealth, healthTextPos, Color.White);
            if(player.GetFirstWeapon().GetWeaponName() == WeaponNames.unarmed && player.GetSecondWeapon().GetWeaponName() == WeaponNames.unarmed)
            {
                Game1.spriteBatch.Draw(Game1.unarmedTex, new Rectangle((int)firstWeaponPos.X, (int)firstWeaponPos.Y, hudWeaponWidth, hudWeaponHeight), Color.White);
            }
            else
            {
                //Draw first weapon
                if (player.GetFirstWeapon().GetWeaponName() == WeaponNames.unarmed)
                    Game1.spriteBatch.Draw(Game1.unarmedTex, new Rectangle((int)firstWeaponPos.X, (int)firstWeaponPos.Y, hudWeaponWidth, hudWeaponHeight), Color.White);
                else if (player.GetFirstWeapon().GetWeaponName() == WeaponNames.handgun)
                    Game1.spriteBatch.Draw(Game1.handgunTex, new Rectangle((int)firstWeaponPos.X, (int)firstWeaponPos.Y, hudWeaponWidth, hudWeaponHeight), Color.White);
                else if (player.GetFirstWeapon().GetWeaponName() == WeaponNames.rifle)
                    Game1.spriteBatch.Draw(Game1.rifleTex, new Rectangle((int)firstWeaponPos.X, (int)firstWeaponPos.Y, hudLongWeaponWidth, hudWeaponHeight), Color.White);
                //Draw second weapon
                if (player.GetSecondWeapon().GetWeaponName() == WeaponNames.unarmed)
                    Game1.spriteBatch.Draw(Game1.unarmedTex, new Rectangle((int)secondWeaponPos.X, (int)secondWeaponPos.Y, hudWeaponWidth, hudWeaponHeight), Color.White);
                else if (player.GetSecondWeapon().GetWeaponName() == WeaponNames.handgun)
                    Game1.spriteBatch.Draw(Game1.handgunTex, new Rectangle((int)secondWeaponPos.X, (int)secondWeaponPos.Y, hudWeaponWidth, hudWeaponHeight), Color.White);
                else if (player.GetSecondWeapon().GetWeaponName() == WeaponNames.rifle)
                    Game1.spriteBatch.Draw(Game1.rifleTex, new Rectangle((int)secondWeaponPos.X, (int)secondWeaponPos.Y, hudLongWeaponWidth, hudWeaponHeight), Color.White);

            }
        }
    }
}
