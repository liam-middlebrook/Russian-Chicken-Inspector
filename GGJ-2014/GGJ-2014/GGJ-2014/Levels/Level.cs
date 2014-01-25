using System;
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
        static Level instance;

        public static Level GetInstance()
        {
            if (instance == null)
            {
                instance = new Level();
            }
            return instance;
        }

        private Tile[,] tiles;
        private List<Creature> creatures = new List<Creature>();

        public Level()
        {
            LoadLevel();
        }

        public void LoadLevel()
        {
            tiles = new Tile[100, 100];

            Random rand = new Random();
            List<Textures> availableTiles = new List<Textures>(new[] { Textures.TILE_COBBLESTONE, Textures.TILE_DIRT, Textures.TILE_GRASS });
            Textures tileToDraw = availableTiles[rand.Next(0, availableTiles.Count)];
            //for (int x = 0; x < Width; x++)
            //{
            //    for (int y = 0; y < Height; y++)
            //    {
            //        bool collide;

            //        double randNum = rand.NextDouble();
            //        double randThreshold = 0.45;

            //        if (tileToDraw == Textures.TILE_TREE_ON_GRASS || tileToDraw == Textures.TILE_PINETREE_ON_GRASS)
            //        {
            //            randThreshold = 0.3;
            //        }
            //        if (randNum >= randThreshold)
            //        {
            //            Textures tileDrawn = tileToDraw;



            //            tileToDraw = availableTiles[rand.Next(0, availableTiles.Count)];

            //            randNum = rand.NextDouble();


            //            if (randNum > 0.99995)
            //            {
            //                tileToDraw = Textures.TILE_PINETREE_ON_GRASS;
            //            }
            //            else if (randNum > 0.9999)
            //            {
            //                tileToDraw = Textures.TILE_TREE_ON_GRASS;
            //            }

            //            availableTiles.Remove(tileToDraw);

            //            availableTiles.Add(tileDrawn);
            //        }
            //        collide = tileToDraw == Textures.TILE_COBBLESTONE || tileToDraw == Textures.TILE_PINETREE_ON_GRASS || tileToDraw == Textures.TILE_TREE_ON_GRASS;
            //        tiles[x, y] = new Tile(TextureStorage.GetInstance().GetTexture(tileToDraw), new Vector2(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE), collide);

            //    }
            //}
            FillTilesIn(new Rectangle(0, 0, Width, Height), Textures.TILE_GRASS, false);
            FillInClumpTile(new Rectangle(0, 0, 10, 10), Textures.TILE_DIRT, false, 0.6f);
        }

        private void FillTilesIn(Rectangle bounds, Textures tileTexture, bool isSolid)
        {
            int minX = GetTileIndexInBoundsX(bounds.X);
            int maxX = GetTileIndexInBoundsX(bounds.X + bounds.Width);
            int minY = GetTileIndexInBoundsY(bounds.Y);
            int maxY = GetTileIndexInBoundsY(bounds.Y + bounds.Height);
            for (int x = minX; x < maxX; x++)
            {
                for (int y = minY; y < maxY; y++)
                {
                    tiles[x, y] = new Tile(TextureStorage.GetInstance().GetTexture(tileTexture), new Vector2(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE), isSolid);
                }
            }
        }

        private void FillInClumpTile(Rectangle bounds, Textures tileTexture, bool isSolid, float percent)
        {
            Random rand = new Random();
            int minX = GetTileIndexInBoundsX(bounds.X);
            int maxX = GetTileIndexInBoundsX(bounds.X + bounds.Width);
            int minY = GetTileIndexInBoundsY(bounds.Y);
            int maxY = GetTileIndexInBoundsY(bounds.Y + bounds.Height);
            for (int x = minX; x < maxX; x++)
            {
                for (int y = minY; y < maxY; y++)
                {
                    if (rand.NextDouble() < percent)
                    {
                        tiles[x, y] = new Tile(TextureStorage.GetInstance().GetTexture(tileTexture), new Vector2(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE), isSolid);
                    }
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
            for (int x = GetTileIndexInBoundsX(-Camera.ViewBounds.X / Tile.TILE_SIZE - 1); x < GetTileIndexInBoundsX((-Camera.ViewBounds.X + Camera.ViewBounds.Width) / Tile.TILE_SIZE + 1); x++)
            {
                for (int y = GetTileIndexInBoundsY(-Camera.ViewBounds.Y / Tile.TILE_SIZE - 1); y < GetTileIndexInBoundsY((-Camera.ViewBounds.Y + Camera.ViewBounds.Height) / Tile.TILE_SIZE + 1); y++)
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
            return x >= 0 && x < Width && y >= 0 && y < Height;
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
