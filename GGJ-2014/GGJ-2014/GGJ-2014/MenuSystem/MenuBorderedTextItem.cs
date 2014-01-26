using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GGJ_2014.MenuSystemNS
{
    delegate void UpdateDelegate();
    class MenuBorderedTextItem : MenuControl
    {
        public const int TICK_CONSTANT_SECOND = 10000000;

        private Rectangle buttonRect;

        private Texture2D buttonTexture;

        private Color buttonTint;

        private string text;

        private SpriteFont font;

        private UpdateDelegate delegatePrm;

        private int timeExistFor;
        private long currentTime;

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                Vector2 textSize = font.MeasureString(text);

                this.buttonRect = new Rectangle((int)position.X, (int)position.Y, (int)textSize.X + 20, (int)textSize.Y + 20);
                
                this.buttonTexture = GGJ_2014.Graphics.TextureGenerator.GenerateTexture(MenuSystem.GetInstance().GraphicsDevice, Graphics.Textures.BORDERED, null, buttonRect.Width, buttonRect.Height);

            }
        }

        public MenuBorderedTextItem(Vector2 position, Color buttonTint, string text, float timeForMessageToApearFor = 0)
            : base(position)
        {
            this.text = text;

            this.font = MenuSystem.GetInstance().MenuFont;

            Vector2 textSize = font.MeasureString(text);

            this.buttonRect = new Rectangle((int)position.X, (int)position.Y, (int)textSize.X + 20, (int)textSize.Y + 20);

            this.buttonTexture = GGJ_2014.Graphics.TextureGenerator.GenerateTexture(MenuSystem.GetInstance().GraphicsDevice, Graphics.Textures.BORDERED, null, buttonRect.Width, buttonRect.Height);

            this.buttonTint = buttonTint;

            currentTime = DateTime.Now.Ticks;
            timeExistFor = (int)(timeForMessageToApearFor * TICK_CONSTANT_SECOND);
        }

        public void ChangeText(string text, bool resize = true)
        {
            this.text = text;
            if (resize)
            {
                Vector2 textSize = font.MeasureString(text);
                this.buttonRect = new Rectangle((int)position.X, (int)position.Y, (int)textSize.X + 20, (int)textSize.Y + 20);
                this.buttonTexture = GGJ_2014.Graphics.TextureGenerator.GenerateTexture(MenuSystem.GetInstance().GraphicsDevice, Graphics.Textures.BORDERED, null, buttonRect.Width, buttonRect.Height);
            }
        }

        public override void Update(KeyboardState keyState, KeyboardState prevKeyState, MouseState mouseState, MouseState prevMouseState)
        {
            if (timeExistFor > 0 && (DateTime.Now.Ticks - currentTime) > timeExistFor)
            {
                //Console.WriteLine("Go " + DateTime.Now.Second);
                MenuSystem.GetInstance().CurrentScreen.RemoveControl(this);
                currentTime = DateTime.Now.Ticks;
            }
            if (delegatePrm != null)
            {
                delegatePrm();
            }
            return;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(buttonTexture, buttonRect, buttonTint);
            spriteBatch.DrawString(font, text, position + new Vector2(10, 10), Color.Black);
        }
    }
}
