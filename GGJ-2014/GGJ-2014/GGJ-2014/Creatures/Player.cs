using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GGJ_2014.Levels;
using Microsoft.Xna.Framework.Input;
using GGJ_2014.Graphics;
using GGJ_2014.MenuSystemNS;

namespace GGJ_2014.Creatures
{
    class Player
        : Person
    {
        public const int EGG_TREE_VALUE = 100;
        public const int GOLDEN_EGG_VALUE = 1000;

        public const int INTERACT_LONG_LENGTH = 32;
        public const int INTERACT_SHORT_LENGTH = 16;

        public static float Strength;
        public static float Compassion;
        public static float Luck;
        public static int Eggs;

        private Rectangle interactRectangle;
        private Creature lastInteractor;

        public Player(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
            interactRectangle = new Rectangle(0, 0, INTERACT_LONG_LENGTH, INTERACT_SHORT_LENGTH);
            SyncInteractCollider();
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
            for (int i = 0; i < Level.GetInstance().EggList.Count; ++i)
            {
                if (this.collisionBox.Intersects(Level.GetInstance().EggList[i].CollisionBox))
                {
                    Level.GetInstance().EggList.RemoveAt(i);
                    --i;
                    ++Eggs;
                }
            }
            base.Update(gameTime);
            CheckForInteraction();


            if (Eggs >= 1000000)
            {
                Console.WriteLine("WIN!");
                MenuSystem.GetInstance().SwitchToMenuScreenOfType(MenuScreenType.WIN_MENU);
                MenuSystem.GetInstance()
                    .GetMenuScreenOfType(MenuScreenType.WIN_MENU)
                    .AddControl(new MenuBorderedTextItem(
                        new Vector2(10, 10),
                        Color.White,
                        "You Win!"
                ));
            }
        }

        public void CheckForInteraction()
        {
            SyncInteractCollider();
            if (lastInteractor != null && !collisionBox.Intersects(interactRectangle))
            {
                lastInteractor = null;
                //IniateInteraction();
                return;
            }
            List<Creature> creatures = Level.GetInstance().CreatureList;
            for (int c = 0; c < creatures.Count; c++)
            {
                if (creatures[c] != this && creatures[c].CollitionBox.Intersects(interactRectangle))
                {
                    lastInteractor = creatures[c];
                    //IniateInteraction();
                    return;
                }
            }
        }

        public void SyncInteractCollider()
        {
            Vector2 currentPosition = MiddlePosition;
            switch (directionFacing)
            {
                case Direction.NORTH:
                    interactRectangle = new Rectangle((int)currentPosition.X - INTERACT_SHORT_LENGTH / 2, (int)currentPosition.Y - INTERACT_LONG_LENGTH, INTERACT_SHORT_LENGTH, INTERACT_LONG_LENGTH);
                    break;
                case Direction.EAST:
                    interactRectangle = new Rectangle((int)currentPosition.X, (int)currentPosition.Y - INTERACT_SHORT_LENGTH / 2, INTERACT_LONG_LENGTH, INTERACT_SHORT_LENGTH);
                    break;
                case Direction.SOUTH:
                    interactRectangle = new Rectangle((int)currentPosition.X - INTERACT_SHORT_LENGTH / 2, (int)currentPosition.Y, INTERACT_SHORT_LENGTH, INTERACT_LONG_LENGTH);
                    break;
                case Direction.WEST:
                    interactRectangle = new Rectangle((int)currentPosition.X - INTERACT_LONG_LENGTH, (int)currentPosition.Y - INTERACT_SHORT_LENGTH / 2, INTERACT_LONG_LENGTH, INTERACT_SHORT_LENGTH);
                    break;
            }
        }

        public void IniateInteraction()
        {
            if (lastInteractor == null)
            {
                return;
            }
            lastInteractor.Interact(this);
            Console.WriteLine(lastInteractor.Identifyer);
        }

        public void UseObject()
        {
            Tile tile = GetTileInFrontOf();
            if (tile != null)
            {
                switch (tile.Type)
                {
                    case Textures.TILE_PINETREE_ON_GRASS:
                        ChopDownTree(tile);
                        return;
                }
            }
            //Interact with the thing!
            if (lastInteractor != null)
            {
                IniateInteraction();
            }
        }

        private void ChopDownTree(Tile tree)
        {
            tree.Texture = TextureStorage.GetInstance().GetTexture(Textures.TILE_PINETREE_STUMP);
            tree.IsSolid = false;
            tree.Type = Textures.TILE_PINETREE_STUMP;
            Eggs += EGG_TREE_VALUE;
        }
    }
}
