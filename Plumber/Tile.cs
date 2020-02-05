using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Plumber
{
    public enum TileType 
    { 
        Straight = 0,
        Corner = 1,
        Finish = 2,
        Null = 3
    }

    public struct NextTileStruct 
    {
        public Tile tile;
        public int side;
    }

    public class Tile : PictureBox
    {
        
        public TileType tileType { get; private set; }
        public int rotation;
        public bool[] sides = new bool[] { false, false, false, false };
        //0 = 'N', 1 = 'E', 2 = 'S', 3 = 'W'
        public bool Filled { get; private set; }
        Tile next;
        Tile previous;

        public Tile()
        {
            tileType = TileType.Null;
            rotation = 0;
            Filled = false;
            next = null;
            previous = null;
        }

        public void reset()
        {
            tileType = TileType.Null;
            rotation = 0;
            for (int i = 0; i < 4; i++)
                sides[i] = false;
            Filled = false;
            next = null;
            previous = null;

        }

        public void setFilled(bool value)
        {
            Filled = value;
            changeImage();
            redrawRotation();
        }

        public void newTileType(TileType inType)
        {
            tileType = inType;
            changeImage();
        }

        public void setRotation(int n)
        {
            rotation = n;
            changeImage();
            redrawRotation();
        }

        private void redrawRotation()
        {
            switch (tileType)
            {
                case TileType.Corner:
                    switch (rotation)
                    {
                        case 0:
                            this.Image.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                            sides[0] = true;
                            sides[1] = true;
                            break;
                        case 1:
                            this.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            sides[1] = true;
                            sides[2] = true;
                            break;
                        case 2:
                            this.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            sides[2] = true;
                            sides[3] = true;
                            break;
                        case 3:
                            this.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            sides[3] = true;
                            sides[0] = true;
                            break;
                    } 
                    break;
                case TileType.Straight:
                    switch (rotation % 2)
                    {
                        case 0:
                            this.Image.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                            sides[0] = true;
                            sides[2] = true;
                            break;
                        case 1:
                            this.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            sides[1] = true;
                            sides[3] = true;
                            break;
                    } 
                    break;
                case TileType.Finish:
                    switch (rotation)
                    {
                        case 0:
                            this.Image.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                            sides[0] = true;
                            break;
                        case 1:
                            this.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            sides[1] = true;
                            break;
                        case 2:
                            this.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            sides[2] = true;
                            break;
                        case 3:
                            this.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            sides[3] = true;
                            break;
                    }
                    break;
            }
        }

        public void addRotation()
        {
            if (tileType == TileType.Straight || tileType == TileType.Corner)
            {
                for (int i = 0; i < 4; i++)
                    sides[i] = false;

                rotation++;
                setRotation(rotation % 4);
                this.Invalidate();
            }
        }

        private void changeImage()
        {
            switch (tileType)
            {
                case TileType.Straight:
                    if (!Filled)
                        this.Image = Properties.Resources.tube_hor_ns;
                    else
                        this.Image = Properties.Resources.tube_hor_ns_filled;
                    break;
                case TileType.Corner:
                    if (!Filled)
                        this.Image = Properties.Resources.tube_cor_ne;
                    else
                        this.Image = Properties.Resources.tube_cor_ne_filled;
                    break;
                case TileType.Finish:
                    if (!Filled)
                        this.Image = Properties.Resources.tube_finish;
                    else
                        this.Image = Properties.Resources.tube_finish_filled;
                    break;
            }
        }

        public void changeSide(char side, bool value)
        {
            sides[side] = value;
        }
        public void changeSide(char side)
        {
            sides[side] = !sides[side];
        }

        public void setNext(Tile nextTile)
        {
            next = nextTile;
        }

        public Tile getNext()
        {
            return next;
        }

        public void setPrevious(Tile tile)
        {
            previous = tile;
        }

        public Tile getPrevious()
        {
            return previous;
        }

        public int getOtherSide(int inSide)
        {
            for (int i = 0; i < 4; i++)
                if (sides[i] && i != inSide)
                    return i;
            return -1;
        }

        public bool fillAllNext(bool isFilling)
        {
            if (next != null)
            {
                try
                {
                    if (next.getPrevious() == this)
                    {
                        next.setFilled(isFilling);
                        next.fillAllNext(isFilling);
                        return true;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error: " + e);
                }
            }
            return false;
        }

        
    }
}
