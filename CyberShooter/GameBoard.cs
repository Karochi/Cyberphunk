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
    class GameBoard
    {
        public Player player;
        public NPC testNPC;
        int screenWidth, screenHeight;
        public static Map map;

        public Pickup testPickUp;
        public List<Pickup> pickUpList;
        public float playerPickUpDistance, shortestPickUpDistance;
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

        public GameBoard(int screenWidth, int screenHeight)
        {
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            player = new Player(new Vector2(100, 200));
            testNPC = new NPC(new Vector2(100,100));

            pickUpList = new List<Pickup>();
            testPickUp = new Pickup(new Vector2(100, 500), PickUpTypes.handgun);
            pickUpList.Add(testPickUp);
            testPickUp = new Pickup(new Vector2(100, 475), PickUpTypes.handgun);
            pickUpList.Add(testPickUp);
            testPickUp = new Pickup(new Vector2(155, 510), PickUpTypes.rifle);
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
            testNPC.Update();
            NPCCollision();
            PickUpSelection();
            PickUpCollection();

            foreach (Projectile projectile in projectileList)
            {
                projectile.Update();
            }

            for (int i = 0; i < map.collisionRects.Count(); i++)
            {
                if (player.hitRect.Intersects(map.collisionRects[i]))
                {
                    player.position = player.oldPosition;
                    player.speed = new Vector2(0, 0);
                }
            }
        }
        public void NPCCollision()
        {
            if(player.hitRect.Intersects(testNPC.hitRect))
            {
                player.position = player.oldPosition;
                player.speed = new Vector2(0,0);
            }
        }
        public void PickUpSelection()
        {
            shortestPickUpDistance = float.MaxValue;
            pickUpIndex = -1;
            for (int i = 0; i < pickUpList.Count(); i++)
            {
                pickUpList[i].isInteractable = false;
                playerPickUpDistance = Vector2.Distance(player.playerCenter, pickUpList[i].pickUpCenter);
                if (playerPickUpDistance < pickUpList[i].radius)
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
                pickUpList[pickUpIndex].isInteractable = true;
            }
        }
        public void PickUpCollection()
        {
            foreach (Pickup pickUp in pickUpList)
            {
                if (pickUp.isInteractable && KeyMouseReader.KeyPressed(Keys.E))
                {
                    if (player.firstWeapon.weaponName != WeaponNames.unarmed && player.secondWeapon.weaponName != WeaponNames.unarmed)
                        WeaponDrop();
                    pickUp.PickedUp(player);
                    pickUpList.Remove(pickUpList[pickUpIndex]);
                    return;
                }
            }
        }
        public void WeaponDrop()
        {
            pickUpList.Add(new Pickup(player.position, player.firstWeapon.pickUpType));
            player.firstWeapon.weaponName = WeaponNames.unarmed;
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
