using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GGJ_2014.Graphics
{
    public enum Textures
    {
        NONE,
        BORDERED,
        TILE_GRASS,
        TILE_DIRT,
        TILE_COBBLESTONE,
        TILE_PAVEMENT,
        TILE_TREE_ON_GRASS,
        TILE_PINETREE_ON_GRASS,
        TILE_PINETREE_STUMP,
        TILE_BRICK_WALL,
        TILE_WOOD_PLANK,
        CREATURE_CHICKEN,
        CREATURE_GENERIC
    }

    class TextureGenerator
    {
        public static Texture2D GenerateTexture(GraphicsDevice graphicsDevice, Textures textureToGenerate, Color? color, int width = 0, int height = 0)
        {
            Texture2D texture;

            if (width == 0 && height == 0)
            {
                texture = new Texture2D(graphicsDevice, Levels.Tile.TILE_SIZE, Levels.Tile.TILE_SIZE);
            }
            else
            {
                texture = new Texture2D(graphicsDevice, width, height);
            }
            Color[] textureData = new Color[texture.Width * texture.Height];

            switch (textureToGenerate)
            {
                #region BORDERED_TEXTURE_GENERATOR

                case Textures.BORDERED:
                    {
                        Color fillColor = color ?? Color.White;
                        for (int y = 0; y < texture.Height; y++)
                        {
                            for (int x = 0; x < texture.Width; x++)
                            {
                                if (x == 0 || x == texture.Width - 1 || y == 0 || y == texture.Height - 1)
                                {
                                    textureData[y * texture.Width + x] = Color.Black;
                                }
                                else
                                {
                                    textureData[y * texture.Width + x] = fillColor;
                                }
                            }
                        }
                        break;
                    }

                #endregion

                #region DIRT_GENERATOR

                case Textures.TILE_DIRT:
                    {
                        Color baseColor = Color.Sienna;
                        Color subtractiveColor;
                        Random rand = new Random();
                        double randVal;
                        for (int y = 0; y < texture.Height; y++)
                        {
                            for (int x = 0; x < texture.Width; x++)
                            {
                                randVal = rand.NextDouble();

                                int safeXMax = (int)MathHelper.Clamp(x + 1, 0, texture.Width - 1);
                                int safeYMax = (int)MathHelper.Clamp(y + 1, 0, texture.Height - 1);
                                int safeXMin = (int)MathHelper.Clamp(x - 1, 0, texture.Width - 1);
                                int safeYMin = (int)MathHelper.Clamp(y - 1, 0, texture.Height - 1);

                                if (DarkerThan(textureData[y * texture.Width + safeXMax], baseColor)
                                    || DarkerThan(textureData[y * texture.Width + safeXMin], baseColor)
                                    || DarkerThan(textureData[safeYMax * texture.Width + x], baseColor)
                                    || DarkerThan(textureData[safeYMin * texture.Width + x], baseColor))
                                {
                                    randVal = MathHelper.Clamp((float)randVal - 0.05f, 0.0f, 1.0f);
                                }

                                if (randVal > 0.7)
                                {
                                    subtractiveColor = new Color(0, 0, 0, 0);
                                }
                                else
                                {
                                    subtractiveColor = new Color(25, 25, 25, 0);
                                }
                                textureData[y * texture.Width + x] = AddColor(baseColor, subtractiveColor);
                            }
                        }
                        break;
                    }

                #endregion

                #region BRICK_WALL_GENERATOR

                case Textures.TILE_BRICK_WALL:
                    {
                        Color baseColor = Color.SaddleBrown;
                        Color subtractiveColor;
                        Random rand = new Random();
                        double randVal;
                        for (int y = 0; y < texture.Height; y++)
                        {
                            for (int x = 0; x < texture.Width; x++)
                            {
                                randVal = rand.NextDouble();

                                int safeXMax = (int)MathHelper.Clamp(x + 1, 0, texture.Width - 1);
                                int safeYMax = (int)MathHelper.Clamp(y + 1, 0, texture.Height - 1);
                                int safeXMin = (int)MathHelper.Clamp(x - 1, 0, texture.Width - 1);
                                int safeYMin = (int)MathHelper.Clamp(y - 1, 0, texture.Height - 1);

                                if (DarkerThan(textureData[y * texture.Width + safeXMax], baseColor)
                                    || DarkerThan(textureData[y * texture.Width + safeXMin], baseColor)
                                    || DarkerThan(textureData[safeYMax * texture.Width + x], baseColor)
                                    || DarkerThan(textureData[safeYMin * texture.Width + x], baseColor))
                                {
                                    randVal = MathHelper.Clamp((float)randVal - 0.05f, 0.0f, 1.0f);
                                }

                                if (randVal > 0.7)
                                {
                                    subtractiveColor = new Color(45, 45, 45, 0);
                                }
                                else
                                {
                                    subtractiveColor = new Color(85, 85, 85, 0);
                                }
                                textureData[y * texture.Width + x] = SubtractColor(baseColor, subtractiveColor);
                            }
                        }
                        break;
                    }

                #endregion

                #region WOOD_PLANK_GENERATOR

                case Textures.TILE_WOOD_PLANK:
                    {
                        Color baseColor = Color.SaddleBrown;
                        Color subtractiveColor;
                        Random rand = new Random();
                        double randVal;

                        for (int x = 0; x < texture.Width; x++)
                        {
                            for (int y = 0; y < texture.Height; y++)
                            {
                                randVal = rand.NextDouble();

                                if (randVal > 0.99 || x % 4 == 0 || x % 4 == 1)
                                {
                                    subtractiveColor = new Color(rand.Next(20, 65), rand.Next(20, 65), rand.Next(20, 65), 0);
                                }
                                else
                                {
                                    subtractiveColor = new Color(85, 85, 85, 0);
                                }
                                textureData[y * texture.Width + x] = AddColor(baseColor, subtractiveColor);
                            }
                        }
                        break;
                    }

                #endregion

                #region GRASS_GENERATOR

                case Textures.TILE_GRASS:
                    {
                        Color baseColor = Color.MediumSeaGreen;
                        Color subtractiveColor;
                        Random rand = new Random();
                        double randVal;
                        for (int y = 0; y < texture.Height; y++)
                        {
                            for (int x = 0; x < texture.Width; x++)
                            {
                                randVal = rand.NextDouble();

                                int safeXMax = (int)MathHelper.Clamp(x + 1, 0, texture.Width - 1);
                                int safeYMax = (int)MathHelper.Clamp(y + 1, 0, texture.Height - 1);
                                int safeXMin = (int)MathHelper.Clamp(x - 1, 0, texture.Width - 1);
                                int safeYMin = (int)MathHelper.Clamp(y - 1, 0, texture.Height - 1);

                                if (DarkerThan(textureData[y * texture.Width + safeXMax], baseColor)
                                    || DarkerThan(textureData[y * texture.Width + safeXMin], baseColor)
                                    || DarkerThan(textureData[safeYMax * texture.Width + x], baseColor)
                                    || DarkerThan(textureData[safeYMin * texture.Width + x], baseColor))
                                {
                                    randVal = MathHelper.Clamp((float)randVal - 0.05f, 0.0f, 1.0f);
                                }

                                if (randVal > 0.7)
                                {
                                    subtractiveColor = new Color(25, 25, 25, 0);
                                }
                                else if (randVal > 0.5)
                                {
                                    subtractiveColor = new Color(0, 0, 0, 0);
                                }
                                else
                                {
                                    subtractiveColor = new Color(25, 25, -25, 0);
                                }
                                textureData[y * texture.Width + x] = SubtractColor(baseColor, subtractiveColor);
                            }
                        }
                        break;
                    }

                #endregion

                #region TREE_ON_GRASS_GENERATOR

                case Textures.TILE_TREE_ON_GRASS:
                    {
                        Color baseColor = Color.MediumSeaGreen;
                        Color subtractiveColor;
                        Random rand = new Random();
                        double randVal;
                        for (int y = 0; y < texture.Height; y++)
                        {
                            for (int x = 0; x < texture.Width; x++)
                            {
                                randVal = rand.NextDouble();

                                int safeXMax = (int)MathHelper.Clamp(x + 1, 0, texture.Width - 1);
                                int safeYMax = (int)MathHelper.Clamp(y + 1, 0, texture.Height - 1);
                                int safeXMin = (int)MathHelper.Clamp(x - 1, 0, texture.Width - 1);
                                int safeYMin = (int)MathHelper.Clamp(y - 1, 0, texture.Height - 1);

                                if (DarkerThan(textureData[y * texture.Width + safeXMax], baseColor)
                                    || DarkerThan(textureData[y * texture.Width + safeXMin], baseColor)
                                    || DarkerThan(textureData[safeYMax * texture.Width + x], baseColor)
                                    || DarkerThan(textureData[safeYMin * texture.Width + x], baseColor))
                                {
                                    randVal = MathHelper.Clamp((float)randVal - 0.05f, 0.0f, 1.0f);
                                }

                                if (randVal > 0.7)
                                {
                                    subtractiveColor = new Color(25, 25, 25, 0);
                                }
                                else if (randVal > 0.5)
                                {
                                    subtractiveColor = new Color(0, 0, 0, 0);
                                }
                                else
                                {
                                    subtractiveColor = new Color(25, 25, -25, 0);
                                }
                                textureData[y * texture.Width + x] = SubtractColor(baseColor, subtractiveColor);
                            }
                        }


                        for (int i = 0; i < 5; i++)
                        {
                            int a = rand.Next(-10, 30);
                            
                            textureData = AddCircle(
                                textureData,
                                new Vector2(texture.Width, texture.Height),
                                rand.Next(8, 12),
                                new Vector2(rand.Next(8, 24), rand.Next(8, 24)),
                                SubtractColor(Color.ForestGreen, new Color(a, a, a, 0))
                                );
                        }


                        break;
                    }

                #endregion

                #region PINETREE_ON_GRASS_GENERATOR

                case Textures.TILE_PINETREE_ON_GRASS:
                    {
                        Color baseColor = Color.MediumSeaGreen;
                        Color subtractiveColor;
                        Random rand = new Random();
                        double randVal;
                        for (int y = 0; y < texture.Height; y++)
                        {
                            for (int x = 0; x < texture.Width; x++)
                            {
                                randVal = rand.NextDouble();

                                int safeXMax = (int)MathHelper.Clamp(x + 1, 0, texture.Width - 1);
                                int safeYMax = (int)MathHelper.Clamp(y + 1, 0, texture.Height - 1);
                                int safeXMin = (int)MathHelper.Clamp(x - 1, 0, texture.Width - 1);
                                int safeYMin = (int)MathHelper.Clamp(y - 1, 0, texture.Height - 1);

                                if (DarkerThan(textureData[y * texture.Width + safeXMax], baseColor)
                                    || DarkerThan(textureData[y * texture.Width + safeXMin], baseColor)
                                    || DarkerThan(textureData[safeYMax * texture.Width + x], baseColor)
                                    || DarkerThan(textureData[safeYMin * texture.Width + x], baseColor))
                                {
                                    randVal = MathHelper.Clamp((float)randVal - 0.05f, 0.0f, 1.0f);
                                }

                                if (randVal > 0.7)
                                {
                                    subtractiveColor = new Color(25, 25, 25, 0);
                                }
                                else if (randVal > 0.5)
                                {
                                    subtractiveColor = new Color(0, 0, 0, 0);
                                }
                                else
                                {
                                    subtractiveColor = new Color(25, 25, -25, 0);
                                }
                                textureData[y * texture.Width + x] = SubtractColor(baseColor, subtractiveColor);
                            }
                        }

                        textureData = AddRectangle(
                            textureData,
                            new Vector2(texture.Width, texture.Height),
                            new Rectangle(15, 8, 3, 24),
                            Color.SaddleBrown
                            );

                        for (int i = 0; i < 5; i++)
                        {
                            int a = rand.Next(-10, 30);

                            textureData = AddTriangle(
                                textureData,
                                new Vector2(texture.Width, texture.Height),
                                new[] { new Vector2(16, rand.Next(0, 10)), new Vector2(rand.Next(0, 8), rand.Next(14, 24)), new Vector2(rand.Next(24, 32), rand.Next(14, 24)) },
                                SubtractColor(Color.ForestGreen, new Color(a, a, a, 20))
                                );
                        }



                        break;
                    }

                #endregion

                #region PINETREE_STUMP_GENERATOR

                case Textures.TILE_PINETREE_STUMP:
                    {
                        Color baseColor = Color.MediumSeaGreen;
                        Color subtractiveColor;
                        Random rand = new Random();
                        double randVal;
                        for (int y = 0; y < texture.Height; y++)
                        {
                            for (int x = 0; x < texture.Width; x++)
                            {
                                randVal = rand.NextDouble();

                                int safeXMax = (int)MathHelper.Clamp(x + 1, 0, texture.Width - 1);
                                int safeYMax = (int)MathHelper.Clamp(y + 1, 0, texture.Height - 1);
                                int safeXMin = (int)MathHelper.Clamp(x - 1, 0, texture.Width - 1);
                                int safeYMin = (int)MathHelper.Clamp(y - 1, 0, texture.Height - 1);

                                if (DarkerThan(textureData[y * texture.Width + safeXMax], baseColor)
                                    || DarkerThan(textureData[y * texture.Width + safeXMin], baseColor)
                                    || DarkerThan(textureData[safeYMax * texture.Width + x], baseColor)
                                    || DarkerThan(textureData[safeYMin * texture.Width + x], baseColor))
                                {
                                    randVal = MathHelper.Clamp((float)randVal - 0.05f, 0.0f, 1.0f);
                                }

                                if (randVal > 0.7)
                                {
                                    subtractiveColor = new Color(25, 25, 25, 0);
                                }
                                else if (randVal > 0.5)
                                {
                                    subtractiveColor = new Color(0, 0, 0, 0);
                                }
                                else
                                {
                                    subtractiveColor = new Color(25, 25, -25, 0);
                                }
                                textureData[y * texture.Width + x] = SubtractColor(baseColor, subtractiveColor);
                            }
                        }

                        textureData = AddRectangle(
                            textureData,
                            new Vector2(texture.Width, texture.Height),
                            new Rectangle(15, 24, 3, 8),
                            Color.SaddleBrown
                            );




                        break;
                    }

                #endregion

                #region PAVEMENT_GENERATOR

                case Textures.TILE_PAVEMENT:
                    {
                        Color baseColor = Color.SlateGray;
                        Color subtractiveColor;
                        Random rand = new Random();
                        double randVal;
                        for (int y = 0; y < texture.Height; y++)
                        {
                            for (int x = 0; x < texture.Width; x++)
                            {
                                randVal = rand.NextDouble();

                                int safeXMax = (int)MathHelper.Clamp(x + 1, 0, texture.Width - 1);
                                int safeYMax = (int)MathHelper.Clamp(y + 1, 0, texture.Height - 1);
                                int safeXMin = (int)MathHelper.Clamp(x - 1, 0, texture.Width - 1);
                                int safeYMin = (int)MathHelper.Clamp(y - 1, 0, texture.Height - 1);

                                if (DarkerThan(textureData[y * texture.Width + safeXMax], baseColor)
                                    || DarkerThan(textureData[y * texture.Width + safeXMin], baseColor)
                                    || DarkerThan(textureData[safeYMax * texture.Width + x], baseColor)
                                    || DarkerThan(textureData[safeYMin * texture.Width + x], baseColor))
                                {
                                    randVal = MathHelper.Clamp((float)randVal - 0.05f, 0.0f, 1.0f);
                                }

                                if (randVal > 0.7)
                                {
                                    subtractiveColor = new Color(25, 25, 25, 0);
                                }
                                else if (randVal > 0.5)
                                {
                                    subtractiveColor = new Color(0, 0, 0, 0);
                                }
                                else
                                {
                                    subtractiveColor = new Color(25, 25, 25, 0);
                                }
                                textureData[y * texture.Width + x] = SubtractColor(baseColor, subtractiveColor);
                            }
                        }
                        break;
                    }

                #endregion

                #region COBBLESTONE_GENERATOR

                case Textures.TILE_COBBLESTONE:
                    {
                        Color baseColor = Color.DarkSlateGray;
                        Random rand = new Random();
                        for (int y = 0; y < texture.Height; y++)
                        {
                            for (int x = 0; x < texture.Width; x++)
                            {

                                textureData[y * texture.Width + x] = Color.DarkSlateGray;
                            }

                        }
                        for (int i = 0; i < 25; i++)
                        {
                            int a = rand.Next(-10, 30);
                            textureData = AddCircle(
                                textureData,
                                new Vector2(texture.Width, texture.Height),
                                rand.Next(3, 5),
                                new Vector2(rand.Next(0, 32), rand.Next(0, 32)),
                                AddColor(baseColor, new Color(a, a, a, 0))
                                );
                        }
                        break;
                    }

                #endregion

                #region CREATURE_CHICKEN_GENERATOR

                case Textures.CREATURE_CHICKEN:
                    {
                        Color baseColor = Color.DarkSlateGray;
                        Random rand = new Random();
                        for (int y = 0; y < texture.Height; y++)
                        {
                            for (int x = 0; x < texture.Width; x++)
                            {

                                textureData[y * texture.Width + x] = Color.Transparent;
                            }
                        }
                        int a = rand.Next(-10, 50);
                        textureData = AddCircle(textureData, new Vector2(texture.Width, texture.Height), 12, new Vector2(16, 14), SubtractColor(Color.White, new Color(a, a, a, 0)));
                        //textureData = AddCircle(textureData, new Vector2(texture.Width, texture.Height), 9, new Vector2(16, 10), Color.Black);
                        //textureData = AddCircle(textureData, new Vector2(texture.Width, texture.Height), 8, new Vector2(16, 10), SubtractColor(Color.White, new Color(a, a, a, 0)));
                       textureData = AddTriangle(textureData, new Vector2(texture.Width, texture.Height), new[] { new Vector2(10, 10), new Vector2(24, 10), new Vector2(16, 0) }, SubtractColor(Color.Orange, new Color(a, a, a, 0)));
                        //textureData = AddRectangle(textureData, new Vector2(texture.Width, texture.Height), new Rectangle(14, 12, 5, 8), Color.Red);
                        //textureData = AddTriangle(textureData, new Vector2(texture.Width, texture.Height), new[] { new Vector2(20, 28), new Vector2(26, 28), new Vector2(19, 24) }, SubtractColor(Color.Orange, new Color(a, a, a, 0)));
                        //textureData = AddTriangle(textureData, new Vector2(texture.Width, texture.Height), new[] { new Vector2(6, 28), new Vector2(12, 28), new Vector2(13, 24) }, SubtractColor(Color.Orange, new Color(a, a, a, 0)));
                         textureData = AddCircle(textureData, new Vector2(texture.Width, texture.Height), 2, new Vector2(10, 14), Color.Black);
                        textureData = AddCircle(textureData, new Vector2(texture.Width, texture.Height), 2, new Vector2(22, 14), Color.Black);
                        break;
                    }

                #endregion

                #region CREATURE_GENERIC_GENERATOR

                case Textures.CREATURE_GENERIC:
                    {
                        Color baseColor = Color.White;
                        Random rand = new Random();
                        for (int y = 0; y < texture.Height; y++)
                        {
                            for (int x = 0; x < texture.Width; x++)
                            {

                                textureData[y * texture.Width + x] = Color.Transparent;
                            }
                        }
                        textureData = AddCircle(textureData, new Vector2(texture.Width, texture.Height), width/2, new Vector2(width/2, height/2), baseColor);
                        textureData = AddRectangle(textureData, new Vector2(texture.Width, texture.Height), new Rectangle(width/2-2, 1, 3, height/3), Color.Black);
                        break;
                    }

                #endregion



                #region DEFAULT_TEXTURE_GEN

                default:
                    {
                        for (int y = 0; y < texture.Height; y++)
                        {
                            for (int x = 0; x < texture.Width; x++)
                            {
                                if (y % 4 == 0 || x % 4 == 0)
                                {
                                    textureData[y * texture.Width + x] = Color.Black;
                                }
                                else
                                {
                                    textureData[y * texture.Width + x] = Color.Purple;
                                }
                            }
                        }
                        break;
                    }

                #endregion
            }
            texture.SetData<Color>(textureData);
            return texture;
        }

        private static Color SubtractColor(Color color1, Color color2)
        {

            return new Color(color1.R - color2.R, color1.G - color2.G, color1.B - color2.B, color1.A - color2.A);
        }

        private static Color AddColor(Color color1, Color color2)
        {

            return new Color(color1.R + color2.R, color1.G + color2.G, color1.B + color2.B, color1.A + color2.A);
        }

        private static bool LighterThan(Color color1, Color color2)
        {
            return (color1.R + color1.G + color1.B + color1.A) / 4.0f > (color2.R + color2.G + color2.B + color2.A) / 4.0f;
        }

        private static bool DarkerThan(Color color1, Color color2)
        {
            return (color1.R + color1.G + color1.B + color1.A) / 4.0f < (color2.R + color2.G + color2.B + color2.A) / 4.0f;
        }

        private static Color[] AddCircle(Color[] textureData, Vector2 arraySize, int radius, Vector2 position, Color colorToAdd)
        {
            Random rand = new Random();

            for (int x = 0; x < (int)arraySize.X; x++)
            {
                for (int y = 0; y < (int)arraySize.Y; y++)
                {
                    //y = +/- sqrt(r^2-x^2)
                    //so fill when y <= sqrt(r^2-x^2) AND y >= -sqrt(r^2-x^2)
                    if (y - (int)position.Y <= Math.Sqrt(radius * radius - (x - (int)position.X) * (x - (int)position.X)) && y - (int)position.Y >= -Math.Sqrt(radius * radius - (x - (int)position.X) * (x - (int)position.X)))
                    {
                        textureData[y * (int)arraySize.X + x] = colorToAdd;
                    }
                }
            }
            return textureData;
        }

        private static Color[] AddTriangle(Color[] textureData, Vector2 arraySize, Vector2[] verticies, Color colorToAdd)
        {
            for (int x = 0; x < (int)arraySize.X; x++)
            {
                for (int y = 0; y < (int)arraySize.Y; y++)
                {
                    if (CheckIfPointInTriangle(verticies, new Vector2(x, y)))
                    {
                        textureData[y * (int)arraySize.X + x] = colorToAdd;
                    }
                }
            }
            return textureData;
        }

        private static Color[] AddRectangle(Color[] textureData, Vector2 arraySize, Rectangle rect, Color colorToAdd)
        {
            for (int x = 0; x < (int)arraySize.X; x++)
            {
                for (int y = 0; y < (int)arraySize.Y; y++)
                {
                    if (x > rect.Left && x < rect.Right && y > rect.Top && y < rect.Bottom)
                    {
                        textureData[y * (int)arraySize.X + x] = colorToAdd;
                    }
                }
            }
            return textureData;
        }

        private static bool CheckIfPointInTriangle(Vector2[] verticies, Vector2 point)
        {
            Vector2 v0 = verticies[2] - verticies[0];
            Vector2 v1 = verticies[1] - verticies[0];
            Vector2 v2 = point - verticies[0];

            double dot00 = Vector2.Dot(v0, v0);
            double dot01 = Vector2.Dot(v0, v1);
            double dot02 = Vector2.Dot(v0, v2);
            double dot11 = Vector2.Dot(v1, v1);
            double dot12 = Vector2.Dot(v1, v2);

            double invDenom, u, v;
            invDenom = 1.0 / (dot00 * dot11 - dot01 * dot01);

            u = (dot11 * dot02 - dot01 * dot12) * invDenom;
            v = (dot00 * dot12 - dot01 * dot02) * invDenom;

            return (u >= 0) && (v >= 0) && (u + v < 1);

        }

        private static Vector2 Midpoint(Vector2 vec1, Vector2 vec2)
        {
            return new Vector2((vec1.X + vec2.X) / 2.0f, (vec1.Y + vec2.Y) / 2.0f);
        }
    }
}

                           