using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GGJ_2014.MenuSystemNS
{
    class MultipleChoiceQuiz : MenuControl
    {
        private List<List<MenuControl>> questionSets;

        float strength;
        float charisma;
        float intelligence;

        int questionSetIndex;
        
        public MultipleChoiceQuiz()
            :base(Vector2.Zero)
        {
            questionSets = new List<List<MenuControl>>();
            questionSets.Add(new List<MenuControl>());
            questionSets[0].Add(
                new MenuBorderedTextItem(
                    new Vector2(10, 10),
                    "Multiple Choice Question 1",
                    Color.Salmon
                ));
            questionSets[0].Add(
                new MenuButton(
                    new Vector2(10, 60),
                    "Multiple Choice 1 Answer 1",
                    Color.White,
                    () =>
                    {
                        intelligence -= 0.2f;
                        charisma += 0.5f;
                        strength -= 0.3f;
                        ++questionSetIndex;
                    }
                ));
            questionSets[0].Add(
                new MenuButton(
                    new Vector2(10, 110),
                    "Multiple Choice 1 Answer 2",
                    Color.White,
                    () =>
                    {
                        intelligence -= 0.4f;
                        charisma -= 0.1f;
                        strength += 0.3f;
                        ++questionSetIndex;
                    }
                ));
            questionSets[0].Add(
                new MenuButton(
                    new Vector2(10, 160),
                    "Multiple Choice 1 Answer 3",
                    Color.White,
                    () =>
                    {
                        intelligence += 0.1f;
                        charisma -= 0.2f;
                        strength += 0.5f;
                        ++questionSetIndex;
                    }
                ));
            questionSets.Add(new List<MenuControl>());
            questionSets[1].Add(
                new MenuBorderedTextItem(
                    new Vector2(10, 10),
                    "Multiple Choice Question 2",
                    Color.Salmon
                ));
            questionSets[1].Add(
                new MenuButton(
                    new Vector2(10, 60),
                    "Multiple Choice 2 Answer 1",
                    Color.White,
                    () =>
                    {
                        intelligence -= 0.2f;
                        charisma += 0.5f;
                        strength -= 0.3f;
                        ++questionSetIndex;
                    }
                ));
            questionSets[1].Add(
                new MenuButton(
                    new Vector2(10, 110),
                    "Multiple Choice 2 Answer 2",
                    Color.White,
                    () =>
                    {
                        intelligence -= 0.4f;
                        charisma -= 0.1f;
                        strength += 0.3f;
                        ++questionSetIndex;
                    }
                ));
            questionSets[1].Add(
                new MenuButton(
                    new Vector2(10, 160),
                    "Multiple Choice 2 Answer 3",
                    Color.White,
                    () =>
                    {
                        intelligence += 0.1f;
                        charisma -= 0.2f;
                        strength += 0.5f;
                        ++questionSetIndex;
                    }
                ));
        }

        public override void Update(KeyboardState keyState, KeyboardState prevKeyState, MouseState mouseState, MouseState prevMouseState)
        {
            if (questionSetIndex < questionSets.Count)
            {
                foreach (MenuControl button in questionSets[questionSetIndex])
                {
                    button.Update(keyState, prevKeyState, mouseState, prevMouseState);
                }
            }
            else
            {
                GGJ_2014.Creatures.Player.Intelligence = intelligence;
                GGJ_2014.Creatures.Player.Charisma = charisma;
                GGJ_2014.Creatures.Player.Strength = strength;
                MenuSystem.GetInstance().CurrentScreen.menuControls.Remove(this);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (questionSetIndex < questionSets.Count)
            {
                foreach (MenuControl button in questionSets[questionSetIndex])
                {
                    button.Draw(spriteBatch);
                }
            }
        }
    }
}
