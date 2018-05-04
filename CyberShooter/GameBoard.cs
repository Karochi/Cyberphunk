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

        ResourcePickup resourcePickUp;
        public List<ResourcePickup> ResourcePickupList { get; private set; }
        float playerPickUpDistance, shortestPickUpDistance;
        int pickUpIndex;

        public static int mapHeight = 20;
        public static int mapWidth = 20;
        public static int tileHeight = 32;
        public static int tileWidth = 32;
        public static Vector2 drawOffset = Vector2.Zero;
        public static int drawableLayer = 0;
        string loadFileName = "Text Files\\Level1.txt";
        Random rnd = new Random();

        public GameBoard(int screenWidth, int screenHeight)
        {
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            Player = new Player(new Vector2(100, 100));

            ResourcePickupList = new List<ResourcePickup>();

            map = new Map(mapWidth, mapHeight, tileWidth, tileHeight);
            map.LoadMap(loadFileName);
            mapHeight = map.mapHeight;
            mapWidth = map.mapWidth;
            tileHeight = map.tileHeight;
            tileWidth = map.tileWidth;

            map = new Map(mapWidth, mapHeight, tileWidth, tileHeight);

            map.LoadMap(loadFileName);

            map.LoadTileSet(Game1.tileSheet);
            map.PopulateLootLayer();
            map.PopulateHostileHumanLayer();
            map.PopulateFriendlyHumanLayer();
            map.PopulateCollisionLayer();
            map.PopulateFriendlyRobotLayer();
            map.PopulateHostileRobotLayer();

        }
        public void Update(GameTime gameTime, Vector2 crosshairPos)
        {
            ResourcePickupCollection();
            Player.Update();
            Player.Update2(gameTime, crosshairPos, map.collisionRects, map.weaponPickups, map.NPCs);
            WallCollision();
            NPC(gameTime);
            WeaponPickUpSelection();
            WeaponPickupCollection();
        }
        public void NPC(GameTime gameTime)
        {
            for (int i = 0; i < map.NPCs.Count(); i++)
            {
                if (!map.NPCs[i].IsDead)
                {
                    if (map.NPCs[i].DirectionChangeCooldown <= 0)
                    {
                        map.NPCs[i].DirectionX = (rnd.Next(map.NPCs[i].MinDirectionX, map.NPCs[i].MaxDirectionX));
                        map.NPCs[i].DirectionY = (rnd.Next(map.NPCs[i].MinDirectionY, map.NPCs[i].MaxDirectionY));
                        map.NPCs[i].NormalizeDirection();
                        map.NPCs[i].DirectionChangeCooldown = rnd.Next(1000, 2000);
                        map.NPCs[i].MovementCooldown = rnd.Next(200, 2000);
                        map.NPCs[i].Velocity= 1f;
                    }
                    map.NPCs[i].Update(gameTime, Player, map.collisionRects);
                }
                else if (map.NPCs[i].IsDead)
                {
                    map.NPCs[i].DropCheck(rnd.Next(1, 100), Player, ResourcePickupList);
                }
                map.NPCs[i].ProjectileUpdate(map.NPCs[i].ProjectileList);
            }
        }
        public void WallCollision()
        {
            for (int i = 0; i < map.collisionRects.Count(); i++)
            {
                foreach (NPC npc in map.NPCs)
                {
                    npc.CollisionCheck(map.collisionRects[i]);
                    if (npc.HitRect.Intersects(map.collisionRects[i]))
                    {
                        npc.Velocity = 0;
                        npc.Position = npc.OldPosition;
                    }
                }
                if (Player.HitRect.Intersects(map.collisionRects[i]))
                {
                    Player.Position = Player.OldPosition;
                    Player.Speed = Vector2.Zero;
                }
            }
        }
        public void WeaponPickUpSelection()
        {
            shortestPickUpDistance = float.MaxValue;
            pickUpIndex = -1;
            for (int i = 0; i < map.weaponPickups.Count(); i++)
            {
                map.weaponPickups[i].SetIsInteractable(false);
                playerPickUpDistance = Vector2.Distance(Player.GetPlayerCenter(), map.weaponPickups[i].GetPickUpCenter());
                if (playerPickUpDistance < map.weaponPickups[i].GetRadius())
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
                map.weaponPickups[pickUpIndex].SetIsInteractable(true);
            }
        }
        public void WeaponPickupCollection()
        {
            foreach (WeaponPickup pickUp in map.weaponPickups)
            {
                if (pickUp.GetIsInteractable() && KeyMouseReader.KeyPressed(Keys.E))
                {
                    if (Player.WeaponFullCheck())
                    {
                        Player.WeaponDrop(map.weaponPickups);
                    }
                    pickUp.PickedUp(Player);
                    map.weaponPickups.Remove(map.weaponPickups[pickUpIndex]);
                    return;
                }
            }
        }
        public void ResourcePickupCollection()
        {
            foreach (ResourcePickup pickUp in ResourcePickupList)
            {
                if (pickUp.HitRect.Intersects(Player.HitRect))
                {
                    if (pickUp.PickedUp(Player))
                    {
                        ResourcePickupList.Remove(pickUp);
                        return;
                    }
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            map.DrawMap(Player, ResourcePickupList);
            foreach (WeaponPickup pickUp in map.weaponPickups)
            {
                if (pickUp.GetIsInteractable())
                {
                    spriteBatch.DrawString(Game1.spriteFont, "E", new Vector2(pickUp.Position.X + pickUp.TexWidth / 2, pickUp.Position.Y - 10), Color.White);
                }
            }
        }
    }
}
