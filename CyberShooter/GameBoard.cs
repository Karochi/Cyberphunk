using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace CyberShooter
{
    public class GameBoard
    {
        public Player Player { get; set; }
        int screenWidth, screenHeight;
        public static Map map;

        WeaponPickup weaponPickUp;
        ResourcePickup resourcePickUp;
        List<WeaponPickup> weaponPickupList;
        List<ResourcePickup> resourcePickupList;
        float playerPickUpDistance, shortestPickUpDistance;
        int pickUpIndex;

        public List<Projectile> projectileList, enemyProjectileList;

        public static int mapHeight = 20;
        public static int mapWidth = 20;
        public static int tileHeight = 32;
        public static int tileWidth = 32;
        public static Vector2 drawOffset = Vector2.Zero;
        public static int drawableLayer = 0;
        string loadFileName = "Text Files\\allHumanTest.txt";
        Vector2 collisionDist = Vector2.Zero;
        Vector2 normal;

        Random rnd = new Random();

        public List<WeaponPickup> GetWeaponPickUpList()
        {
            return weaponPickupList;
        }
        public List<ResourcePickup> GetResourcePickUpList()
        {
            return resourcePickupList;
        }
        public GameBoard(int screenWidth, int screenHeight)
        {
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            Player = new Player(new Vector2(100, 100));

            //Should be spawned through the map/other system later.
            weaponPickupList = new List<WeaponPickup>();
            weaponPickUp = new WeaponPickup(new Vector2(100, 500), PickUpTypes.handgun);
            weaponPickupList.Add(weaponPickUp);
            weaponPickUp = new WeaponPickup(new Vector2(100, 475), PickUpTypes.handgun);
            weaponPickupList.Add(weaponPickUp);
            weaponPickUp = new WeaponPickup(new Vector2(155, 510), PickUpTypes.rifle);
            weaponPickupList.Add(weaponPickUp);

            resourcePickupList = new List<ResourcePickup>();
            resourcePickUp = new ResourcePickup(new Vector2(500, 50), PickUpTypes.rifleAmmo);
            resourcePickupList.Add(resourcePickUp);
            resourcePickUp = new ResourcePickup(new Vector2(500, 400), PickUpTypes.health);
            resourcePickupList.Add(resourcePickUp);

            projectileList = new List<Projectile>();
            enemyProjectileList = new List<Projectile>();

            map = new Map(mapWidth, mapHeight, tileWidth, tileHeight);
            map.LoadMap(loadFileName);
            mapHeight = map.mapHeight;
            mapWidth = map.mapWidth;
            tileHeight = map.tileHeight;
            tileWidth = map.tileWidth;

            map = new Map(mapWidth, mapHeight, tileWidth, tileHeight);

            map.LoadMap(loadFileName);

            map.LoadTileSet(Game1.tileSheet);
            map.PopulateHostileHumanLayer();
            map.PopulateFriendlyHumanLayer();
            map.PopulateCollisionLayer();
        }
        public void Update(GameTime gameTime, Vector2 target)
        {
            Player.Update(gameTime, target, map.collisionRects, weaponPickupList, map.NPCs);
            //PlayerNPCCollision(gameTime);
            WeaponPickUpSelection();
            WeaponPickupCollection();
            ResourcePickupCollection();
            NPC(gameTime);
            WallCollision();
        }
        public void NPC(GameTime gameTime)
        {
            for (int i = 0; i < map.NPCs.Count(); i++)
            {
                if (!map.NPCs[i].IsDead)
                {
                    //map.NPCs[i].GetPlayerPos(Player);
                    if (map.NPCs[i].GetDirectionChangeCooldown() <= 0)
                    {
                        map.NPCs[i].SetDirectionX(rnd.Next(map.NPCs[i].GetMinDirectionX(), map.NPCs[i].GetMaxDirectionX()));
                        map.NPCs[i].SetDirectionY(rnd.Next(map.NPCs[i].GetMinDirectionY(), map.NPCs[i].GetMaxDirectionY()));
                        map.NPCs[i].NormalizeDirection();
                        map.NPCs[i].SetDirectionChangeCooldown(rnd.Next(1000, 2000));
                        map.NPCs[i].SetMovementCooldown(rnd.Next(200, 2000));
                        map.NPCs[i].SetVelocity(1f);
                    }
                    map.NPCs[i].Update(gameTime, Player, map.collisionRects);
                }
            }
        }
        public void WallCollision()
        {
            for (int i = 0; i < map.collisionRects.Count(); i++)
            {
                for (int j = 0; j < map.NPCs.Count(); j++)
                {
                    map.NPCs[j].CollisionCheck(map.collisionRects[i]);
                    if (map.NPCs[j].HitRect.Intersects(map.collisionRects[i]))
                    {
                        map.NPCs[j].SetVelocity(0);
                        map.NPCs[j].Position = map.NPCs[j].OldPosition;
                    }
                }
                if (Player.HitRect.Intersects(map.collisionRects[i]))
                {
                    Player.Position = Player.OldPosition;
                    Player.Speed = Vector2.Zero;
                }
                if (Player.leftRect.Intersects(map.collisionRects[i]))
                    Player.leftRectCollision = true;
                if (Player.rightRect.Intersects(map.collisionRects[i]))
                    Player.rightRectCollision = true;
                if (Player.topRect.Intersects(map.collisionRects[i]))
                    Player.topRectCollision = true;
                if (Player.bottomRect.Intersects(map.collisionRects[i]))
                    Player.bottomRectCollision = true;
            }
        }
       
        //public void PlayerNPCCollision(GameTime gameTime)
        //{
        //    foreach(NPC npc in map.NPCs)
        //    {
        //        if (Player.HitRect.Intersects(npc.HitRect) && !Player.IsDamaged && !npc.IsDead)
        //        {
        //            Player.Damage();
        //        }
        //    }
        //}
        public void WeaponPickUpSelection()
        {
            shortestPickUpDistance = float.MaxValue;
            pickUpIndex = -1;
            for (int i = 0; i < weaponPickupList.Count(); i++)
            {
                weaponPickupList[i].SetIsInteractable(false);
                playerPickUpDistance = Vector2.Distance(Player.GetPlayerCenter(), weaponPickupList[i].GetPickUpCenter());
                if (playerPickUpDistance < weaponPickupList[i].GetRadius())
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
                weaponPickupList[pickUpIndex].SetIsInteractable(true);
            }
        }
        public void WeaponPickupCollection()
        {
            foreach (WeaponPickup pickUp in weaponPickupList)
            {
                if (pickUp.GetIsInteractable() && KeyMouseReader.KeyPressed(Keys.E))
                {
                    if (Player.WeaponFullCheck())
                    {
                        Player.WeaponDrop(weaponPickupList);
                    }
                    pickUp.PickedUp(Player);
                    weaponPickupList.Remove(weaponPickupList[pickUpIndex]);
                    return;
                }
            }
        }
        public void ResourcePickupCollection()
        {
            foreach (ResourcePickup pickUp in resourcePickupList)
            {
                if (pickUp.HitRect.Intersects(Player.HitRect))
                {
                    if (pickUp.PickedUp(Player))
                    {
                        resourcePickupList.Remove(pickUp);
                        return;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            map.DrawMap(Player, weaponPickupList, resourcePickupList);
            foreach (WeaponPickup pickUp in GetWeaponPickUpList())
            {
                if (pickUp.GetIsInteractable())
                {
                    spriteBatch.DrawString(Game1.spriteFont, "E", new Vector2(pickUp.Position.X + pickUp.TexWidth / 2, pickUp.Position.Y - 10), Color.Black);
                }
            }
        }
    }
}
