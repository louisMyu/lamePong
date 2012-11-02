using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class Sprite
    {
        private Vector2 mPosition = new Vector2(0, 0);
        private Texture2D mSpriteTexture;

        public string AssetName;
        public Rectangle Size;
        private float m_Scale = 1.0f;

        Rectangle mSource;

        public Rectangle Source
        {
            get { return mSource;}
            set
            {
                mSource = value;
                Size = new Rectangle(0, 0, (int)(mSource.Width * Scale), (int)(mSource.Height * Scale));
            }
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)mPosition.X, (int)mPosition.Y, Source.Width, Source.Height);
            }
        }
        public Vector2 Position
        {
            get
            {
                return mPosition;
            }
            set
            {
                mPosition = value;
            }
        }

        public float Scale
        {
            get { return m_Scale; }
            set 
            {
                m_Scale = value;
                //Recalculate the size of the sprite with the value
                Size = new Rectangle(0, 0, (int)(Source.Width * Scale), (int)(Source.Height * Scale));
            }
        }
        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            mSpriteTexture = theContentManager.Load<Texture2D>(theAssetName);
            AssetName = theAssetName;
            Source = new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height);
            Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));
        }

        public void Update(GameTime gameTime, Vector2 theSpeed, Vector2 direction)
        {
            Position += direction * theSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(mSpriteTexture, Position, new Rectangle(0,0, (int)(Source.Width * Scale), (int)(Source.Height * Scale)), Color.White, 0.0f,
                                    Vector2.Zero, Scale, SpriteEffects.None, 0);
        }
    }
}
