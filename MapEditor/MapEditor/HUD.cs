using Microsoft.Xna.Framework;
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
        Texture2D panel, newMap, saveMap, loadMap, loadTiles, backLayer, 
            frontLayer, collisionLayer, hostileHumanLayer, friendlyHumanLayer, shadowLayer, friendlyRobotLayer, hostileRobotLayer,
            lootLayer, questLayer, wallArtLayer;

        Vector2 position;
        Rectangle horizontalPanelRect;
        int horizontalPanelHeight = 100;

        Rectangle verticalPanelRect;
        int verticalPanelWidth = 60;

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
            hostileHumanLayer = content.Load<Texture2D>("hostileHButton");
            friendlyHumanLayer = content.Load<Texture2D>("friendlyHButton");
            friendlyRobotLayer = content.Load<Texture2D>("friendlyRButton");
            hostileRobotLayer = content.Load<Texture2D>("hostileRButton");
            lootLayer = content.Load<Texture2D>("lootButton");
            questLayer = content.Load<Texture2D>("questButton");
            wallArtLayer = content.Load<Texture2D>("wallArtButton");
            shadowLayer = content.Load<Texture2D>("shadowbutton");

            position = new Vector2(0, (int)Game1.clientBounds.Y - panel.Height);

            horizontalPanelRect = new Rectangle(0, (int)Game1.clientBounds.Y - horizontalPanelHeight, (int)Game1.clientBounds.X, horizontalPanelHeight);
            verticalPanelRect = new Rectangle((int)Game1.clientBounds.X - verticalPanelWidth, 0, verticalPanelWidth, (int)Game1.clientBounds.Y - horizontalPanelHeight);

            buttons.Add(new NewMapButton(newMap, new Vector2(position.X, position.Y - 25)));
            buttons.Add(new SaveMapButton(saveMap, new Vector2(position.X + 115, position.Y - 25)));
            buttons.Add(new LoadMapButton(loadMap, new Vector2(position.X + 230, position.Y - 25)));
            buttons.Add(new LoadTileButton(loadTiles, new Vector2(position.X + 345, position.Y - 25)));
            buttons.Add(new BackLayerButton(backLayer, new Vector2(position.X + 460, position.Y - 25)));
            buttons.Add(new FrontLayerButton(frontLayer, new Vector2(position.X + 575, position.Y - 25)));
            buttons.Add(new CollisionLayerButton(collisionLayer, new Vector2(position.X + 690, position.Y - 25)));
            buttons.Add(new ShadowLayerButton(shadowLayer, new Vector2(Game1.clientBounds.X - 55, 5)));
            buttons.Add(new HostileHumanLayerButton(hostileHumanLayer, new Vector2(Game1.clientBounds.X - 55,60)));
            buttons.Add(new FriendlyHumanLayerButton(friendlyHumanLayer, new Vector2(Game1.clientBounds.X - 55, 115)));
            buttons.Add(new FriendlyRobotButton(hostileRobotLayer, new Vector2(Game1.clientBounds.X - 55, 165)));
            buttons.Add(new HostileRobotButton(friendlyRobotLayer, new Vector2(Game1.clientBounds.X - 55, 215)));
            buttons.Add(new LootButton(lootLayer, new Vector2(Game1.clientBounds.X - 55, 265)));
            buttons.Add(new WallArtButton(wallArtLayer, new Vector2(Game1.clientBounds.X - 55, 315)));
            buttons.Add(new QuestButton(questLayer, new Vector2(Game1.clientBounds.X - 55, 365)));

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
            Game1.spriteBatch.Draw(panel, horizontalPanelRect, Color.LightGray);
            Game1.spriteBatch.Draw(panel, verticalPanelRect, Color.LightGray);

            foreach(Button b in buttons)
            {
                b.Draw();
            }
        }
    }
}
