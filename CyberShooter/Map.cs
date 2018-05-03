using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberShooter
{
    public class Map
    {
        public int mapWidth { get; set; }
        public int mapHeight { get; set; }
        public int tileWidth { get; set; }
        public int tileHeight { get; set; }

        public Layer tileLayer1;
        public Layer hostileHumanLayer;
        public Layer friendlyHumanLayer;
        public Layer hostileRobotLayer;
        public Layer friendlyRobotLayer;
        public Layer lootLayer;
        public Layer questLayer;
        public Layer wallArtLayer;
        public Layer shadowLayer;
        public Layer tileLayer2;
        public Layer solidLayer;

        public List<Rectangle> tileSet = new List<Rectangle>();
        public List<Rectangle> collisionRects = new List<Rectangle>();
        public List<NPC> NPCs = new List<NPC>();
        public List<WeaponPickup> weaponPickups = new List<WeaponPickup>();
        public List<GameObject> questItems = new List<GameObject>();
        public List<GameObject> wallArts = new List<GameObject>();

        Rectangle bounds;
        Random rnd = new Random();
        int gunType;

        public Map(int mapWidth, int mapHeight, int tileWidth, int tileHeight)
        {
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;

            tileLayer1 = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);
            hostileHumanLayer = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);
            friendlyHumanLayer = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);
            hostileRobotLayer = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);
            friendlyRobotLayer = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);
            lootLayer = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);
            questLayer = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);
            wallArtLayer = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);
            shadowLayer = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);
            tileLayer2 = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);
            solidLayer = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);
        }
        public void LoadMap(string loadFileName)
        {
            try
            {
                StreamReader objReader;
                objReader = new StreamReader(loadFileName);

                mapHeight = Convert.ToInt32(objReader.ReadLine());
                mapWidth = Convert.ToInt32(objReader.ReadLine());
                tileHeight = Convert.ToInt32(objReader.ReadLine());
                tileWidth = Convert.ToInt32(objReader.ReadLine());

                tileLayer1 = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);
                hostileHumanLayer = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);
                friendlyHumanLayer = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);
                hostileRobotLayer = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);
                friendlyRobotLayer = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);
                lootLayer = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);
                questLayer = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);
                wallArtLayer = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);
                shadowLayer = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);
                tileLayer2 = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);
                solidLayer = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);

                tileLayer1.LoadLayer(objReader);
                hostileHumanLayer.LoadLayer(objReader);
                friendlyHumanLayer.LoadLayer(objReader);
                hostileRobotLayer.LoadLayer(objReader);
                friendlyRobotLayer.LoadLayer(objReader);
                lootLayer.LoadLayer(objReader);
                questLayer.LoadLayer(objReader);
                wallArtLayer.LoadLayer(objReader);
                shadowLayer.LoadLayer(objReader);
                tileLayer2.LoadLayer(objReader);
                solidLayer.LoadLayer(objReader);

                objReader.Close();
                objReader.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error loading the map, is the file a valid map file?/nError:" + ex);
            }
        }
        public void DrawMap(Player p,  List<ResourcePickup> resourcePickUpList)
        {
            try
            {
                for (int x = 0; x < mapWidth; ++x)
                {
                    for (int y = 0; y < mapHeight; ++y)
                    {
                        if (tileLayer1.layer[y, x] != 0)
                        {
                            bounds = tileSet[tileLayer1.layer[y, x] - 1];

                            Game1.spriteBatch.Draw(Game1.tileSheet, new Vector2(((y - GameBoard.drawOffset.X) * tileWidth), ((x - GameBoard.drawOffset.Y) * tileHeight)), bounds, Color.White);
                        }
                        if (lootLayer.layer[y, x] != 0)
                        {
                            bounds = tileSet[lootLayer.layer[y, x] - 1];

                            Game1.spriteBatch.Draw(Game1.tileSheet, new Vector2(((y - GameBoard.drawOffset.X) * tileWidth), ((x - GameBoard.drawOffset.Y) * tileHeight)), bounds, Color.White);
                        }
                    }
                }
                foreach(WeaponPickup wP in weaponPickups)
                {
                    wP.Draw(Game1.spriteBatch, Game1.square);
                }
                foreach(NPC npc in NPCs)
                {
                    foreach (Projectile projectile in npc.ProjectileList)
                    {
                        projectile.Draw(Game1.spriteBatch, Game1.square);
                    }
                }
                foreach (Projectile projectile in p.ProjectileList)
                {
                    projectile.Draw(Game1.spriteBatch, Game1.square);
                }
                p.Draw(Game1.spriteBatch, Game1.square);
                foreach (NPC npc in NPCs)
                {
                    npc.Draw(Game1.spriteBatch, Game1.square);
                }
                foreach (ResourcePickup pickup in resourcePickUpList)
                {
                    pickup.Draw(Game1.spriteBatch, Game1.square);
                }
                for (int x = 0; x < mapWidth; ++x)
                {
                    for (int y = 0; y < mapHeight; ++y)
                    {
                        if(shadowLayer.layer[y, x] != 0)
                        {
                            bounds = tileSet[shadowLayer.layer[y, x] - 1];

                            Game1.spriteBatch.Draw(Game1.tileSheet, new Vector2(((y - GameBoard.drawOffset.X) * tileWidth), ((x - GameBoard.drawOffset.Y) * tileHeight)), bounds, Color.White);
                        }
                        if (tileLayer2.layer[y, x] != 0)
                        {
                            bounds = tileSet[tileLayer2.layer[y, x] - 1];

                            Game1.spriteBatch.Draw(Game1.tileSheet, new Vector2(((y - GameBoard.drawOffset.X) * tileWidth), ((x - GameBoard.drawOffset.Y) * tileHeight)), bounds, Color.White);
                        }
                        if (solidLayer.layer[y, x] != 0)
                        {
                           //Draw Collision Layer

                           //Game1.spriteBatch.Draw(Game1.square, new Vector2(((y - GameBoard.drawOffset.X) * tileWidth), ((x - GameBoard.drawOffset.Y) * tileHeight)), new Rectangle(0, 0, tileWidth, tileHeight), new Color(255, 0, 0, 100));
                        }
                    }
                }
            }
            catch
            {
                ;
            }
        }
        public void LoadTileSet(Texture2D tileSheet)
        {
            int numbOfTilesX = (int)tileSheet.Width / tileWidth;
            int numbOfTilesY = (int)tileSheet.Height / tileHeight;

            tileSet = new List<Rectangle>(numbOfTilesX * numbOfTilesY);

            for (int j = 0; j < numbOfTilesX; ++j)
            {
                for (int i = 0; i < numbOfTilesY; ++i)
                {
                    bounds = new Rectangle(i * tileWidth, j * tileHeight, tileWidth, tileHeight);
                    tileSet.Add(bounds);
                }
            }
        }
        public void PopulateCollisionLayer()
        {
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    if (solidLayer.layer[x, y] == 1)
                    {
                        collisionRects.Add(new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight));
                    }
                }
            }
        }
        public void PopulateHostileHumanLayer()
        {
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    if (hostileHumanLayer.layer[x, y] == 1)
                    {
                        NPCs.Add(new NPC(new Vector2(x * tileWidth, y * tileHeight), true));
                    }
                }
            }
        }
        public void PopulateFriendlyHumanLayer()
        {
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    if (friendlyHumanLayer.layer[x, y] == 1)
                    {
                        NPCs.Add(new NPC(new Vector2(x * tileWidth, y * tileHeight), false));
                    }
                }
            }
        }
        public void PopulateFriendlyRobotLayer()
        {
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    if (friendlyRobotLayer.layer[x, y] == 1)
                    {
                        NPCs.Add(new NPC(new Vector2(x * tileWidth, y * tileHeight), false));
                    }
                }
            }
        }
        public void PopulateHostileRobotLayer()
        {
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    if (hostileRobotLayer.layer[x, y] == 1)
                    {
                        NPCs.Add(new NPC(new Vector2(x * tileWidth, y * tileHeight), true));
                    }
                }
            }
        }
        public void PopulateLootLayer()
        {
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    if (lootLayer.layer[x, y] != 0)
                    {
                        gunType = (int)rnd.Next(1, 3);

                        if (gunType == 1)
                            weaponPickups.Add(new WeaponPickup(new Vector2(x * tileWidth, y * tileHeight), PickupTypes.handgun));
                        else if(gunType== 2)
                            weaponPickups.Add(new WeaponPickup(new Vector2(x * tileWidth, y * tileHeight), PickupTypes.rifle));

                        collisionRects.Add(new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight));
                    }
                }
            }
        }
        public void PopulateQuestLayer()
        {
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    if (questLayer.layer[x, y] == 1)
                    {
                        
                    }
                }
            }
        }
        public void PopulateWallArtLayer()
        {
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    if (wallArtLayer.layer[x, y] == 1)
                    {
                        
                    }
                }
            }
        }
    }
}
