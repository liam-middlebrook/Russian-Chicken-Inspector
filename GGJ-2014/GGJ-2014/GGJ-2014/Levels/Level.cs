using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GGJ_2014.Graphics;
using GGJ_2014.Levels;
using GGJ_2014.Creatures;

namespace GGJ_2014.Levels
{
    class Level
    {
        //GENERATION VALUES
        //VILLAGES & HOUSES
        public const int MIN_NUMBER_OF_VILLAGES = 2;
        public const int MAX_NUMBER_OF_VILLAGES = 10;
        public const int MIN_VILLAGE_SIZE = 20;
        public const int MAX_VILLAGE_SIZE = 30;
        public const int MIN_NUMBER_OF_HOUSES = 3;
        public const int MAX_NUMBER_OF_HOUSES = 6;
        public const int VILLAGE_BUFFER_SIZE = 10;
        //FORESTS
        public const int MIN_NUMBER_OF_FORESTS = 8;
        public const int MAX_NUMBER_OF_FORESTS = 25;
        public const int MIN_FOREST_DENSITY = 10;
        public const int MAX_FOREST_DENSITY = 90;
        public const int MIN_FOREST_SIZE = 6;
        public const int MAX_FOREST_SIZE = 30;
        public const float FOREST_GRASS_DENSITY = 0.8f;
        //GRASS PLANE
        public const int MIN_NUMBER_OF_GRASS = 10;
        public const int MAX_NUMBER_OF_GRASS = 20;
        public const int MIN_GRASS_SIZE = 5;
        public const int MAX_GRASS_SIZE = 30;
        public const int MIN_BUSH_DENSITY = 4;
        public const int MAX_BUSH_DENSITY = 10;
        public const int GRASS_DENSITY = 100;
        //ROCK
        public const int MIN_NUMBER_OF_ROCKS = 1;
        public const int MAX_NUMBER_OF_ROCKS = 4;
        public const int MIN_ROCK_SIZE = 20;
        public const int MAX_ROCK_SIZE = 30;

        //SPAWN VARIABLES
        public const float SPAWN_CHICKEN = 0.001f;
        public const float SPAWN_GOLDEN_EGG = 0.00023f;
        public const float SPAWN_VILLAGER = 0.008f;

        //SPAWN CAPS
        public const int MAX_CHICKENS = 500;
        public const int MAX_VILLAGERS = 80;

        private Random rand = new Random();
        private int numberChickens = 0;
        private int numberVillagers = 0;

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
        private List<Creature> creatures;
        private List<Egg> eggs;
        private List<Rectangle> housesPositions;
        private List<Rectangle> villagePositions;
        private List<Interactable> interactables;

        public int NumberOfTrees = 0;
        public Level()
        {
            LoadLevel();
        }

        public void LoadLevel()
        {
            tiles = new Tile[128, 128];
            creatures = new List<Creature>();
            eggs = new List<Egg>();
            housesPositions = new List<Rectangle>();
            villagePositions = new List<Rectangle>();
            interactables = new List<Interactable>();
            numberChickens = 0;

            List<Textures> availableTiles = new List<Textures>(new[] { Textures.TILE_COBBLESTONE, Textures.TILE_DIRT, Textures.TILE_GRASS });
            Textures tileToDraw = availableTiles[rand.Next(0, availableTiles.Count)];

            //DIRT
            FillTilesIn(new Rectangle(0, 0, Width, Height), Textures.TILE_DIRT, false);

            //GRASS PLANES
            int numberOfGrass = rand.Next(MIN_NUMBER_OF_GRASS, MAX_NUMBER_OF_GRASS);
            for (int g = 0; g < numberOfGrass; g++)
            {
                int width = rand.Next(MIN_GRASS_SIZE, MAX_GRASS_SIZE);
                int height = rand.Next(MIN_GRASS_SIZE, MAX_GRASS_SIZE);
                int x = rand.Next(0, Width - width);
                int y = rand.Next(0, Height - height);
                FadeFillTexure(new Rectangle(x, y, width, height), Textures.TILE_GRASS, false, GRASS_DENSITY / 100.0f);
                FillInClumpTile(new Rectangle(x, y, width, height), Textures.TILE_TREE_ON_GRASS, true, rand.Next(MIN_BUSH_DENSITY, MAX_BUSH_DENSITY) / 100.0f);
            }

            //ROCKS
            int numberOfRocks = rand.Next(MIN_NUMBER_OF_ROCKS, MAX_NUMBER_OF_ROCKS);
            for (int r = 0; r < numberOfRocks; r++)
            {
                int width = rand.Next(MIN_ROCK_SIZE, MAX_ROCK_SIZE);
                int height = rand.Next(MIN_ROCK_SIZE, MAX_ROCK_SIZE);
                int x = rand.Next(0, Width - width);
                int y = rand.Next(0, Height - height);
                FadeFillTexure(new Rectangle(x, y, width, height), Textures.TILE_COBBLESTONE, false, 1.0f);
            }

            //FORESTS
            int numberOfForest = rand.Next(MIN_NUMBER_OF_FORESTS, MAX_NUMBER_OF_FORESTS);
            for (int f = 0; f < numberOfForest; f++)
            {
                int width = rand.Next(MIN_FOREST_SIZE, MAX_FOREST_SIZE);
                int height = rand.Next(MIN_FOREST_SIZE, MAX_FOREST_SIZE);
                int x = rand.Next(0, Width - width);
                int y = rand.Next(0, Height - height);
                FadeFillTexure(new Rectangle(x, y, width, height), Textures.TILE_GRASS, false, FOREST_GRASS_DENSITY);
                FillInClumpTile(new Rectangle(x, y, width, height), Textures.TILE_PINETREE_ON_GRASS, true, rand.Next(MIN_FOREST_DENSITY, MAX_FOREST_DENSITY) / 100.0f);
            }

            //VILLAGES
            int numberOfVillages = rand.Next(MIN_NUMBER_OF_VILLAGES, MAX_NUMBER_OF_VILLAGES);
            for (int v = 0; v < numberOfVillages; v++)
            {
                int width = rand.Next(MIN_VILLAGE_SIZE, MAX_VILLAGE_SIZE);
                int height = rand.Next(MIN_VILLAGE_SIZE, MAX_VILLAGE_SIZE);
                PlaceVillage(new Rectangle(rand.Next(VILLAGE_BUFFER_SIZE, Width - VILLAGE_BUFFER_SIZE - width), rand.Next(VILLAGE_BUFFER_SIZE, Height - VILLAGE_BUFFER_SIZE - height), width, height), rand.Next(MIN_NUMBER_OF_HOUSES, MAX_NUMBER_OF_HOUSES));
            }

            TraceRectangleTiles(new Rectangle(0, 0, Width, Height), Textures.TILE_BRICK_WALL, true);

            for (int c = 0; c < rand.Next(4, 30); c++)
            {
                SpawnChicken();
            }
            for (int e = 0; e < rand.Next(1, 5); e++)
            {
                SpawnGoldenEgg();
            }
            for (int v = 0; v < rand.Next(5, 30); v++)
            {
                SpawnVillager();
            }
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (tiles[x,y] != null && tiles[x, y].Type == Textures.TILE_PINETREE_ON_GRASS)
                    {
                        ++NumberOfTrees;
                    }
                }
            }
                        Console.WriteLine(NumberOfTrees + " Trees Generated");
        }

        private void FadeFillTexure(Rectangle bounds, Textures texture, bool isSolid, float density)
        {
            FillInClumpTile(new Rectangle(bounds.X - bounds.Width / 10, bounds.Y - bounds.Height / 10, bounds.Width + bounds.Width / 5, bounds.Height + bounds.Height / 5), texture, isSolid, density);
            FillInClumpTile(new Rectangle(bounds.X - bounds.Width / 6, bounds.Y - bounds.Height / 6, bounds.Width + bounds.Width / 3, bounds.Height + bounds.Height / 3), texture, isSolid, density / 2);
            FillInClumpTile(new Rectangle(bounds.X - bounds.Width / 2, bounds.Y - bounds.Height / 2, bounds.Width + bounds.Width, bounds.Height + bounds.Height), texture, isSolid, density / 4);
        }

        private void PlaceVillage(Rectangle bounds, int numberOfHouses)
        {
            int minX = GetTileIndexInBoundsX(bounds.X);
            int maxX = GetTileIndexInBoundsX(bounds.X + bounds.Width);
            int minY = GetTileIndexInBoundsY(bounds.Y);
            int maxY = GetTileIndexInBoundsY(bounds.Y + bounds.Height);

            int width = maxX - minX;
            int height = maxY - minY;

            Rectangle village = new Rectangle(minX, minY, width, height);

            for (int v = 0; v < villagePositions.Count; v++)
            {
                if (villagePositions[v].Intersects(village))
                {
                    return;
                }
            }
            villagePositions.Add(village);

            FillTilesIn(new Rectangle(bounds.X - 1, bounds.Y - 1, bounds.Width + 2, bounds.Height + 2), Textures.TILE_PAVEMENT, false);

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
            Textures floorTexture = Textures.TILE_WOOD_PLANK;
            Textures wallTexture = Textures.TILE_BRICK_WALL;

            int minX = GetTileIndexInBoundsX(bounds.X);
            int maxX = GetTileIndexInBoundsX(bounds.X + bounds.Width);
            int minY = GetTileIndexInBoundsY(bounds.Y);
            int maxY = GetTileIndexInBoundsY(bounds.Y + bounds.Height);

            int width = maxX - minX;
            int height = maxY - minY;

            Rectangle house = new Rectangle(minX + 1, minY + 1, width - 1, height - 1);
            for (int h = 0; h < housesPositions.Count; h++)
            {
                if (housesPositions[h].Intersects(house))
                {
                    return;
                }
            }

            housesPositions.Add(house);

            FillTilesIn(new Rectangle(minX, minY, width, height), floorTexture, false);//Floor

            TraceRectangleTiles(new Rectangle(minX, minY, width, height), wallTexture, true);

            //Door stuff
            int x = rand.Next(minX+1, maxX-1);
            int y = minY;
            if (rand.Next(0, 2) == 0)
            {
                y = maxY - 1;
            }
            tiles[x, y] = new Tile(floorTexture, new Vector2(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE), false);
        }

        private void TraceRectangleTiles(Rectangle bounds, Textures tileTexture, bool isSolid)
        {
            int minX = GetTileIndexInBoundsX(bounds.X);
            int maxX = GetTileIndexInBoundsX(bounds.X + bounds.Width);
            int minY = GetTileIndexInBoundsY(bounds.Y);
            int maxY = GetTileIndexInBoundsY(bounds.Y + bounds.Height);

            int width = maxX - minX;
            int height = maxY - minY;

            FillTilesIn(new Rectangle(minX, minY, 1, height), tileTexture, isSolid);//Left wall
            FillTilesIn(new Rectangle(maxX - 1, minY, 1, height), tileTexture, isSolid);//Right Wall
            FillTilesIn(new Rectangle(minX, minY, width, 1), tileTexture, isSolid);//Top Wall
            FillTilesIn(new Rectangle(minX, maxY - 1, width, 1), tileTexture, isSolid);//Bottom Wall
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
                    tiles[x, y] = new Tile(tileTexture, new Vector2(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE), isSolid);
                }
            }
        }

        private void FillInClumpTile(Rectangle bounds, Textures tileTexture, bool isSolid, float percent)
        {
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
                        tiles[x, y] = new Tile(tileTexture, new Vector2(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE), isSolid);
                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int e = eggs.Count - 1; e > -1; e--)
            {
                if (eggs[e].RemoveEgg)
                {
                    eggs.RemoveAt(e);
                }
                else
                {
                    eggs[e].Update(gameTime);
                }
            }
            for (int c = creatures.Count-1; c > -1; c--)
            {
                if (creatures[c].IsAlive())
                {
                    creatures[c].Update(gameTime);
                }
                else
                {
                    if(creatures[c] is Chicken)
                    {
                        numberChickens --;
                    }
                    else if(creatures[c] is Villager)
                    {
                        numberVillagers--;
                    }
                    creatures.RemoveAt(c);
                }
            }
            HandleSpawns(gameTime);
        }

        private void HandleSpawns(GameTime gameTime)
        {
            double chance = rand.NextDouble();
            if (chance < SPAWN_CHICKEN/numberChickens + Player.Luck/300.0f)
            {
                SpawnChicken();
            }
            if (chance < SPAWN_GOLDEN_EGG + Player.Luck/1000.0f)
            {
                SpawnGoldenEgg();
            }
            if (chance < SPAWN_VILLAGER)
            {
                SpawnVillager();
            }
        }

        private void SpawnChicken()
        {
            AddCreature(new Chicken(FindSpawnPlace(new Rectangle(0, 0, Width, Height))));
            Console.WriteLine("CHICKEN Spwaned!");
        }

        private void SpawnGoldenEgg()
        {
            AddEgg(new GoldenEgg(FindSpawnPlace(housesPositions[rand.Next(0, housesPositions.Count)]) - new Vector2(Tile.TILE_SIZE / 4, Tile.TILE_SIZE / 4)));
            Console.WriteLine("Golden Egg Spawned!");
        }

        private void SpawnVillager()
        {
            AddCreature(new Villager(FindSpawnPlace(housesPositions[rand.Next(0, housesPositions.Count)]) - new Vector2(Tile.TILE_SIZE / 4, Tile.TILE_SIZE / 4)));
            Console.WriteLine("Spawn Villager");
        }

        private Vector2 FindSpawnPlace(Rectangle bounds)
        {
            Vector2 spawnPlace = new Vector2(-1, -1);
            int x = 0;
            int y = 0;
            Tile tile;
            int timesTried = 0;
            while (spawnPlace.X == -1 && spawnPlace.Y == -1 && timesTried < 10)
            {
                x = rand.Next(bounds.X, bounds.X + bounds.Width);
                y = rand.Next(bounds.Y, bounds.Y + bounds.Height);
                tile = GetTile(x, y);
                if (tile != null && !tile.IsSolid)
                {
                    spawnPlace = new Vector2(x * Tile.TILE_SIZE + Tile.TILE_SIZE / 2, y * Tile.TILE_SIZE + Tile.TILE_SIZE / 2);
                }
            }
            return spawnPlace;
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
            for (int e = 0; e < eggs.Count; e++)
            {
                eggs[e].Draw(spriteBatch);
            }
        }

        public void AddCreature(Creature creature)
        {
            if (creature is Chicken)
            {
                if (numberChickens > MAX_CHICKENS)
                {
                    return;
                }
                numberChickens++;
            }
            else if (creature is Villager)
            {
                if(numberVillagers > MAX_VILLAGERS)
                {
                    return;
                }
                numberVillagers++;
            }
            if (creature is Interactable)
            {
                interactables.Add(creature);
            }
            creatures.Add(creature);
        }

        public void AddEgg(Egg egg)
        {
            if (egg is Interactable)
            {
                interactables.Add((Interactable)egg);
            }
            eggs.Add(egg);
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

        //DO NOT DIRECTLY ADD TO EGG LIST
        public List<Egg> EggList
        {
            get { return eggs; }
        }

        public List<Creature> CreatureList
        {
            get
            {
                return creatures;
            }
        }

        public List<Interactable> InteractableList
        {
            get
            {
                return interactables;
            }
        }
    }
}
