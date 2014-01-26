using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GGJ_2014.Creatures
{
    interface Interactable
    {
        void Interact(Creature user);
        bool IsAlive();
        Rectangle GetCollisionBox();
        string GetIdentifier();
    }
}
