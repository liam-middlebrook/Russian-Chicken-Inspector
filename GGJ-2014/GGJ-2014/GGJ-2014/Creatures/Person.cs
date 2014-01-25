using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GGJ_2014.Creatures
{
    class Person
        : Creature
    {
        public const float WALK_SPEED = 4.5f;

        public Person(Texture2D texture, Vector2 position)
            : base(texture, position, "Person")
        {

        }

        public override void Interact(Creature user)
        {
            
        }

        public override void HandleInput(Microsoft.Xna.Framework.Input.KeyboardState keyState)
        {
            
        }
    }
}
