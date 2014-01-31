using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GGJ_2014.MenuSystemNS
{
    abstract class MenuControl
    {
        protected Vector2 position;

        protected MenuControl(Vector2 position)
        {
            this.position = position;
        }

        public abstract void Update(KeyboardState keyState, KeyboardState prevKeyState, MouseState mouseState, MouseState prevMouseState);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
