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
    //Each player's gamespace holds their score and blocks and paddle
    class GameSpace
    {
        private const int HORIZONTAL_PADDING = 30;

        public enum Player 
        {
            One,
            Two
        }
        const float BLOCK_DISTANCE_FROM_TOP = 25;

        private List<FadeBlock> m_blocks;
        public Paddle m_Paddle;
        public Paddle m_PaddleBehind;
        public int[] mFadeBlocks; 
        private Player m_WhichPlayer;

        public List<FadeBlock> Blocks
        {
            get { return m_blocks; }
        }
        public GameSpace(Player p1)
        {
            m_WhichPlayer = p1;
            m_blocks = new List<FadeBlock>();
            for (int i = 0; i < 6; ++i)
            {
                FadeBlock block = new FadeBlock();
                m_blocks.Add(block);
            }
            m_Paddle = new Paddle();
            m_PaddleBehind = new Paddle();
            mFadeBlocks = new int[6];
            for (int i = 0; i < mFadeBlocks.Length; ++i)
            {
                mFadeBlocks[i] = i + 1;
            }
        }

        public void LoadContent(ContentManager content, GameWindow window)
        {
            m_Paddle.LoadContent(content, m_WhichPlayer, window);
            m_PaddleBehind.LoadContent(content, m_WhichPlayer, window);
            int x = 0;
            foreach (FadeBlock block in m_blocks)
            {
                block.LoadContent(content);
                ++x;
            }
            float xWhere = (m_WhichPlayer == Player.One) ? window.ClientBounds.Width *0.25f : (window.ClientBounds.Width * 0.75f - m_blocks[0].Source.Width);
            x = 0;
            foreach (FadeBlock block in m_blocks)
            {
                block.SetPosition(xWhere, BLOCK_DISTANCE_FROM_TOP + (block.Source.Height * x));
                ++x;
            }

            if (m_WhichPlayer == Player.One)
            {
                m_Paddle.Position = new Vector2(xWhere + HORIZONTAL_PADDING + m_blocks[0].Source.Width - m_Paddle.Source.Width, window.ClientBounds.Height / 2 - m_Paddle.Source.Height/2);
                m_PaddleBehind.Position = new Vector2(xWhere - HORIZONTAL_PADDING, window.ClientBounds.Height / 2 - m_PaddleBehind.Source.Height / 2);
            }
            else
            {
                m_PaddleBehind.Position = new Vector2(xWhere + HORIZONTAL_PADDING + m_blocks[0].Source.Width - m_Paddle.Source.Width, window.ClientBounds.Height / 2 - m_PaddleBehind.Source.Height / 2);
                m_Paddle.Position = new Vector2(xWhere - HORIZONTAL_PADDING, window.ClientBounds.Height / 2 - m_Paddle.Source.Height / 2);
            }
        }

        public void Update(GameTime gametime)
        {
            foreach (FadeBlock block in m_blocks)
            {
                block.Update(gametime);
            }

            m_Paddle.MovingUp = 0;
            m_PaddleBehind.MovingUp = 0;
            KeyboardState curKeyState = Keyboard.GetState();
            if (m_WhichPlayer == Player.One)
            {
                if (curKeyState.IsKeyDown(Keys.W) == true && !(curKeyState.IsKeyDown(Keys.S) == true))
                {
                    m_Paddle.MovingUp = -1;
                    m_PaddleBehind.MovingUp = -1;
                }
                else if (curKeyState.IsKeyDown(Keys.S) == true && !(curKeyState.IsKeyDown(Keys.W) == true))
                {
                    m_Paddle.MovingUp = 1;
                    m_PaddleBehind.MovingUp = 1;
                }
                
            }
            else if (m_WhichPlayer == Player.Two)
            {
                if (curKeyState.IsKeyDown(Keys.Up) == true && !(curKeyState.IsKeyDown(Keys.Down) == true))
                {
                    m_Paddle.MovingUp = -1;
                    m_PaddleBehind.MovingUp = -1;
                }
                else if (curKeyState.IsKeyDown(Keys.Down) == true && !(curKeyState.IsKeyDown(Keys.Up) == true))
                {
                    m_Paddle.MovingUp = 1;
                    m_PaddleBehind.MovingUp = 1;
                }
            }
            m_Paddle.Update(gametime, curKeyState);
            m_PaddleBehind.Update(gametime, curKeyState);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (FadeBlock block in m_blocks)
            {
                block.Draw(spriteBatch);
            }
            m_Paddle.Draw(spriteBatch);
            m_PaddleBehind.Draw(spriteBatch);
        }

        public void AddColliders(List<Sprite> collideList)
        {
            foreach (FadeBlock block in m_blocks)
            {
                collideList.Add(block);
            }
            collideList.Add(m_Paddle);
            collideList.Add(m_PaddleBehind);
        }

        public bool RemoveBlock(FadeBlock block)
        {
            FadeBlock temp = null;
            foreach (FadeBlock myBlock in m_blocks)
            {
                if (myBlock.Equals(block))
                {
                    temp = myBlock;
                }
            }
            if (temp != null)
            {
                m_blocks.Remove(temp);
                return true;
            }
            return false;
        }
    }
}
