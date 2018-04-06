﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace CyberShooter
{
    class GameBoard
    {
        Player player;
        NPC testNPC;
        int screenWidth, screenHeight;
        public static Map map;

        Pickup testPickUp;
        List<Pickup> pickUpList;
        float playerPickUpDistance, shortestPickUpDistance;
        int pickUpIndex;

        public List<Projectile> projectileList;

        public static int mapHeight = 20;
        public static int mapWidth = 20;
        public static int tileHeight = 32;
        public static int tileWidth = 32;
        public static Vector2 drawOffset = Vector2.Zero;
        public static int drawableLayer = 0;
        string loadFileName = "testest.txt";
        Vector2 collisionDist = Vector2.Zero;
        Vector2 normal;

        public Player GetPlayer()
        {
            return this.player;
        }
        public List<Pickup> GetPickUpList()
        {
            return this.pickUpList;
        }
        public GameBoard(int screenWidth, int screenHeight)
        {
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            player = new Player(new Vector2(100, 200));
            testNPC = new NPC(new Vector2(100,100));

            //Should be spawned through the map/other system later.
            pickUpList = new List<Pickup>();
            testPickUp = new Pickup(new Vector2(100, 500), PickUpTypes.handgun);
            pickUpList.Add(testPickUp);
            testPickUp = new Pickup(new Vector2(100, 475), PickUpTypes.handgun);
            pickUpList.Add(testPickUp);
            testPickUp = new Pickup(new Vector2(155, 510), PickUpTypes.rifle);
            pickUpList.Add(testPickUp);
            testPickUp = new Pickup(new Vector2(500, 50), PickUpTypes.ammo);
            pickUpList.Add(testPickUp);
            projectileList = new List<Projectile>();

            map = new Map(mapWidth, mapHeight, tileWidth, tileHeight);
            map.LoadMap(loadFileName);
            mapHeight = map.mapHeight;
            mapWidth = map.mapWidth;
            tileHeight = map.tileHeight;
            tileWidth = map.tileWidth;

            map = new Map(mapWidth, mapHeight, tileWidth, tileHeight);

            map.LoadMap(loadFileName);

            map.LoadTileSet(Game1.tileSheet);
            map.PopulateCollisionLayer();
        }
        public void Update(GameTime gameTime, Vector2 target)
        {
            player.Update(gameTime, target, this);
            NPCCollision();
            PickUpSelection();
            PickUpCollection();
            ProjectileUpdate();
            ProjectileNPCCollision();

            for (int i = 0; i < map.collisionRects.Count(); i++)
            {
                testNPC.CollisionCheck(map.collisionRects[i]);
                if (player.GetHitRect().Intersects(map.collisionRects[i]))
                {
                    player.SetPosition(player.GetOldPosition());
                    player.SetSpeed(new Vector2(0, 0));
                }
                ProjectileWallCollision(i);
            }
            testNPC.GetPlayerPos(player);
            testNPC.Update();
        }
        public void ProjectileUpdate()
        {
            foreach (Projectile projectile in projectileList)
                projectile.Update();

            foreach (Projectile projectile in projectileList)
            {
                if (Vector2.Distance(projectile.GetOriginPosition(), projectile.GetPosition()) >= projectile.GetRange())
                {
                    projectileList.Remove(projectile);
                    return;
                }
            }
        }
        public void ProjectileWallCollision(int i)
        {
            foreach(Projectile projectile in projectileList)
            {
                if (projectile.GetHitRect().Intersects(map.collisionRects[i]))
                {
                    projectileList.Remove(projectile);
                    return;
                }
            }
        }
        public void ProjectileNPCCollision()
        {
            foreach(Projectile projectile in projectileList)
            {
                if (projectile.GetHitRect().Intersects(testNPC.GetHitRect()))
                {
                    projectileList.Remove(projectile);
                    return;
                }
            }
        }
        public void NPCCollision()
        {
            if(player.GetHitRect().Intersects(testNPC.GetHitRect()))
            {
                player.SetPosition(player.GetOldPosition());
                player.SetSpeed(new Vector2(0, 0));
                testNPC.SetSpeed(new Vector2(0, 0));
            }
        }
        public void PickUpSelection()
        {
            shortestPickUpDistance = float.MaxValue;
            pickUpIndex = -1;
            for (int i = 0; i < pickUpList.Count(); i++)
            {
                pickUpList[i].SetIsInteractable(false);
                playerPickUpDistance = Vector2.Distance(player.GetPlayerCenter(), pickUpList[i].GetPickUpCenter());
                if (playerPickUpDistance < pickUpList[i].GetRadius())
                {
                    if (playerPickUpDistance < shortestPickUpDistance)
                    {
                        shortestPickUpDistance = playerPickUpDistance;
                        pickUpIndex = i;
                    }
                }
            }
            if (pickUpIndex >= 0)
            {
                pickUpList[pickUpIndex].SetIsInteractable(true);
            }
        }
        public void PickUpCollection()
        {
            foreach (Pickup pickUp in pickUpList)
            {
                if (pickUp.GetIsInteractable() && KeyMouseReader.KeyPressed(Keys.E))
                {
                    if (WeaponFullCheck() && pickUp.GetIsWeapon())
                    {
                        WeaponDrop();
                    }
                    pickUp.PickedUp(player);
                    pickUpList.Remove(pickUpList[pickUpIndex]);
                    return;
                }
            }
        }
        public bool WeaponFullCheck()
        {
            if (player.GetFirstWeapon().GetWeaponName() != WeaponNames.unarmed && player.GetSecondWeapon().GetWeaponName() != WeaponNames.unarmed)
                return true;
            else return false;
        }
        public void WeaponDrop()
        {
            GetPickUpList().Add(new Pickup(GetPlayer().GetPosition(), GetPlayer().GetFirstWeapon().GetPickUpType()));
            GetPlayer().GetFirstWeapon().SetWeaponName(WeaponNames.unarmed);
        }
        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            map.DrawMap();
            player.Draw(spriteBatch, texture);
            foreach(Projectile projectile in projectileList)
            {
                projectile.Draw(spriteBatch, texture);
            }
            foreach(Pickup pickup in pickUpList)
            {
                pickup.Draw(spriteBatch, texture);
            }
            testNPC.Draw(spriteBatch, texture);
        }
    }
}
