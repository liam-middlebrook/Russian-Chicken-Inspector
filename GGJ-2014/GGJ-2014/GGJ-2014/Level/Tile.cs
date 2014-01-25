using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GGJ_2014.Graphics;

namespace GGJ_2014.Level
{
    class Tile : Sprite
    {
        public const int TILE_SIZE = 32;

        private bool isSolid = false;

        public Tile(Texture2D texture, Vector2 position, bool isSolid)
            : base(texture, position)
        {
            this.isSolid = isSolid;
        }

        public Rectangle TileRectangle
        {
            get
            {
                return new Rectangle((int)base.Position.X, (int)base.Position.Y, TILE_SIZE, TILE_SIZE);
            }
        }

        public bool IsSolid
        {
            get
            {
                return isSolid;
            }
            set
            {
                isSolid = value;
            }
        }
    }
}
