using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GGJ_2014.MenuSystemNS
{
    delegate void ButtonDelegate();

    class MenuHiddenButton : MenuControl
    {
        private event ButtonDelegate OnTriggered;

        private Keys keyForTrigger;

        public MenuHiddenButton(Keys keyForTrigger, ButtonDelegate buttonDelegate)
            : base(Vector2.Zero)
        {
            this.keyForTrigger = keyForTrigger;
            this.OnTriggered += buttonDelegate;
        }

        public override void Update(KeyboardState keyState, KeyboardState prevKeyState, MouseState mouseState, MouseState prevMouseState)
        {
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
            //This is a hidden item and will NOT draw to the screen
            return;
        }
    }
}
