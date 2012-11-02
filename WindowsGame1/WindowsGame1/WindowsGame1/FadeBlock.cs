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
    class FadeBlock : Sprite
    {
        const string ASSET_NAME = "Block";

        public void LoadContent(ContentManager content)
        {

            base.LoadContent(content, ASSET_NAME);
        }

        public void Update(GameTime theGameTime)
        {
            base.Update(theGameTime, Vector2.One, Vector2.Zero);
        }

        public void SetPosition(float x, float y)
        {
            Position = new Vector2(x, y);
        }
    }
}
