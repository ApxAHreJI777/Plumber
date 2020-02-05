using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Plumber
{
    public class TileGrid
    {
        public Tile[,] tileArr;
        int W, H;
        public int SX { get; private set; }
        public int SY { get; private set; }
        int FX, FY, FDir;
        public bool victory { get; private set; }

        public TileGrid(int w, int h)
        {
            W = w;
            H = h;
            tileArr = new Tile[H, W];
            victory = false;
            createRandomPass();
        }

        public void reset()
        {
            victory = false;
            createRandomPass();
        }

        public void createRandomPass()
        {
            char[,] field;
            FX = Form1.rnd.Next(H / 2, W);
            FY = Form1.rnd.Next(H / 2, W);
            if (FX >= H - 1 && FY >= W - 1)
            {
                FDir = Form1.rnd.Next(2);
                if (FDir > 0) FDir = FDir + 2;
            }
            else if (FX >= H - 1)
            {
                FDir = Form1.rnd.Next(3);
                if (FDir > 0) FDir++;
            }
            else if (FY >= W - 1)
            {
                FDir = Form1.rnd.Next(3);
                if (FDir > 1) FDir++;
            }
            else FDir = Form1.rnd.Next(4);
            if (tileArr[FX, FY] == null)
                tileArr[FX, FY] = new Tile();
            else
                tileArr[FX, FY].reset();
            tileArr[FX, FY].newTileType(TileType.Finish);
            tileArr[FX, FY].setRotation(FDir);

            if (Form1.rnd.Next(100) < 50)
            {
                SX = Form1.rnd.Next(1, W / 2);
                SY = 0;
            }
            else
            {
                SY = Form1.rnd.Next(1, W / 2);
                SX = 0;
            }
            RandomPass rndPass = new RandomPass(W, H);
            field = rndPass.createPass(SX, SY, FX, FY, FDir);
            for (int i = 0; i < H; i++)
                for (int j = 0; j < W; j++)
                {
                    if (field[i, j] == 'I' || field[i, j] == 'L')
                    {
                        if (tileArr[i, j] == null)
                            tileArr[i, j] = new Tile();
                        else
                            tileArr[i, j].reset();
                        if (field[i, j] == 'I')
                        {
                            tileArr[i, j].newTileType(TileType.Straight);
                        }
                        else if (field[i, j] == 'L')
                        {
                            tileArr[i, j].newTileType(TileType.Corner);
                            
                        }
                        tileArr[i, j].setRotation(Form1.rnd.Next(4));
                    }
                    else if (field[i, j] == '_')
                    {
                        if (tileArr[i, j] == null)
                            tileArr[i, j] = new Tile();
                        else
                            tileArr[i, j].reset();
                        tileArr[i, j].newTileType(
                            Form1.rnd.Next(100) < 50 ? 
                            TileType.Straight : 
                            TileType.Corner);
                        tileArr[i, j].setRotation(Form1.rnd.Next(4));
                    }
                }
        }

        public void checkStartTile()
        {
            checkAdjacentTiles(SX, SY);
        }

        public void checkAdjacentTiles(int x, int y)
        {
            tileArr[x, y].setFilled(false);
            tileArr[x, y].fillAllNext(false);
            for (int i = 0; i < 4; i++)
            {
                if (tileArr[x, y].sides[i] && !tileArr[x, y].Filled)
                    victory = checkTile(x, y, i);
            }
        }

        public bool checkTile(int x, int y, int i)
        {
            int ox = x, oy = y, oi= 0;
            int[] r = getCoordSide(x, y, i);
            ox = r[0]; oy = r[1]; oi = r[2];
            if (r[0] != -1 && r[1] != -1)
            {
                if (tileArr[ox, oy].sides[oi])
                {
                    if (tileArr[ox, oy].Filled)
                    {
                        tileArr[x, y].setFilled(true);
                        tileArr[ox, oy].setNext(tileArr[x, y]);
                        tileArr[x, y].setPrevious(tileArr[ox, oy]);
                        return checkOtherSide(x, y, i);
                    }
                }
            }
            else if (x == SX && y == SY)
            {
                if (SX == 0 && i == 3)
                {
                    tileArr[x, y].setFilled(true);
                    return checkOtherSide(x, y, i);
                }
                if (SY == 0 && i == 0)
                {
                    tileArr[x, y].setFilled(true);
                    return checkOtherSide(x, y, i);
                }

            }
            return false;
        }

        public bool checkOtherSide(int x, int y, int i)
        {
            int[] nr = getCoordSide(x, y, tileArr[x, y].getOtherSide(i));
            if (nr[0] != -1 && nr[1] != -1)
                if (tileArr[nr[0], nr[1]].sides[nr[2]])
                {
                    if (tileArr[nr[0], nr[1]].tileType != TileType.Finish)
                        return checkTile(nr[0], nr[1], nr[2]);
                    else
                    {
                        tileArr[nr[0], nr[1]].setFilled(true);
                        return true;
                    }
                }
            return false;
        }

        public int[] getCoordSide(int x, int y, int i)
        {
            int[] res = new int[3];
            res[2] = (i + 2) % 4;
            switch (i)
            {
                case 0: res[0] = x; res[1] = y - 1; break;
                case 1: res[0] = x + 1; res[1] = y; break;
                case 2: res[0] = x; res[1] = y + 1; break;
                case 3: res[0] = x - 1; res[1] = y; break;
            }
            if (res[0] >= H) res[0] = -1;
            if (res[1] >= W) res[1] = -1;
            return res;
        }
    }
}