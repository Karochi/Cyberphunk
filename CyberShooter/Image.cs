using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CyberShooter
{
     public class Image
    {
        public float Alpha;
        public string Text, FontName, Path;
        public Vector2 Position, Scale;
        public Rectangle SrcRect;
        [XmlIgnore]
        public Texture2D Texture;
        Vector2 origin;
        ContentManager content;
        RenderTarget2D renderTarget;
        SpriteFont font;

        public Image()
        {
            Path = Text = String.Empty;
            FontName = "spriteFont";
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Alpha = 1.0f;
            SrcRect = Rectangle.Empty;
        }
        public void LoadContent()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");

            if (Path != String.Empty)
                Texture = content.Load<Texture2D>(Path);

            font = content.Load<SpriteFont>(FontName);

            Vector2 dimensions = Vector2.Zero;

            if (Texture != null) 
                dimensions.X += Texture.Width;
            dimensions.X += font.MeasureString(Text).X;

            if (Texture != null)
                dimensions.Y = Math.Max(Texture.Height, font.MeasureString(Text).Y);
            else
                dimensions.Y = font.MeasureString(Text).Y;

            if (SrcRect == Rectangle.Empty)
                SrcRect = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y);

            renderTarget = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice, (int)dimensions.X, (int)dimensions.Y);
            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(renderTarget);
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);

            ScreenManager.Instance.SpriteBatch.Begin();

            if (Texture != null)
                ScreenManager.Instance.SpriteBatch.Draw(Texture, Vector2.Zero, Color.White);
            ScreenManager.Instance.SpriteBatch.DrawString(font, Text, Vector2.Zero, Color.White);

            ScreenManager.Instance.SpriteBatch.End();

            Texture = renderTarget;

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);
        }
        public void UnloadContent()
        {
            content.Unload();
        }
        public void Update(GameTime gameTime)
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            origin = new Vector2(SrcRect.Width / 2, SrcRect.Height / 2);

            spriteBatch.Draw(Texture, Position + origin, SrcRect, Color.White * Alpha, 0.0f, origin, Scale, SpriteEffects.None, 0.0f);
        }
    }
}
