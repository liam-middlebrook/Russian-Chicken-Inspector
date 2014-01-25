using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GGJ_2014.MenuSystemNS
{
    class MenuButton : MenuControl
    {
        private Rectangle buttonRect;
        private Texture2D buttonTexture;
        private SpriteFont spriteFont;
        private string text;
        private Color buttonTint;
        private event ButtonDelegate OnTriggered;

        private Keys keyForTrigger;

        public MenuButton(
            Vector2 position,
            string text,
            Color color,
            ButtonDelegate buttonDelegate,
            Keys triggerKey = 0)
            : base(position)
        {
            this.spriteFont = MenuSystem.GetInstance().MenuFont;
            this.text = text;
            Vector2 textSize = spriteFont.MeasureString(text);
            this.buttonRect = new Rectangle((int)position.X, (int)position.Y, (int)textSize.X + 20, (int)textSize.Y + 20);
            GenerateTexture(MenuSystem.GetInstance().GraphicsDevice, color);
            this.OnTriggered += buttonDelegate;
            this.buttonTint = new Color(255, 255, 255, 128);
            keyForTrigger = triggerKey;
        }

        void GenerateTexture(GraphicsDevice graphics, Color color)
        {
            Texture2D tex = new Texture2D(graphics, buttonRect.Width, buttonRect.Height);
            Color[] colorData = new Color[buttonRect.Width * buttonRect.Height];
            for (int i = 0; i < colorData.Length; i++)
            {
                colorData[i] = color;
            }
            tex.SetData<Color>(colorData);
            buttonTexture = tex;
        }

        public override void Update(KeyboardState keyState, KeyboardState prevKeyState, MouseState mouseState, MouseState prevMouseState)
        {
            Rectangle mouseRect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

            if (mouseRect.Intersects(buttonRect))
            {
                buttonTint = Color.White;
                if (mouseState.LeftButton == ButtonState.Released && prevMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (OnTriggered != null)
                    {
                        OnTriggered();
                    }
                }
            }
            else
            {
                buttonTint = new Color(255, 255, 255, 128);
            }
            if (keyState.IsKeyUp(keyForTrigger) && prevKeyState.IsKeyDown(keyForTrigger))
            {
                if (OnTriggered != null)
                {
                    OnTriggered();
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(buttonTexture, buttonRect, buttonTint);
            spriteBatch.DrawString(spriteFont, text, position + new Vector2(10, 10), Color.Black);
        }
    }
}
