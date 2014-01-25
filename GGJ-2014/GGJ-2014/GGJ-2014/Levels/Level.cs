﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GGJ_2014.Graphics;
using GGJ_2014.Levels;

namespace GGJ_2014.Levels
{
    class Level
    {
        private Tile[,] tiles;
        private List<Creature> creatures = new List<Creature>();

        public Level()
        {
            LoadLevel();
        }

        public void LoadLevel()
        {
            tiles = new Tile[10, 10];

            Random rand = new Random();

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    tiles[x, y] = new Tile(TextureStorage.GetInstance().GetTexture(Textures.COBBLESTONE), new Vector2(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE), false);
                }
            }
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
            for (int x = GetTileIndexInBoundsX(-Camera.ViewBounds.X/Tile.TILE_SIZE-1); x < GetTileIndexInBoundsX((-Camera.ViewBounds.X + Camera.ViewBounds.Width)/Tile.TILE_SIZE + 1); x++)
            {
                for (int y = GetTileIndexInBoundsY(-Camera.ViewBounds.Y/Tile.TILE_SIZE-1); y < GetTileIndexInBoundsY((-Camera.ViewBounds.Y + Camera.ViewBounds.Height)/Tile.TILE_SIZE + 1); y++)
                {
                    if (tiles[x, y] != null)
                    {
                        tiles[x, y].Draw(spriteBatch);
                    }
                }
            }

            //Optomise?
            for (int c = 0; c < creatures.Count; c++)
            {
                creatures[c].Draw(spriteBatch);
            }
        }

        public void AddCreature(Creature creature)
        {
            creatures.Add(creature);
        }

        public void RemoveCreature(Creature creature)
        {
            creatures.Remove(creature);
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