using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GGJ_2014.Graphics;

namespace GGJ_2014.Level
{
    class Level
    {
        private Tile[,] tiles;
        private List<Creature> creatures = new List<Creature>();

        public void LoadLevel()
        {

        }

        public void Update(GameTime gameTime)
        {
            for (int c = 0; c < creatures.Count; c++)
            {
                creatures[c].Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = GetTileIndexInBoundsX(Camera.ViewBounds.X); GetTileIndexInBoundsX(Camera.ViewBounds.X + Camera.ViewBounds.Width) < Width; x++)
            {
                for (int y = GetTileIndexInBoundsY(Camera.ViewBounds.Y); GetTileIndexInBoundsY(Camera.ViewBounds.Y + Camera.ViewBounds.Height) < Height; y++)
                {
                    tiles[x, y].Draw(spriteBatch);
                }
            }

            //Optomise?
            for (int c = 0; c < creatures.Count; c++)
            {
                creatures[c].Draw(spriteBatch);
            }
        }

        public Tile GetTile(int x, int y)
        {
            if (IsTileIndexInBounds(x, y))
            {
                return tiles[x, y];
            }
            return null;
        }

        public bool IsTileIndexInBounds(int x, int y)
        {
            return x > 0 && x < Width && y > 0 && y < Height;
        }

        public int GetTileIndexInBoundsX(int x)
        {
            return (int)MathHelper.Clamp(x, 0, Width - 1);
        }

        public int GetTileIndexInBoundsY(int y)
        {
            return (int)MathHelper.Clamp(y, 0, Height - 1);
        }

        public int Width
        {
            get
            {
                return tiles.GetLength(0);
            }
        }

        public int Height
        {
            get
            {
                return tiles.GetLength(1);
            }
        }
    }
}
