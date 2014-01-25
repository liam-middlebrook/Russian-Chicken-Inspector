using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GGJ_2014.Graphics
{
    enum Textures
    {
        NONE,
        GRASS,
        DIRT,
        COBBLESTONE,
        BORDERED
    }

    class TextureGenerator
    {
        public static Texture2D GenerateTexture(GraphicsDevice graphicsDevice, Textures textureToGenerate, int width = 0, int height = 0)
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
                case Textures.DIRT:
                    {
                        Color baseColor = Color.Brown;
                        Color subtractiveColor;
                        Random rand = new Random();
                        double randVal;
                        for (int y = 0; y < texture.Height; y++)
                        {
                            for (int x = 0; x < texture.Width; x++)
                            {
                                randVal = rand.NextDouble();

                                int safeXMax = (int)MathHelper.Clamp(x + 1, 0, texture.Width-1);
                                int safeYMax = (int)MathHelper.Clamp(y + 1, 0, texture.Height-1);
                                int safeXMin = (int)MathHelper.Clamp(x - 1, 0, texture.Width-1);
                                int safeYMin = (int)MathHelper.Clamp(y - 1, 0, texture.Height-1);

                                if ((textureData[y * texture.Width + safeXMax] == AddColor(baseColor, new Color(25, 25, 25, 0)))
                                    || (textureData[y * texture.Width + safeXMin] == AddColor(baseColor, new Color(25, 25, 25, 0)))
                                    || (textureData[safeYMax * texture.Width + x] == AddColor(baseColor, new Color(25, 25, 25, 0)))
                                    || (textureData[safeYMin * texture.Width + x] == AddColor(baseColor, new Color(25, 25, 25, 0))))
                                {
                                    randVal = MathHelper.Clamp((float)randVal - 0.05f, 0.0f, 1.0f);
                                }
                                    
                                if (randVal > 0.7)
                                {
                                    subtractiveColor = new Color(0,0,0,0);
                                }
                                else
                                {
                                    subtractiveColor = new Color(25,25,25,0);
                                }
                                textureData[y * texture.Width + x] = AddColor(baseColor, subtractiveColor);
                            }
                        }
                        break;
                    }

                case Textures.BORDERED:
                    {
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
                                    textureData[y * texture.Width + x] = Color.White;
                                }
                            }
                        }
                        break;
                    }

                #region DEFAULT_TEXTURE_GEN

                default:
                    {
                        for (int y = 0; y < texture.Height; y++)
                        {
                            for (int x = 0; x < texture.Width; x++)
                            {
                                textureData[y * texture.Width + x] = Color.White;
                            }
                        }
                        break;
                    }

                #endregion

            }
            texture.SetData<Color>(textureData);
            return texture;
        }

        public static Color SubtractColor(Color color1, Color color2)
        {

            return new Color(color1.R - color2.R, color1.G - color2.G, color1.B - color2.B, color1.A - color2.A);
        }
        public static Color AddColor(Color color1, Color color2)
        {

            return new Color(color1.R + color2.R, color1.G + color2.G, color1.B + color2.B, color1.A + color2.A);
        }
    }
}
