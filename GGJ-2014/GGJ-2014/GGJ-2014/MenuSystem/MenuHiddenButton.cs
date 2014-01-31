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
        private Keys keyForTrigger2;

        public MenuHiddenButton(Keys keyForTrigger, ButtonDelegate buttonDelegate, Keys keyForTrigger2 = 0)
            : base(Vector2.Zero)
        {
            this.keyForTrigger = keyForTrigger;
            this.keyForTrigger2 = keyForTrigger2;
            this.OnTriggered += buttonDelegate;
        }

        public override void Update(KeyboardState keyState, KeyboardState prevKeyState, MouseState mouseState, MouseState prevMouseState)
        {
            if ((keyState.IsKeyUp(keyForTrigger) && prevKeyState.IsKeyDown(keyForTrigger)) || (keyState.IsKeyUp(keyForTrigger2) && prevKeyState.IsKeyDown(keyForTrigger2)))
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
