﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GGJ_2014.Levels;
using Microsoft.Xna.Framework.Input;
using GGJ_2014.Graphics;

namespace GGJ_2014.Creatures
{
    class Player
        : Person
    {
        public const int EGG_TREE_VALUE = 100;

        public static float Strength;
        public static float Charisma;
        public static float Intelligence;
        public static int Eggs;

        public Player(Texture2D texture, Vector2 position)
            : base(texture, position)
        {

        }

        public override void HandleInput(KeyboardState keyState)
        {
            Direction direction = 0;
            direction = keyState.IsKeyDown(Keys.W) ? Direction.NORTH : direction;
            direction = keyState.IsKeyDown(Keys.S) ? Direction.SOUTH : direction;
            direction = keyState.IsKeyDown(Keys.A) ? Direction.WEST : direction;
            direction = keyState.IsKeyDown(Keys.D) ? Direction.EAST : direction;
            Walk(direction);

            if (keyState.IsKeyDown(Keys.Space))
            {
                UseObject();
            }
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < Level.GetInstance().EggList.Count; ++i )
            {
                if (this.collisionBox.Intersects(Level.GetInstance().EggList[i].CollisionBox))
                {
                    Level.GetInstance().EggList.RemoveAt(i);
                    --i;
                    ++Eggs;
                }
            }
            base.Update(gameTime);
        }
        public void UseObject()
        {
            Tile tile = GetTileInFrontOf();
            if (tile != null)
            {
                switch (tile.Type)
                {
                    case Textures.TILE_PINETREE_ON_GRASS :
                        ChopDownTree(tile);
                        break;
                }
            }
        }

        private void ChopDownTree(Tile tree)
        {
            tree.Texture = TextureStorage.GetInstance().GetTexture(Textures.TILE_PINETREE_STUMP);
            tree.IsSolid = false;
            Eggs += EGG_TREE_VALUE;
        }
    }
}
