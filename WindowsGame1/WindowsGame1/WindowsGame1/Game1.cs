using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        Sprite mBackground;

        public static Wall mWallTop;
        public static Wall mWallBottom;
        Ball mBall;

        public static List<Sprite> colliders;

        GameSpace player1;
        GameSpace player2;

        PowerUp powerUp;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            mBackground = new Sprite();
            mBall = new Ball();
            mWallTop = new Wall();
            mWallBottom = new Wall();

            player1 = new GameSpace(GameSpace.Player.One);
            player2 = new GameSpace(GameSpace.Player.Two);
            powerUp = new PowerUp("powerUpTest");
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            mBackground.LoadContent(this.Content, "dvptg");
            mBackground.Position = new Vector2(0, 0);

            mWallTop.LoadContent(this.Content, true, Window);
            mWallBottom.LoadContent(this.Content, false, Window);

            mBall.LoadContent(this.Content);
            mBall.Position = new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);
            colliders = new List<Sprite>();
            colliders.Add(mWallTop);
            colliders.Add(mWallBottom);

            player1.LoadContent(Content, Window);
            player2.LoadContent(Content, Window);

            player1.AddColliders(colliders);
            player2.AddColliders(colliders);

            powerUp.LoadContent(this.Content, Window);
            colliders.Add(powerUp);
            PowerUp.powersDrawn.Add(powerUp);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            PowerUp.UpdatePowerState(gameTime);

            
            foreach (ActivePower power in PowerUp.powersActive)
            {
                if (power.mActivePower == PowerUp.PowerState.Chaos && power.TriggerReady)
                {
                    player1.m_Paddle.Source = new Rectangle(0, 0, (int)player1.m_Paddle.Source.Width, (int)player1.m_Paddle.Source.Height - 1);
                    player1.m_PaddleBehind.Source = new Rectangle(0, 0, (int)player1.m_PaddleBehind.Source.Width, (int)player1.m_PaddleBehind.Source.Height - 1);
                    player2.m_Paddle.Source = new Rectangle(0, 0, (int)player2.m_Paddle.Source.Width, (int)player2.m_Paddle.Source.Height - 1);
                    player2.m_PaddleBehind.Source = new Rectangle(0, 0, (int)player2.m_PaddleBehind.Source.Width, (int)player2.m_PaddleBehind.Source.Height - 1);
                    power.TriggerReady = false;
                }
            }

            player1.Update(gameTime);
            player2.Update(gameTime);
            Sprite gotHit = null;
            mBall.Update(gameTime, Window, player1, player2, colliders, out gotHit);
            if (gotHit != null)
            {
                PowerUp.checkPowerUp(gotHit, colliders);
            }
            PowerUp.UpdatePowerUps(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here            
            spriteBatch.Begin();
            mBackground.Draw(this.spriteBatch);
            mWallTop.Draw(this.spriteBatch);
            mWallBottom.Draw(this.spriteBatch);
            mBall.Draw(this.spriteBatch);
            player1.Draw(this.spriteBatch);
            player2.Draw(this.spriteBatch);

            PowerUp.DrawPowerUps(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
