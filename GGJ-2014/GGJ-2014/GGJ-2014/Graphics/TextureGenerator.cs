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
        GRASS,
        DIRT,
        COBBLESTONE,
        DEFAULT
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
                        for (int y = 0; y < texture.Height; y++)
                        {
                            for (int x = 0; x < texture.Width; x++)
                            {
                                textureData[y * texture.Width + x] = Color.Brown;
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
    }
}
