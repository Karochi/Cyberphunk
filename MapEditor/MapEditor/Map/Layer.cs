using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    public class Layer
    {
        public int[,] layer;

        int mapWidth, mapHeight, tileWidth, tileHeight;

        public Layer(int mapWidth, int mapHeight, int tileWidth, int tileHeight)
        {
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;

            layer = new int[mapWidth, mapHeight];
        }
        public void SetTiles(int selectedTile)
        {
            Vector2 mouse;
            double mouseMapX;
            double mouseMapY;

            MouseState currMouseState = Mouse.GetState();

            try
            {
                if(currMouseState.LeftButton == ButtonState.Pressed)
                {
                    mouse = new Vector2(currMouseState.X, currMouseState.Y);
                    mouseMapX = ((int)mouse.X / tileWidth) + Game1.drawOffset.X;
                    mouseMapY = ((int)mouse.Y / tileHeight) + Game1.drawOffset.Y;

                    if(mouseMapX < mapWidth && mouseMapY < mapHeight && mouseMapX >= 0 && mouseMapY >= 0)
                    {
                        layer[(int)mouseMapX, (int)mouseMapY] = selectedTile;
                    }
                }
                if (currMouseState.RightButton == ButtonState.Pressed)
                {
                    mouse = new Vector2(currMouseState.X, currMouseState.Y);
                    mouseMapX = ((int)mouse.X / tileWidth) + Game1.drawOffset.X;
                    mouseMapY = ((int)mouse.Y / tileHeight) + Game1.drawOffset.Y;

                    if (mouseMapX < mapWidth && mouseMapY < mapHeight && mouseMapX >= 0 && mouseMapY >= 0)
                    {
                        layer[(int)mouseMapX, (int)mouseMapY] = 0;
                    }
                }
            }
            catch
            {
                ;
            }
        }
        public void SaveLayer(StreamWriter objWriter)
        {
            try
            {
                for (int i = 0; i < mapWidth; ++i)
                {
                    for(int j = 0; j < mapHeight; ++j)
                    {
                        objWriter.WriteLine(layer[i, j]);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("There was an error saving the map. /nError:" + ex);
            }
        }
        public void LoadLayer(StreamReader objReader)
        {
            try
            {
                for (int i = 0; i < mapWidth; ++i)
                {
                    for (int j = 0; j < mapHeight; ++j)
                    {
                        layer[i, j] = Convert.ToInt32(objReader.ReadLine());
                    }
                }
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("There was an error loading the map. /nError:" + ex);
            }
        }
    }
}
