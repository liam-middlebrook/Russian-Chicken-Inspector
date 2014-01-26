using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GGJ_2014.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GGJ_2014.Graphics;
using GGJ_2014.Levels;

namespace GGJ_2014.Creatures
{
    class Egg : Sprite
    {
        public const int EGG_SPAWN_CHICKEN_TIME = 60000;

        protected bool removeEgg = false;
        protected bool canPickUp = true;
        private int timePassed = 0;

        public Egg(Vector2 position)
            : base(TextureStorage.GetInstance().GetTexture(Textures.CHICKEN_EGG), position)
        {
            Scale = new Vector2(0.25f, .5f);
        }

        public virtual Rectangle GetCollisionBox()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width/4, (int)Texture.Height/2);
        }

        public virtual void Update(GameTime gameTime)
        {
            timePassed += gameTime.ElapsedGameTime.Milliseconds;
            if (timePassed >= EGG_SPAWN_CHICKEN_TIME)
            {
                removeEgg = true;
                Level.GetInstance().AddCreature(new Chicken(Position));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }


        public bool RemoveEgg
        {
            get
            {
                return removeEgg;
            }
        }

        public virtual int EggsGiven
        {
            get
            {
                return 1;
            }
        }

        public bool CanPickUpEgg
        {
            get
            {
                return canPickUp;
            }
        }
    }
}
