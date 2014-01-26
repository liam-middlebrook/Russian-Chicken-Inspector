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
                    "BAD answer hurts all traits",
                    Color.PeachPuff,
                    () =>
                    {
                        compassion -= 0.2f;
                        strength -= 0.3f;
                        ++questionSetIndex;
                    }
                ));
            questionSets[0].Add(
                new MenuButton(
                    new Vector2(10, 110),
                    "Increase Strength, Lower Compassion",
                    Color.PeachPuff,
                    () =>
                    {
                        compassion -= 0.4f;
                        strength += 0.3f;
                        ++questionSetIndex;
                    }
                ));
            questionSets[0].Add(
                new MenuButton(
                    new Vector2(10, 160),
                    "Increase both traits!",
                    Color.PeachPuff,
                    () =>
                    {
                        compassion += 0.1f;
                        strength += 0.5f;
                        ++questionSetIndex;
                    }
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
                    "Ignore them",
                    Color.PeachPuff,
                    () =>
                    {
                        compassion -= 0.2f;
                        ++questionSetIndex;
                    }
                ));
            questionSets[1].Add(
                new MenuButton(
                    new Vector2(10, 110),
                    "Help Them",
                    Color.PeachPuff,
                    () =>
                    {
                        compassion += 0.4f;
                        ++questionSetIndex;
                    }
                ));
            questionSets[1].Add(
                new MenuButton(
                    new Vector2(10, 160),
                    "Kick them",
                    Color.PeachPuff,
                    () =>
                    {
                        compassion -= 0.5f;
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
