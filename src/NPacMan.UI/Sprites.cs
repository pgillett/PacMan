﻿using System;
using System.Drawing;
using NPacMan.Game;

namespace NPacMan.UI
{
    public class Sprites
    {
        private Bitmap Gfx;

        public Sprites()
        {
            Gfx = new Bitmap("gfx.png");
            Bonus = new SpriteSource[8];
            for (int i = 0; i < 8; i++)
                Bonus[i] = new SpriteSource(i * 2, 10, 2);
        }
        
        /// <summary>
        /// Render a sprite to the graphics context
        /// Position is top corner of 8 pixel grid
        /// Larger sprites (eg. PacMac, Ghosts) are offset so they are in the centre of the square
        /// </summary>
        /// <param name="g"></param>
        /// <param name="x">Screen X (pixel)</param>
        /// <param name="y">Screen Y (pixel)</param>
        /// <param name="size">Size of sprite on screen (pixel)</param>
        /// <param name="source">Sprite to show</param>
        public void RenderSprite(Graphics g, int x, int y, int size, SpriteSource source)
        {
            size = PixelGrid * source.Size;
            x = x + PixelGrid / 2 - size / 2;
            y = y + PixelGrid / 2 - size / 2;
            g.DrawImage(Gfx, new Rectangle(x, y, size, size), PixelGrid * source.XPos, PixelGrid * source.YPos,
                size, size,
                GraphicsUnit.Pixel);
        }

        public const int PixelGrid = 8;

        public SpriteSource[] Bonus;

        /// <summary>
        /// Get source of ghost sprite
        /// </summary>
        /// <param name="ghostColour"></param>
        /// <param name="direction"></param>
        /// <param name="animated"></param>
        /// <returns></returns>
        public SpriteSource Ghost(GhostColour ghostColour, Direction direction, bool animated)
        {
            int xpos;
            int ypos;
            switch (ghostColour)
            {
                case GhostColour.Red:
                    xpos = 0;
                    ypos = 14;
                    break;
                case GhostColour.Cyan:
                    xpos = 16;
                    ypos = 18;
                    break;
                case GhostColour.Pink:
                    xpos = 0;
                    ypos = 20;
                    break;
                case GhostColour.Orange:
                    xpos = 16;
                    ypos = 20;
                    break;
                case GhostColour.Eyes:
                    xpos = 0;
                    ypos = 22;
                    break;
                case GhostColour.WhiteFlash:
                    xpos = 24;
                    ypos = 12;
                    break;
                case GhostColour.BlueFlash:
                    xpos = 28;
                    ypos = 12;
                    break;
                default:
                    throw new Exception("GhostColour?");
            }

            switch (direction)
            {
                case Direction.Up:
                    xpos += 12;
                    break;
                case Direction.Down:
                    xpos += 4;
                    break;
                case Direction.Left:
                    xpos += 8;
                    break;
                case Direction.Right:
                    break;
                default:
                    throw new Exception("Direction?");
            }

            if (animated)
                xpos += 2;

            return new SpriteSource(xpos, ypos, 2);

        }

        /// <summary>
        /// Points for eating ghosts 200, 400, 800, 1600
        /// </summary>
        /// <param name="multiplier">0-3</param>
        /// <returns></returns>
        public SpriteSource GhostPoints(int multiplier)
        {
            return new SpriteSource(16 + multiplier * 2, 14, 2);
        }

        /// <summary>
        /// Text number (hexadecimal)
        /// </summary>
        /// <param name="number">0-9</param>
        /// <returns></returns>
        public SpriteSource Digit(int number)
        {
            return new SpriteSource(16 + number, 1, 1);
        }

        /// <summary>
        /// Text character upper case letters
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public SpriteSource Character(char character)
        {
            if (character < 'A' || character > 'Z') return new SpriteSource(0, 2, 1);
            if (character == '!') return new SpriteSource(27, 2, 1);
            return new SpriteSource(character - 64, 2, 1);
        }

        /// <summary>
        /// Get source of Pacmac sprite
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="animation">0-3 moving, 0-11 dying</param>
        /// <param name="dying"></param>
        /// <returns></returns>
        public SpriteSource PacMan(Direction direction, int animation, bool dying)
        {
            if (dying)
            {
                return new SpriteSource(8 + animation - 4 * 2, 16, 2);
            }

            if (animation == 2)
            {
                return new SpriteSource(0,16,2);
            }

            int xpos;
            int ypos = 18;
            switch (direction)
            {
                case Direction.Right:
                    xpos = 0;
                    break;
                case Direction.Down:
                    xpos = 2;
                    break;
                case Direction.Left:
                    xpos = 6;
                    break;
                case Direction.Up:
                    xpos = 4;
                    break;
                default:
                    throw new Exception("Direction?");
            }

            if (animation == 1 || animation == 3)
                xpos += 8;

            return new SpriteSource(xpos, ypos, 2);
        }

        public SpriteSource Map(BoardPiece piece)
        {
            switch (piece)
            {
                case BoardPiece.Blank:
                    return new SpriteSource(6, 0, 1);
                case BoardPiece.Pill:
                    return new SpriteSource(16, 0, 1);
                case BoardPiece.PowerAnim1:
                    return new SpriteSource(18, 0, 1);
                case BoardPiece.PowerAnim2:
                    return new SpriteSource(20, 0, 1);
                case BoardPiece.DoubleTopRight:
                    return new SpriteSource(16, 6, 1);
                case BoardPiece.DoubleTopLeft:
                    return new SpriteSource(17,6,1);
                case BoardPiece.DoubleRight:
                    return new SpriteSource(18,6,1);
                case BoardPiece.DoubleLeft:
                    return new SpriteSource(19,6,1);
                case BoardPiece.DoubleBottomRight:
                    return new SpriteSource(20,6,1);
                case BoardPiece.DoubleBottomLeft:
                    return new SpriteSource(21,6,1);
                case BoardPiece.JoinRightHandTop:
                    return new SpriteSource(22, 6, 1);
                case BoardPiece.JoinLeftHandTop:
                    return new SpriteSource(23,6,1);
                case BoardPiece.JoinRightHandBottom:
                    return new SpriteSource(24,6,1);
                case BoardPiece.JoinLeftHandBottom:
                    return new SpriteSource(25,6,1);
                case BoardPiece.DoubleTop:
                    return new SpriteSource(26, 6, 1);
                case BoardPiece.DoubleBottom:
                    return new SpriteSource(28, 6, 1);
                case BoardPiece.Top:
                    return new SpriteSource(30, 6, 1);
                case BoardPiece.Bottom:
                    return new SpriteSource(4, 7, 1);
                case BoardPiece.TopRight:
                    return new SpriteSource(6, 7, 1);
                case BoardPiece.TopLeft:
                    return new SpriteSource(7, 7, 1);
                case BoardPiece.Right:
                    return new SpriteSource(8, 7, 1);
                case BoardPiece.Left:
                    return new SpriteSource(9, 7, 1);
                case BoardPiece.BottomRight:
                    return new SpriteSource(10, 7, 1);
                case BoardPiece.BottomLeft:
                    return new SpriteSource(11, 7, 1);
                case BoardPiece.GhostTopRight:
                    return new SpriteSource(12, 7, 1);
                case BoardPiece.GhostTopLeft:
                    return new SpriteSource(13, 7, 1);
                case BoardPiece.GhostBottomRight:
                    return new SpriteSource(14, 7, 1);
                case BoardPiece.GhostBottomLeft:
                    return new SpriteSource(15, 7, 1);
                case BoardPiece.GhostEndRight:
                    return new SpriteSource(16, 7, 1);
                case BoardPiece.GhostEndLeft:
                    return new SpriteSource(17,7,1);
                case BoardPiece.JoinTopRight:
                    return new SpriteSource(26,7,1);
                case BoardPiece.JoinTopLeft:
                    return new SpriteSource(27, 7, 1);
                case BoardPiece.GhostDoor:
                    return new SpriteSource(15, 6, 1);
                default:
                    throw new Exception("Bad board piece");
            }
        }
    }

    public class SpriteSource
    {
        public int XPos;
        public int YPos;
        public int Size;

        public SpriteSource(int x, int y, int size)
        {
            XPos = x;
            YPos = y;
            Size = size;
        }
    }
}