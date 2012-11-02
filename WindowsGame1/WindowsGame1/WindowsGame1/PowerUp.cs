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
    class PowerUp : Sprite
    {
        public static List<ActivePower> powersActive = new List<ActivePower>();
        public static List<PowerUp> powersDrawn = new List<PowerUp>();

        public enum PowerState
        {
            Chaos
        }

        string mSpriteName = null;
        private PowerState mPower;
        public PowerState Power
        {
            get
            {
                return mPower;
            }
            set
            {
                mPower = value;
            }
        }

        public PowerUp(string name)
        {
            mSpriteName = name;
            if (name.Equals("powerUpTest")) 
            {
                mPower = PowerState.Chaos;
            }
            
        }

        public void LoadContent(ContentManager content, GameWindow window)
        {
            base.LoadContent(content, mSpriteName);
            if (mSpriteName.Equals("powerUpTest")) 
            {
                System.Diagnostics.Debug.WriteLine("SLDFKN" + Source.Width / 2 + " " + Source.Height / 2);
                Position = new Vector2((window.ClientBounds.Width / 2) - (Source.Width / 2), window.ClientBounds.Height / 2 - (Source.Height / 2)-50);
            }
        }

        public void Update(GameTime gameTime)
        {
            base.Update(gameTime, Vector2.Zero, Vector2.Zero);
        }

        public static void checkPowerUp(Sprite power, List<Sprite> colliders) 
        {
            PowerUp powerUp = power as PowerUp;
            if (powerUp != null)
            {
                ActivePower ap = new ActivePower(powerUp);
                ap.TriggerReady = true;
                powersActive.Add(ap);
                colliders.Remove(powerUp);
                powersDrawn.Remove(powerUp);
            }
        }

        public static void UpdatePowerUps(GameTime gameTime)
        {
            foreach (PowerUp power in powersDrawn)
            {
                power.Update(gameTime);
            }
        }

        public static void DrawPowerUps(SpriteBatch spriteBatch)
        {
            foreach (PowerUp power in powersDrawn)
            {
                power.Draw(spriteBatch);
            }
        }

        public static void UpdatePowerState(GameTime gameTime)
        {
            List<ActivePower> templist = new List<ActivePower>();
            foreach (ActivePower ap in powersActive)
            {
                if (ap.isActive())
                {
                    ap.Update(gameTime);
                }
                if (!ap.readyToDie)
                {
                    templist.Add(ap);
                }
            }
            powersActive = templist;
        }
    }
}