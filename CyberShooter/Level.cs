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
    class Level
    {
        public int mapWidth { get; set; }
        public int mapHeight { get; set; }
        public int tileWidth { get; set; }
        public int tileHeight { get; set; }

        public Layer TileLayer1;
        public Layer TileLayer2;
        public Layer SolidLayer;

        public List<Rectangle> tileSet = new List<Rectangle>();
        public List<Rectangle> collisonRects = new List<Rectangle>();

        Rectangle bounds;

        Vector2 drawOffset = Vector2.Zero;

        public void LoadMap(String loadFileName)
        {
            try
            {
                StreamReader objReader;
                objReader = new StreamReader(@loadFileName);

                mapHeight = Convert.ToInt32(objReader.ReadLine());
                mapWidth = Convert.ToInt32(objReader.ReadLine());
                tileHeight = Convert.ToInt32(objReader.ReadLine());
                tileWidth = Convert.ToInt32(objReader.ReadLine());

                TileLayer1 = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);
                TileLayer2 = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);
                SolidLayer = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);

                TileLayer1.LoadLayer(objReader);
                TileLayer2.LoadLayer(objReader);
                SolidLayer.LoadLayer(objReader);

                objReader.Close();
                objReader.Dispose();
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Map file name error" + ex);
            }
        }
        public void LoadTileSet(Texture2D tileTex)
        {
            int numbOfTilesX = (int)tileTex.Width / tileWidth;
            int numbOfTilesY = (int)tileTex.Height / tileHeight;

            tileSet = new List<Rectangle>(numbOfTilesX * numbOfTilesY);

            for (int j = 0; j < numbOfTilesY; ++j)
            {
                for(int i = 0; i < numbOfTilesX; ++i)
                {
                    bounds = new Rectangle(i * tileWidth, j * tileHeight, tileWidth, tileHeight);
                    tileSet.Add(bounds);
                }
            }
        }
        public void PopulateCollisionLayer()
        {
            collisonRects = new List<Rectangle>();

            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    if (SolidLayer.layer[x, y] == 1)
                    {
                        collisonRects.Add(new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight));
                    }
                }
            }
        }
        public void DrawMap(SpriteBatch sb, Player player, NPC nPC, Texture2D tileSheet)
        {
            drawOffset.X = (player.position.X / tileWidth) - (player.mapPosition.X/tileWidth);
            drawOffset.Y = (player.position.Y / tileHeight) - (player.mapPosition.Y / tileHeight);

            try
            {
                for (int x = 0; x < mapHeight; ++x)
                {
                    for ( int y = 0; y < mapWidth; ++y)
                    {
                        if(TileLayer1.layer[y,x] != 0)
                        {
                            bounds = tileSet[TileLayer1.layer[y, x] - 1];

                            sb.Draw(tileSheet, new Vector2(((y - drawOffset.X) * tileWidth), ((x - drawOffset.Y) * tileHeight)), bounds, Color.White);
                        }
                    }
                }
                player.Draw(sb, Game1.square);

                nPC.Draw(sb, Game1.square);

                for (int x = 0; x < mapHeight; ++x)
                {
                    for (int y = 0; y < mapWidth; ++y)
                    {
                        if (TileLayer2.layer[y, x] != 0)
                        {
                            bounds = tileSet[TileLayer2.layer[y, x] - 1];

                            sb.Draw(tileSheet, new Vector2(((y - drawOffset.X) * tileWidth), ((x - drawOffset.Y) * tileHeight)), bounds, Color.White);
                        }
                    }
                }
            }

            catch
            {
                Console.WriteLine("Back Map drawing error");
            }
        }
    }
}
