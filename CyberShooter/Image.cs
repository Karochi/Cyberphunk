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
        public float alpha;
        public string text, fontName, path;
        public Vector2 position, scale;
        public Rectangle srcRect;
        public bool isActive;
        public string effects;
        public FadeEffect fadeEffect;
        [XmlIgnore]
        public Texture2D texture;
        Vector2 origin;
        ContentManager content;
        RenderTarget2D renderTarget;
        SpriteFont font;
        Dictionary<string, ImageEffect> effectList;

        void SetEffect<T>(ref T effect)
        {
            if (effect == null)
                effect = (T)Activator.CreateInstance(typeof(T));
            else
            {
                (effect as ImageEffect).isActive = true;
                var obj = this;
                (effect as ImageEffect).LoadContent(ref obj);
            }
            effectList.Add(effect.GetType().ToString().Replace("CyberShooter.", ""), (effect as ImageEffect));
        }
        public void ActivateEffect(string effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].isActive = true;
                var obj = this;
                effectList[effect].LoadContent(ref obj);
            }
        }
        public void DeactivateEffect(string effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].isActive = false;
                effectList[effect].UnloadContent();
            }
        }
        public Image()
        {
            path = text = effects = String.Empty;
            fontName = "spriteFont";
            position = new Vector2(560,260);
            scale = Vector2.One;
            alpha = 1.0f;
            srcRect = Rectangle.Empty;
            effectList = new Dictionary<string, ImageEffect>();
        }
        public void LoadContent()
        {
            content = new ContentManager(ScreenManager.Instance.content.ServiceProvider, "Content");

            if (path != String.Empty)
                texture = content.Load<Texture2D>(path);

            font = content.Load<SpriteFont>(fontName);

            Vector2 dimensions = Vector2.Zero;

            if (texture != null) 
                dimensions.X += texture.Width;
            dimensions.X += font.MeasureString(text).X;

            if (texture != null)
                dimensions.Y = Math.Max(texture.Height, font.MeasureString(text).Y);
            else
                dimensions.Y = font.MeasureString(text).Y;

            if (srcRect == Rectangle.Empty)
                srcRect = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y);

            renderTarget = new RenderTarget2D(ScreenManager.Instance.graphicsDevice, (int)dimensions.X, (int)dimensions.Y);
            ScreenManager.Instance.graphicsDevice.SetRenderTarget(renderTarget);
            ScreenManager.Instance.graphicsDevice.Clear(Color.Transparent);
            ScreenManager.Instance.spriteBatch.Begin();

            if (texture != null)
                ScreenManager.Instance.spriteBatch.Draw(texture, Vector2.Zero, Color.White);
            ScreenManager.Instance.spriteBatch.DrawString(font, text, Vector2.Zero, Color.White);

            ScreenManager.Instance.spriteBatch.End();

            texture = renderTarget;

            ScreenManager.Instance.graphicsDevice.SetRenderTarget(null);

            SetEffect<FadeEffect>(ref fadeEffect);

            if(effects != String.Empty)
            {
                string[] split = effects.Split(':');
                foreach (string item in split)
                    ActivateEffect(item);
            }
        }
        public void UnloadContent()
        {
            content.Unload();
            foreach (var effect in effectList)
                DeactivateEffect(effect.Key);
        }
        public void Update(GameTime gameTime)
        {
            foreach (var effect in effectList)
            {
                if (effect.Value.isActive)
                    effect.Value.Update(gameTime);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            origin = new Vector2(srcRect.Width / 2, srcRect.Height / 2);

            spriteBatch.Draw(texture, new Vector2(1920/2,1080/2), srcRect, Color.White * alpha, 0.0f, origin, scale, SpriteEffects.None, 0.0f);
        }
    }
}
