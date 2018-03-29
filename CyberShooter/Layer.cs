using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CyberShooter
{
    public class Layer
    {
        public int[,] layer;
        int mapWidth, mapHeight, tileWidth, tileHeight;

        public Layer(int mapWidth, int mapHeight, int tileWidth, int tileHeight)
        {
            this.mapHeight = mapHeight;
            this.mapWidth = mapWidth;
            this.tileHeight = tileHeight;
            this.tileWidth = tileWidth;
            layer = new int[mapWidth, mapHeight];

        }
        public void LoadLayer(System.IO.StreamReader objReader)
        {
            try
            {
                for(int i = 0; i <mapWidth; i++)
                {
                    for(int j = 0; i <mapHeight; j++)
                    {
                        layer[i, j] = Convert.ToInt32(objReader.ReadLine());
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Map loading error");
            }
        }
    }
}
