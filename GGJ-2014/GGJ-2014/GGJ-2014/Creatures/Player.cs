using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

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
    }
}
