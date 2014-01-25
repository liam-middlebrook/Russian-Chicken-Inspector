using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GGJ_2014.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GGJ_2014.Graphics;

namespace GGJ_2014.Creatures
{
    class Egg : Sprite
    {
        public Egg(Vector2 position)
            : base(TextureStorage.GetInstance().GetTexture(Textures.CHICKEN_EGG), position)
        {
            Scale = new Vector2(0.25f, .5f);
        }

        public Rectangle CollisionBox
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width/4, (int)Texture.Height/2); }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}
