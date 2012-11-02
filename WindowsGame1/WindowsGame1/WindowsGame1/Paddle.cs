using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace WindowsGame1
{
    class Paddle : Sprite
    {
        const string ASSET_NAME = "paddle";
        const int START_POSITION_X = 125;
        const int START_POSITION_Y = 245;
        const int WIZARD_SPEED = 600;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_RIGHT = 1;
        const int MOVE_LEFT = -1;

        public enum State
        {
            Still,
            Moving
        }
        State mCurrentState = State.Still;

        Vector2 mDirection = Vector2.Zero;
        Vector2 mSpeed = Vector2.Zero;

        SoundEffect soundEffect;
        
        State mPreviousState = State.Still;
        private int mMoveUp;
        private Vector2 mPreviousPosition;

        public int MovingUp
        {
            get
            {
                return mMoveUp;
            }
            set
            {
                mMoveUp = value;
            }
        }

        public void LoadContent(ContentManager content, GameSpace.Player player1, GameWindow window)
        {
            base.LoadContent(content, ASSET_NAME);

            soundEffect = content.Load<SoundEffect>("blip");
        }

        public void Update(GameTime theGameTime, KeyboardState state)
        {
            if (MovingUp == 1 || MovingUp == -1)
            {
                mCurrentState = State.Moving;
            }
            else
            {
                mCurrentState = State.Still;
            }
            this.UpdateMovement(mMoveUp);
            mPreviousPosition = Position;
            base.Update(theGameTime, mSpeed, mDirection);
            this.CheckWallCollision();

            mPreviousState = mCurrentState;
        }

        public void UpdateMovement(int moveUp)
        {
            if (mCurrentState == State.Moving)
            {
                if (moveUp == -1)
                {
                    if (mSpeed.Y >= WIZARD_SPEED)
                    {
                        mSpeed.Y = WIZARD_SPEED;
                    }
                    else
                    {
                        if (mPreviousState == State.Still)
                        {
                            mSpeed.Y = 250;
                        }
                        else
                        {
                            mSpeed.Y *= 1.2f;
                        }
                    }
                    mDirection.Y = MOVE_UP;
                }
                else if (moveUp == 1)
                {
                    if (mSpeed.Y >= WIZARD_SPEED)
                    {
                        mSpeed.Y = WIZARD_SPEED;
                    }
                    else
                    {
                        if (mPreviousState == State.Still)
                        {
                            mSpeed.Y = 250;
                        }
                        else
                        {
                            mSpeed.Y *= 1.2f;
                        }
                    }
                    mDirection.Y = MOVE_DOWN;
                }
            }
            else
            {
                mSpeed = Vector2.Zero;
                mDirection = Vector2.Zero;
            }
        }
        public void CollideWithBall(Ball ball)
        {
            Vector2 previousBallLocation = ball.Position;
            SpriteBoxCollision.Corner corner= SpriteBoxCollision.GetRectangleCornerInRectangle(ball.BoundingBox, this.BoundingBox);

            Vector2 bCorner = new Vector2();
            if (corner == SpriteBoxCollision.Corner.None) return;
            else if (corner == SpriteBoxCollision.Corner.TopLeft)
            {
                bCorner = ball.Position;
            }
            else if (corner == SpriteBoxCollision.Corner.TopRight)
            {
                bCorner = new Vector2(ball.Position.X + ball.Source.Width, ball.Position.Y);
            }
            else if (corner == SpriteBoxCollision.Corner.BottomLeft)
            {
                bCorner = new Vector2(ball.Position.X, ball.Position.Y + ball.Source.Height);
            }
            else if (corner == SpriteBoxCollision.Corner.BottomRight)
            {
                bCorner = new Vector2(ball.Position.X + ball.Source.Width, ball.Position.Y + ball.Source.Height);
            }

            SpriteBoxCollision.SideCollided side = SpriteBoxCollision.GetSidesCollided(bCorner, this.BoundingBox);

            if (side.HasFlag(SpriteBoxCollision.SideCollided.Left))
            {
                ball.Direction = new Vector2(Math.Abs(ball.Direction.X) * -1, ball.Direction.Y);
                ball.Position = new Vector2(ball.Position.X - 10, ball.Position.Y);
                ball.Speed -= (this.mSpeed *.4f) * mDirection;
            }
            else if (side.HasFlag(SpriteBoxCollision.SideCollided.Right))
            {
                ball.Direction = new Vector2(Math.Abs(ball.Direction.X), ball.Direction.Y);
                ball.Position = new Vector2(ball.Position.X + 10, ball.Position.Y);
                ball.Speed -= (this.mSpeed * .4f) * mDirection;
            }
            if (side.HasFlag(SpriteBoxCollision.SideCollided.Top))
            {
                ball.Direction = new Vector2(ball.Direction.X, Math.Abs(ball.Direction.Y) * -1);
                ball.Position = new Vector2(ball.Position.X, ball.Position.Y - 10);
            }
            else if (side.HasFlag(SpriteBoxCollision.SideCollided.Bottom))
            {
                ball.Direction = new Vector2(ball.Direction.X, Math.Abs(ball.Direction.Y));
                ball.Position = new Vector2(ball.Position.X, ball.Position.Y + 10);
            }

            soundEffect.Play();
        }
        public void CheckWallCollision()
        {
            Rectangle bottom = Game1.mWallBottom.BoundingBox;
            Rectangle top = Game1.mWallTop.BoundingBox;

            if (this.BoundingBox.Intersects(top))
            {
                Position = new Vector2(Position.X, top.Bottom + 1);
                return;
            }
            if (BoundingBox.Intersects(bottom))
            {
                Position = new Vector2(Position.X, bottom.Top - 1 - this.BoundingBox.Height);
            }
            
        }
    }
}