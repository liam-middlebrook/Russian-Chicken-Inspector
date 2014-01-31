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

        private List<Keys> keysForTrigger;

        public MenuHiddenButton(List<Keys> keysForTrigger, ButtonDelegate buttonDelegate)
            : base(Vector2.Zero)
        {
            this.keysForTrigger = keysForTrigger;
            this.OnTriggered += buttonDelegate;
        }

        public override void Update(KeyboardState keyState, KeyboardState prevKeyState, MouseState mouseState, MouseState prevMouseState)
        {
            foreach (Keys key in keysForTrigger)
            {
                if (keyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key))
                {
                    if (OnTriggered != null)
                    {
                        OnTriggered();
                    }
                    break;
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
