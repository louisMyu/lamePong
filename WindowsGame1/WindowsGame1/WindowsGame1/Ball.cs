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
    class Ball : Sprite
    {
        private Vector2 m_prevPosition = new Vector2();
        public Vector2 PreviousPosition
        {
            get { return m_prevPosition; }
        }
        const string ASSET_NAME = "ball";

        private Vector2 m_direction;
        private Vector2 m_speed;
        public Vector2 Direction
        {
            get
            {
                return m_direction;
            }
            set
            {
                m_direction = value;
            }
        }
        public Vector2 Speed
        {
            get
            {
                return m_speed;
            }
            set
            {
                m_speed = value;
            }
        }
        public void LoadContent(ContentManager content)
        {
            base.LoadContent(content, ASSET_NAME);
            Position = new Vector2(250, 200);
            Direction = new Vector2(-1f, -1f);
            m_speed = new Vector2(200f, 200f);
        }

        public void Update(GameTime theGameTime, GameWindow window, GameSpace p1, GameSpace p2, List<Sprite> colliders, out Sprite gotHit)
        {
            m_prevPosition = Position;
            if (BoundingBox.Right > window.ClientBounds.Width)
            {
                Position = new Vector2(0, Position.Y);
            }
            if (BoundingBox.Left < 0)
            {
                Position = new Vector2(window.ClientBounds.Width - Source.Width, Position.Y);
            }
            gotHit = CheckCollisions(colliders, p1, p2);
            if (this.Speed.Y >= 300)
            {
                m_speed.Y = 300;
            }
            if (this.Speed.Y <= -300)
            {
                m_speed.Y = 300;
            }
            base.Update(theGameTime, m_speed, Direction);


        }

        public Sprite CheckCollisions(List<Sprite> colliders, GameSpace p1, GameSpace p2)
        {
            foreach (Sprite collider in colliders)
            {
                if (!this.BoundingBox.Intersects(collider.BoundingBox))
                {
                    continue;
                }
                Paddle paddle = collider as Paddle;
                //collision with a paddle
                if (paddle != null)
                {
                    paddle.CollideWithBall(this);
                    return paddle;
                }
                Wall wall = collider as Wall;
                //wall collision
                if (wall != null)
                {
                    if (wall.Equals(Game1.mWallTop))
                    {
                        Position = new Vector2(Position.X, Game1.mWallTop.BoundingBox.Bottom + 1);
                    }
                    else
                    {
                        Position = new Vector2(Position.X, Game1.mWallBottom.BoundingBox.Top - 1 - this.BoundingBox.Height);
                    }
                    this.m_direction.Y *= -1;
                    return wall;
                }
                FadeBlock block = collider as FadeBlock;
                //fadeBlock collision
                if (block != null)
                {
                    colliders.Remove(block);
                    if (!p1.RemoveBlock(block))
                        p2.RemoveBlock(block);
                    this.m_direction.X *= -1;
                    return block;
                }
                PowerUp powerUp = collider as PowerUp;
                //powerUp collision
                if (powerUp != null)
                {
                    return powerUp;
                }
            }
            return null;
        }
    }
}
