using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GGJ_2014.MenuSystemNS
{
    class MenuTextItem : MenuControl
    {
        private string text;

        private SpriteFont font;

        public MenuTextItem(string text, SpriteFont font, Vector2 position)
            : base(position)
        {
            this.text = text;

            this.font = font;
        }

        public override void Update(KeyboardState keyState, KeyboardState prevKeyState, MouseState mouseState, MouseState prevMouseState)
        {
            return;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, position, Color.Black);
        }
    }
}
