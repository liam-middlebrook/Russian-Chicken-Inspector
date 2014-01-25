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
        //GENERATION VALUES
        //VILLAGES & HOUSES
        public const int MIN_NUMBER_OF_VILLAGES = 1;
        public const int MAX_NUMBER_OF_VILLAGES = 10;
        public const int MIN_VILLAGE_SIZE = 20;
        public const int MAX_VILLAGE_SIZE = 30;
        public const int MIN_NUMBER_OF_HOUSES = 3;
        public const int MAX_NUMBER_OF_HOUSES = 6;
        public const int VILLAGE_BUFFER_SIZE = 10;
        //FORESTS
        public const int MIN_NUMBER_OF_FORESTS = 5;
        public const int MAX_NUMBER_OF_FORESTS = 30;
        public const int MIN_FOREST_DENSITY = 10;
        public const int MAX_FOREST_DENSITY = 90;
        public const int MIN_FOREST_SIZE = 3;
        public const int MAX_FOREST_SIZE = 30;
        public const float FOREST_GRASS_DENSITY = 0.8f;


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
            FillTilesIn(new Rectangle(0, 0, Width, Height), Textures.TILE_DIRT, false);

            int numberOfForest = rand.Next(MIN_NUMBER_OF_FORESTS, MAX_NUMBER_OF_FORESTS);
            for (int f = 0; f < numberOfForest; f++)
            {
                int width = rand.Next(MIN_FOREST_SIZE, MAX_FOREST_SIZE);
                int height = rand.Next(MIN_FOREST_SIZE, MAX_FOREST_SIZE);
                //FillInClumpTile();
                FillInClumpTile(new Rectangle(rand.Next(0, Width - width), rand.Next(0, Height - height), width, height), Textures.TILE_PINETREE_ON_GRASS, true, rand.Next(MIN_FOREST_DENSITY, MAX_FOREST_DENSITY) / 100.0f);
            }


            int numberOfVillages = rand.Next(MIN_NUMBER_OF_VILLAGES, MAX_NUMBER_OF_VILLAGES);
            for (int v = 0; v < numberOfVillages; v++)
            {
                int width = rand.Next(MIN_VILLAGE_SIZE, MAX_VILLAGE_SIZE);
                int height = rand.Next(MIN_VILLAGE_SIZE, MAX_VILLAGE_SIZE);
                PlaceVillage(new Rectangle(rand.Next(VILLAGE_BUFFER_SIZE, Width - VILLAGE_BUFFER_SIZE - width), rand.Next(VILLAGE_BUFFER_SIZE, Height - VILLAGE_BUFFER_SIZE - height), width, height), rand.Next(MIN_NUMBER_OF_HOUSES, MAX_NUMBER_OF_HOUSES));
            }
        }

        private void PlaceVillage(Rectangle bounds, int numberOfHouses)
        {
            Random rand = new Random();

            FillTilesIn(new Rectangle(bounds.X -1, bounds.Y - 1, bounds.Width + 2, bounds.Height + 2), Textures.TILE_PAVEMENT, false);

            int minX = GetTileIndexInBoundsX(bounds.X);
            int maxX = GetTileIndexInBoundsX(bounds.X + bounds.Width);
            int minY = GetTileIndexInBoundsY(bounds.Y);
            int maxY = GetTileIndexInBoundsY(bounds.Y + bounds.Height);

            int width = maxX - minX;
            int height = maxY - minY;

            int averageHouseSize = (int)Math.Sqrt(width * height / numberOfHouses * 1.2f)/2;
            for (int t = 0; t < numberOfHouses / 2; t++)
            {
                PlaceHouse(new Rectangle(minX + t * averageHouseSize*2, minY, averageHouseSize + rand.Next(0, 4), averageHouseSize + rand.Next(0, 4)));
            }
            for (int b = 0; b < numberOfHouses - (numberOfHouses / 2); b++)
            {
                int roomHeight = averageHouseSize + rand.Next(0, 4);
                PlaceHouse(new Rectangle(minX + b * averageHouseSize * 2, maxY - roomHeight, averageHouseSize + rand.Next(0, 4), roomHeight));
            }
        }

        private void PlaceHouse(Rectangle bounds)
        {
            Random rand = new Random();

            Textures floorTexture = Textures.TILE_COBBLESTONE;
            Textures wallTexture = Textures.NONE;

            int minX = GetTileIndexInBoundsX(bounds.X);
            int maxX = GetTileIndexInBoundsX(bounds.X + bounds.Width);
            int minY = GetTileIndexInBoundsY(bounds.Y);
            int maxY = GetTileIndexInBoundsY(bounds.Y + bounds.Height);

            int width = maxX - minX;
            int height = maxY - minY;

            FillTilesIn(new Rectangle(minX, minY, width, height), floorTexture, false);//Floor

            FillTilesIn(new Rectangle(minX, minY, 1, height), wallTexture, true);//Left wall
            FillTilesIn(new Rectangle(maxX - 1, minY, 1, height), wallTexture, true);//Right Wall
            FillTilesIn(new Rectangle(minX, minY, width, 1), wallTexture, true);//Top Wall
            FillTilesIn(new Rectangle(minX, maxY - 1, width, 1), wallTexture, true);//Bottom Wall

            //Door stuff
            int x = rand.Next(minX+2, maxX-1);
            int y = minY;
            if (rand.Next(0, 2) == 0)
            {
                y = maxY - 1;
            }
            tiles[x, y] = new Tile(TextureStorage.GetInstance().GetTexture(floorTexture), new Vector2(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE), false);
            if (rand.Next(0, 2) == 0)
            {
                x += 1;
            }
            else
            {
                x -= 1;
            }
            tiles[x, y] = new Tile(TextureStorage.GetInstance().GetTexture(floorTexture), new Vector2(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE), false);
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
