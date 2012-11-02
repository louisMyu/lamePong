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
    class SpriteBoxCollision
    {
        public static Corner GetRectangleCornerInRectangle(Rectangle sourceRect, Rectangle targetRect)
        {
            // is top left corner in target rect  
            if ((sourceRect.X >= targetRect.X) && (sourceRect.X <= targetRect.X + targetRect.Width) && (sourceRect.Y >= targetRect.Y) && (sourceRect.Y <= targetRect.Y + targetRect.Height))
            {
                return Corner.TopLeft;
            }
            // is top right corner in target rect  
            else if ((sourceRect.Right >= targetRect.X) && (sourceRect.Right <= targetRect.X + targetRect.Width) && (sourceRect.Y >= targetRect.Y) && (sourceRect.Y <= targetRect.Y + targetRect.Height))
            {
                return Corner.TopRight;
            }
            // is bottom left corner in target rect  
            else if ((sourceRect.X >= targetRect.X) && (sourceRect.X <= targetRect.X + targetRect.Width) && (sourceRect.Bottom >= targetRect.Y) && (sourceRect.Bottom <= targetRect.Y + targetRect.Height))
            {
                return Corner.BottomLeft;
            }
            // is bottom right corner in target rect  
            else if ((sourceRect.Right >= targetRect.X) && (sourceRect.Right <= targetRect.X + targetRect.Width) && (sourceRect.Bottom >= targetRect.Y) && (sourceRect.Bottom <= targetRect.Y + targetRect.Height))
            {
                return Corner.BottomRight;
            }

            return Corner.None;
        }

        public enum Corner
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,
            None,
        }
        
        [Flags]
        public enum SideCollided
        {
            None = 0x00,
            Top = 0x01,
            Bottom = 0x02,
            Left = 0x04,
            Right = 0x08,
        }

        public static SideCollided GetSidesCollided(Vector2 sourcePoint, Rectangle targetRect)
        {
            Vector2 centerLocation = new Vector2(targetRect.Center.X, targetRect.Center.Y);

            SideCollided returnVal = SideCollided.None;

            // test left side  
            /*
            if (sourcePoint.X > targetRect.Left && sourcePoint.X < centerLocation.X &&
                sourcePoint.Y > targetRect.Top && sourcePoint.Y < targetRect.Bottom)
                returnVal = (returnVal | SideCollided.Left);*/
            if (sourcePoint.X > targetRect.Left && sourcePoint.X < targetRect.Left+4 &&
                sourcePoint.Y > targetRect.Top && sourcePoint.Y < targetRect.Bottom)
                returnVal = (returnVal | SideCollided.Left);

            // test top side  
            /*
            if (sourcePoint.X > targetRect.Left && sourcePoint.X < targetRect.Right &&
                sourcePoint.Y > targetRect.Top && sourcePoint.Y < centerLocation.Y)
                returnVal = (returnVal | SideCollided.Top); */
            if (sourcePoint.X > targetRect.Left && sourcePoint.X < targetRect.Right &&
                sourcePoint.Y > targetRect.Top && sourcePoint.Y < targetRect.Top+3)
                returnVal = (returnVal | SideCollided.Top);

            // test right side  
            /*
            if (sourcePoint.X > centerLocation.X && sourcePoint.X < targetRect.Right &&
                sourcePoint.Y > targetRect.Top && sourcePoint.Y < targetRect.Bottom)
                returnVal = (returnVal | SideCollided.Right);*/
            if (sourcePoint.X > targetRect.Right-4 && sourcePoint.X < targetRect.Right &&
                sourcePoint.Y > targetRect.Top && sourcePoint.Y < targetRect.Bottom)
                returnVal = (returnVal | SideCollided.Right);

            // test bottom side
            /*
            if (sourcePoint.X > targetRect.Left && sourcePoint.X < targetRect.Right &&
                sourcePoint.Y > centerLocation.Y && sourcePoint.Y < targetRect.Bottom)
                returnVal = (returnVal | SideCollided.Bottom);*/
            if (sourcePoint.X > targetRect.Left && sourcePoint.X < targetRect.Right &&
                sourcePoint.Y > targetRect.Bottom-3 && sourcePoint.Y < targetRect.Bottom)
                returnVal = (returnVal | SideCollided.Bottom);

            return returnVal;
        } 
    }
}
