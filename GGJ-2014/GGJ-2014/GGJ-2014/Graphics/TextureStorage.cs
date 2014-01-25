using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GGJ_2014.Graphics
{
    class TextureStorage
    {
        #region SINGLETON_ATTRIBUTES AND METHODS

        static TextureStorage instance;

        public static TextureStorage GetInstance()
        {
            if (instance == null)
            {
                instance = new TextureStorage();
            }
            return instance;
        }

        #endregion
        
        private Dictionary<Textures, Texture2D> textureLookup = new Dictionary<Textures, Texture2D>();

        public void LoadContent(ContentManager content)
        {
            textureLookup.Add(Textures.NONE, content.Load<Texture2D>("noTexture"));
        }

        public void AddTexture(Textures textureID, Texture2D texture)
        {
            textureLookup.Add(textureID, texture);
        }

        public Texture2D GetTexture(Textures textureId)
        {
            try
            {
                return textureLookup[textureId];
            }
            catch(KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return textureLookup[Textures.NONE];
        }

        
    }
}
