﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GGJ_2014.MenuSystemNS
{
    class MenuBorderedTextItem : MenuControl
    {
        private Rectangle buttonRect;

        private Texture2D buttonTexture;

        private Color buttonTint;

        private string text;

        private SpriteFont font;

        public MenuBorderedTextItem(string text, SpriteFont font, Vector2 position, Color buttonTint)
            : base(position)
        {
            this.text = text;

            this.font = font;

            Vector2 textSize = font.MeasureString(text);

            this.buttonRect = new Rectangle((int)position.X, (int)position.Y, (int)textSize.X + 20, (int)textSize.Y + 20);

            this.buttonTexture = GGJ_2014.Graphics.TextureGenerator.GenerateTexture(MenuSystem.GetInstance().GraphicsDevice, Graphics.Textures.BORDERED, buttonRect.Width, buttonRect.Height);

            this.buttonTint = buttonTint;
        }

        public override void Update(KeyboardState keyState, KeyboardState prevKeyState, MouseState mouseState, MouseState prevMouseState)
        {
            return;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(buttonTexture, buttonRect, buttonTint);
            spriteBatch.DrawString(font, text, position + new Vector2(10, 10), Color.Black);
        }
    }
}