using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GGJ_2014.Creatures
{
    class Villager
        : Person
    {
        public static Color[] villagerColors = {Color.Khaki, Color.LightGreen, Color.Peru, Color.PaleVioletRed, Color.SkyBlue, Color.SeaGreen};

        private Random random = new Random();

        public Villager(Vector2 position)
            : base(Graphics.TextureStorage.GetInstance().GetTexture(Graphics.Textures.CREATURE_VILLAGER), position)
        {
            Random rand = new Random();
            TintColor = villagerColors[rand.Next(0, villagerColors.Length)];
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
