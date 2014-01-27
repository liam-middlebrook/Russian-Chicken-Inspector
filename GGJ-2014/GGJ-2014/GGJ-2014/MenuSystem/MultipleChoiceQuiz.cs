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
        float compassion;

        int questionSetIndex;
        
        public MultipleChoiceQuiz()
            :base(Vector2.Zero)
        {
            questionSets = new List<List<MenuControl>>();
            questionSets.Add(new List<MenuControl>());
            questionSets[0].Add(
                new MenuBorderedTextItem(
                    new Vector2(10, 10),
                    Color.PeachPuff,
                    "What would you rather spend your day doing?"
                ));
            questionSets[0].Add(
                new MenuButton(
                    new Vector2(10, 60),
                    "[1] Talking with people",
                    Color.PeachPuff,
                    () =>
                    {
                        compassion += 0.4f;
                        ++questionSetIndex;
                    },
                    Keys.D1
                ));
            questionSets[0].Add(
                new MenuButton(
                    new Vector2(10, 110),
                    "[2] Working out",
                    Color.PeachPuff,
                    () =>
                    {
                        strength += 0.6f;
                        ++questionSetIndex;
                    },
                    Keys.D2
                ));
            questionSets[0].Add(
                new MenuButton(
                    new Vector2(10, 160),
                    "[3] Killing Bunnies",
                    Color.PeachPuff,
                    () =>
                    {
                        compassion -= 2.0f;
                        strength += 0.5f;
                        ++questionSetIndex;
                    },
                    Keys.D3
                ));
            questionSets.Add(new List<MenuControl>());
            questionSets[1].Add(
                new MenuBorderedTextItem(
                    new Vector2(10, 10),
                    Color.PeachPuff,
                    "When you see someone in need of help what do you do?"
                ));
            questionSets[1].Add(
                new MenuButton(
                    new Vector2(10, 60),
                    "[1] Ignore them",
                    Color.PeachPuff,
                    () =>
                    {
                        compassion -= 0.2f;
                        ++questionSetIndex;
                    },
                    Keys.D1
                ));
            questionSets[1].Add(
                new MenuButton(
                    new Vector2(10, 110),
                    "[2] Help Them",
                    Color.PeachPuff,
                    () =>
                    {
                        compassion += 0.4f;
                        ++questionSetIndex;
                    },
                    Keys.D2
                ));
            questionSets[1].Add(
                new MenuButton(
                    new Vector2(10, 160),
                    "[3] Kick them",
                    Color.PeachPuff,
                    () =>
                    {
                        compassion -= 0.5f;
                        strength += 0.5f;
                        ++questionSetIndex;
                    },
                    Keys.D3
                ));

            questionSets.Add(new List<MenuControl>());
            questionSets[2].Add(
                new MenuBorderedTextItem(
                    new Vector2(10, 10),
                    Color.PeachPuff,
                    "How many trees do you think you could chop down?"
                ));
            questionSets[2].Add(
                new MenuButton(
                    new Vector2(10, 60),
                    "[1] I don't want to chop down trees.",
                    Color.PeachPuff,
                    () =>
                    {
                        compassion += 0.7f;
                        ++questionSetIndex;
                    },
                    Keys.D1
                ));
            questionSets[2].Add(
                new MenuButton(
                    new Vector2(10, 110),
                    "[2] None",
                    Color.PeachPuff,
                    () =>
                    {
                        strength -= 0.2f;
                        ++questionSetIndex;
                    },
                    Keys.D2
                ));
            questionSets[2].Add(
                new MenuButton(
                    new Vector2(10, 160),
                    "[3] A few",
                    Color.PeachPuff,
                    () =>
                    {
                        strength += 0.3f;
                        ++questionSetIndex;
                    },
                    Keys.D3
                ));
            questionSets[2].Add(
                new MenuButton(
                    new Vector2(10, 210),
                    "[4] The whole forest",
                    Color.PeachPuff,
                    () =>
                    {
                        compassion -= 0.3f;
                        strength += 1.0f;
                        ++questionSetIndex;
                    },
                    Keys.D4
                ));


            questionSets.Add(new List<MenuControl>());
            questionSets[3].Add(
                new MenuBorderedTextItem(
                    new Vector2(10, 10),
                    Color.PeachPuff,
                    "Are you Ready to Play?"
                ));
            questionSets[3].Add(
                new MenuButton(
                    new Vector2(10, 60),
                    "[1] Yes",
                    Color.PeachPuff,
                    () =>
                    {
                        ++questionSetIndex;
                    },
                    Keys.D1
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
                Random rand = new Random();
                GGJ_2014.Creatures.Player.Compassion = compassion;
                GGJ_2014.Creatures.Player.Luck = Math.Abs((float)rand.NextDouble()*2 - 1.0f)+0.01f;
                GGJ_2014.Creatures.Player.Strength = strength;
                MenuSystem.GetInstance().CurrentScreen.menuControls.Remove(this);
                MenuSystem.GetInstance().SwitchToMenuScreenOfType(MenuScreenType.GAMEPLAY);
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
