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

        public const float TREE_STRNGTH_VALUE = 0.002f;
        public const float TREE_COMPATION_VALUE = -0.00007f;

        public const int IDENTIFIER_POSITION_X = 10;
        public const int IDENTIFIER_POSITION_Y = 480;

        public static float Strength;
        public static float Compassion;
        public static float Luck;
        public static int Eggs;
        public static int Health;

        private Rectangle interactRectangle;
        private Interactable lastInteractor;
        private bool isInteracting = false;//When spacebar it hit
        private float numberOfTreesChopedThisTick = 0;
        private MenuBorderedTextItem interactIdentifier;

        public Player(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
            interactRectangle = new Rectangle(0, 0, INTERACT_LONG_LENGTH, INTERACT_SHORT_LENGTH);
            SyncInteractCollider();
            Health = 100;
        }

        public override void HandleInput(KeyboardState keyState)
        {
            Direction direction = 0;
            direction = keyState.IsKeyDown(Keys.W) ? Direction.NORTH : direction;
            direction = keyState.IsKeyDown(Keys.S) ? Direction.SOUTH : direction;
            direction = keyState.IsKeyDown(Keys.A) ? Direction.WEST : direction;
            direction = keyState.IsKeyDown(Keys.D) ? Direction.EAST : direction;
            Walk(direction);

            isInteracting = keyState.IsKeyDown(Keys.Space);
            if (isInteracting)
            {
                UseObject();
            }
        }

        public override void Update(GameTime gameTime)
        {
            numberOfTreesChopedThisTick = Math.Max(numberOfTreesChopedThisTick-Strength/8, 0);
            for (int i = 0; i < Level.GetInstance().EggList.Count; ++i)
            {
                if (Level.GetInstance().EggList[i].CanPickUpEgg && this.collisionBox.Intersects(Level.GetInstance().EggList[i].GetCollisionBox()))
                {
                    Health = Math.Min(Health+=1,100);
                    Eggs += Level.GetInstance().EggList[i].EggsGiven;
                    Level.GetInstance().EggList.RemoveAt(i);
                    --i;
                }
            }
            base.Update(gameTime);
            CheckForInteraction();


            if (Eggs >= 100000)
            {
                Console.WriteLine("WIN!");
                MenuSystem.GetInstance().SwitchToMenuScreenOfType(MenuScreenType.WIN_MENU);
            }
            if (Health < 1)
            {
                MenuSystem.GetInstance().SwitchToMenuScreenOfType(MenuScreenType.LOSE_MENU);
            }
        }

        protected override void CollidedWithTile(Tile t)
        {
            base.CollidedWithTile(t);
            if (isInteracting && t.Type == Textures.TILE_PINETREE_ON_GRASS && numberOfTreesChopedThisTick < Strength)
            {
                ChopDownTree(t);
            }
        }

        public void CheckForInteraction()
        {
            SyncInteractCollider();
            if (lastInteractor != null && lastInteractor.IsAlive() && lastInteractor.GetCollisionBox().Intersects(interactRectangle))
            {
                return;
            }
            else
            {
                if (interactIdentifier != null)
                {
                    MenuSystem.GetInstance().CurrentScreen.RemoveControl(interactIdentifier);
                }
                lastInteractor = null;
            }
            List<Interactable> interactables = Level.GetInstance().InteractableList;
            for (int i = interactables.Count-1; i > -1 ; i--)
            {
                if (interactables[i].IsAlive())
                {
                    if (interactables[i] != this && interactables[i].GetCollisionBox().Intersects(interactRectangle))
                    {
                        lastInteractor = interactables[i];
                        interactIdentifier = new MenuBorderedTextItem(new Vector2(IDENTIFIER_POSITION_X, IDENTIFIER_POSITION_Y), Color.PeachPuff, lastInteractor.GetIdentifier()+" Space to Interact");
                        MenuSystem.GetInstance().CurrentScreen.AddControl(interactIdentifier);
                        return;
                    }
                }
                else
                {
                    Level.GetInstance().InteractableList.RemoveAt(i);
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
            Strength += TREE_STRNGTH_VALUE;
            Compassion += TREE_COMPATION_VALUE;
            numberOfTreesChopedThisTick++;
        }
    }
}
