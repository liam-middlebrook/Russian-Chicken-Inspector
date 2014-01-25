using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GGJ_2014.Levels;
using Microsoft.Xna.Framework.Input;

namespace GGJ_2014.Creatures
{
    class Player
        : Person
    {
        public static float Strength;
        public static float Charisma;
        public static float Intelligence;


        public Player(Texture2D texture, Vector2 position)
            : base(texture, position)
        {

        }

        public override void HandleInput(KeyboardState keyState)
        {
            base.HandleInput(keyState);
            if (keyState.IsKeyDown(Keys.Space))
            {
                UseObject();
            }
        }

        public void UseObject()
        {
            Tile tile = GetTileInFrontOf();
            if (tile != null)
            {
                Console.WriteLine(tile);
            }
        }
    }
}
