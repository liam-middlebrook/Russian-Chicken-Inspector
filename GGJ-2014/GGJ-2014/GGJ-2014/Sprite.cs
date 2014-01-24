using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GGJ_2014
{
    class Sprite
    {
        #region Fields

        private Texture2D texture;
        private Vector2 position;
        private Color tintColor;
        private Rectangle? sourceRect;
        private float rotation;
        private Vector2 rotationOrigin;
        private Vector2 scale;
        private SpriteEffects spriteEffects;

        #endregion

        #region Properties

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value ?? texture; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Color TintColor
        {
            get { return tintColor; }
            set { tintColor = value; }
        }

        public Rectangle? SourceRectangle
        {
            get { return sourceRect; }
            set { sourceRect = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public Vector2 RotationOrigin
        {
            get { return rotationOrigin; }
            set { rotationOrigin = value; }
        }

        public Vector2 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public SpriteEffects SpriteFX
        {
            get { return spriteEffects; }
            set { spriteEffects = value; }
        }

        #endregion

        #region Constructors

        public Sprite(Texture2D texture, Vector2 position)
            : this(texture, position, Color.White, null, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None)
        {
        }

        public Sprite(Texture2D texture, Vector2 position, Color tintColor)
            : this(texture, position, tintColor, null, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None)
        {
        }

        public Sprite(Texture2D texture, Vector2 position, Color tintColor, Rectangle? sourceRect)
            : this(texture, position, tintColor, sourceRect, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None)
        {
        }

        public Sprite(Texture2D texture, Vector2 position, Color tintColor, Rectangle? sourceRect, float rotation)
            : this(texture, position, tintColor, sourceRect, rotation, Vector2.Zero, Vector2.One, SpriteEffects.None)
        {
        }

        public Sprite(Texture2D texture, Vector2 position, Color tintColor, Rectangle? sourceRect, float rotation, Vector2 rotationOrigin)
            : this(texture, position, tintColor, sourceRect, rotation, Vector2.Zero, Vector2.One, SpriteEffects.None)
        {
        }

        public Sprite(Texture2D texture, Vector2 position, Color tintColor, Rectangle? sourceRect, float rotation, Vector2 rotationOrigin, Vector2 scale)
            : this(texture, position, tintColor, sourceRect, rotation, Vector2.Zero, scale, SpriteEffects.None)
        {
        }

        public Sprite(Texture2D texture, Vector2 position, Color tintColor, Rectangle? sourceRect, float rotation, Vector2 rotationOrigin, Vector2 scale, SpriteEffects spriteFX)
        {
            this.texture = texture;
            this.position = position;
            this.tintColor = tintColor;
            this.sourceRect = sourceRect;
            this.rotation = rotation;
            this.rotationOrigin = rotationOrigin;
            this.scale = scale;
            this.spriteEffects = spriteFX;
        }

        #endregion

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, sourceRect, tintColor, rotation, rotationOrigin, scale, spriteEffects, 1.0f);
        }

    }
}
