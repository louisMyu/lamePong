using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
namespace WindowsGame1
{
    class ActivePower
    {
        public PowerUp.PowerState mActivePower;
        public float timeLeft;
        public float timeAccumulated;
        public bool readyToDie;
        public bool TriggerReady
        {
            get;
            set;
        }
        public ActivePower(PowerUp power)
        {
            mActivePower = power.Power;
            timeLeft = 100;
            timeAccumulated = 0;
            TriggerReady = false;
            readyToDie = false;
        }

        public bool isActive()
        {
            return timeLeft >= 0;
        }


        public void Update(GameTime gameTime)
        {
            timeAccumulated += (float)gameTime.ElapsedGameTime.Milliseconds / 100;
            timeLeft -= (float)gameTime.ElapsedGameTime.Milliseconds / 100;
            if (mActivePower == PowerUp.PowerState.Chaos)
            {
                if (timeAccumulated >= 1.0 && !TriggerReady)
                {
                    timeAccumulated = 0;
                    TriggerReady = true;
                }
            }
            if (timeLeft <= 0)
            {
                readyToDie = true;
            }
        }
    }
}
