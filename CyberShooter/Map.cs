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
        public Layer tileLayer2;
        public Layer solidLayer;

        public List<Rectangle> tileSet = new List<Rectangle>();
        public List<Rectangle> collisionRects = new List<Rectangle>();

        Rectangle bounds;
        Rectangle collisionRect;

        public Map(int mapWidth, int mapHeight, int tileWidth, int tileHeight)
        {
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;

            tileLayer1 = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);
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
                tileLayer2 = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);
                solidLayer = new Layer(mapWidth, mapHeight, tileWidth, tileHeight);

                tileLayer1.LoadLayer(objReader);
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
        public void DrawMap()
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
    }
}
