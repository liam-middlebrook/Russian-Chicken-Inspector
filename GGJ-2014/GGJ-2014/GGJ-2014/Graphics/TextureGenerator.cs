using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GGJ_2014.Levels;

namespace GGJ_2014.Graphics
{
    enum Textures
    {
        GRASS,
        DIRT,
        COBBLESTONE
    }

    class TextureGenerator
    {
        public static Texture2D GenerateTexture(GraphicsDevice graphicsDevice, Textures textureToGenerate)
        {
            Texture2D texture;

            texture = new Texture2D(graphicsDevice, Tile.TILE_SIZE, Tile.TILE_SIZE);

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
