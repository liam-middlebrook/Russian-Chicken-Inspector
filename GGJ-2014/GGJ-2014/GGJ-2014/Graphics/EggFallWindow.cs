using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GGJ_2014.Creatures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GGJ_2014.Graphics
{
    class EggFallWindow
    {
        private List<Vector2> eggs = new List<Vector2>();
        private Random random = new Random();
        public bool isActive = false;

        public EggFallWindow()
        {
            for (int e = 0; e < 10; e++)
            {
                eggs.Add(new Vector2(random.Next(0, 800), random.Next(0, 600)));
            }
        }

        public void Update(GameTime gameTime)
        {
            if (isActive)
            {
                for (int e = 0; e < eggs.Count; e++)
                {
                    eggs[e] += new Vector2(0, 3);
                    if (eggs[e].Y > 700)
                    {
                        eggs[e] = new Vector2(random.Next(0, 800), -100);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                for (int e = 0; e < eggs.Count; e++)
                {
                    spriteBatch.Draw(TextureStorage.GetInstance().GetTexture(Textures.CHICKEN_EGG), new Rectangle((int)eggs[e].X, (int)eggs[e].Y, 16, 32), Color.White);
                }
            }
        }
    }
}
