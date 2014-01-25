using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    }
}
