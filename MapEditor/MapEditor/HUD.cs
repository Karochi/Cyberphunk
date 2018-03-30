﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    public class HUD
    {
        Texture2D panel;
        Texture2D newMap; 
        Texture2D saveMap; 
        Texture2D loadMap; 
        Texture2D loadTiles; 
        Texture2D backLayer;
        Texture2D frontLayer; 
        Texture2D collisionLayer;

        Vector2 position;
        Rectangle panelRect;
        int panelWidth = 100;

        List<Button> buttons = new List<Button>();

        public HUD(ContentManager content)
        {
            panel = content.Load<Texture2D>("plattform");
            newMap = content.Load<Texture2D>("newMapButton");
            saveMap = content.Load<Texture2D>("svaeMapButton");
            loadMap = content.Load<Texture2D>("loadMapbutton");
            loadTiles = content.Load<Texture2D>("loadTileButton");
            backLayer = content.Load<Texture2D>("backLayer");
            frontLayer = content.Load<Texture2D>("frontLayer");
            collisionLayer = content.Load<Texture2D>("collisionLayerButton");

            position = new Vector2(0, (int)Game1.clientBounds.Y - panel.Height);

            panelRect = new Rectangle(0, (int)Game1.clientBounds.Y - panelWidth, (int)Game1.clientBounds.X, panelWidth);

            buttons.Add(new NewMapButton(newMap, new Vector2(position.X, position.Y - 25)));
            buttons.Add(new SaveMapButton(saveMap, new Vector2(position.X + 115, position.Y - 25)));
            buttons.Add(new LoadMapButton(loadMap, new Vector2(position.X + 230, position.Y - 25)));
            buttons.Add(new LoadTileButton(loadTiles, new Vector2(position.X + 345, position.Y - 25)));
            buttons.Add(new BackLayerButton(backLayer, new Vector2(position.X + 460, position.Y - 25)));
            buttons.Add(new FrontLayerButton(frontLayer, new Vector2(position.X + 575, position.Y - 25)));
            buttons.Add(new CollisionLayerButton(collisionLayer, new Vector2(position.X + 690, position.Y - 25)));
        }
        public void Update()
        {
            foreach (Button b in buttons)
            {
                b.Update();

                if (b.clicked)
                    b.Effect();
            }

        }
        public void Draw()
        {
            Game1.spriteBatch.Draw(panel, panelRect, Color.LightGray);

            foreach(Button b in buttons)
            {
                b.Draw();
            }
        }
    }
}