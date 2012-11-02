using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace WindowsGame1
{
    class Wall : Sprite
    {
        const string ASSET_NAME = "wall";
        private bool m_top;
        public bool Top
        {
            get
            {
                return m_top;
            }
        }
        public void LoadContent(ContentManager content, bool top, GameWindow window)
        {
            this.m_top = top;
            base.LoadContent(content, ASSET_NAME);
            if (top)
                Position = new Vector2(0,0);
            else
            {
                Position = new Vector2(0, window.ClientBounds.Height - Source.Height);
            }
        }

        public void Update(GameTime theGameTime)
        {
            base.Update(theGameTime, Vector2.One, Vector2.Zero);
        }
    }
}
