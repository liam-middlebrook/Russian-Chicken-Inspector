using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GGJ_2014.Graphics;

namespace GGJ_2014.Levels
{
    public class Tile : Sprite
    {
        public const int TILE_SIZE = 32;

        private bool isSolid = false;
        private Textures type;

        public Tile(Textures texture, Vector2 position, bool isSolid)
            : base(TextureStorage.GetInstance().GetTexture(texture), position)
        {
            this.isSolid = isSolid;
            this.type = texture;
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

        public Textures Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }
    }
}
