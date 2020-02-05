using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plumber
{
    class RandomPass
    {
        public char[,] field;
        public int pS = 50, pF = 25;
        Random rnd = new Random();
        int SX, SY, FX, FY, CX, CY, pX, pY, FFX, FFY, Fdir;
        int count = 0;
        int trycount = 1;
        int W; int H;
        bool isStart;

        public RandomPass(int w, int h)
        {
            W = w; H = h;
            field = new char[H, W];
            clearfield();
        }

        public char[,] createPass(int sx, int sy, int fx, int fy, int fdir)
        {
            isStart = true;
            SX = sx; SY = sy; FX = fx; FY = fy;
            FFX = FX; FFY = FY; Fdir = fdir;
            switch (Fdir)
            {
                case 0: FY -= 1; break;
                case 1: FX += 1; break;
                case 2: FY += 1; break;
                case 3: FX -= 1; break;
            }
            field[FFX, FFY] = 'F';
            field[FX, FY] = 'f';
            findWay();
            Console.WriteLine("p: " + pX + " " + pY);
            Console.WriteLine("F: " + FFX + " " + FFY);
            Console.WriteLine("f: " + FX + " " + FY);
            return field;
        }

        private void findWay()
        {
            CX = SX;
            CY = SY;
            pX = SX;
            pY = SY;
            while (CX != FX || CY != FY)
            {
                if (count > 150)
                {
                    count = 0;
                    CX = SX;
                    CY = SY;
                    trycount++;
                    clearfield();
                }
                if (CX == FX)
                {
                    if (CY - FY > 0)
                        tryMove(0);
                    else
                        tryMove(2);
                }
                else if (CY == FY)
                {
                    if (CX - FX > 0)
                        tryMove(3);
                    else
                        tryMove(1);
                }
                else if (rnd.Next(100) < 50)
                {
                    if (CX - FX > 0)
                        tryMove(3);
                    else
                        tryMove(1);
                }
                else
                {
                    if (CY - FY > 0)
                        tryMove(0);
                    else
                        tryMove(2);
                }
            }
            if (pX == FFX || pY == FFY)
                field[FX, FY] = 'I';
            else
                field[FX, FY] = 'L';
        }

        private void tryMove(int n)
        {
            count++;
            int chance = rnd.Next(100);
            if (chance > pS)
            {
                if (chance < pS + pF)
                    n = (n + 1) % 4;
                else
                    n = (n + 3) % 4;
            }
            int tX = CX, tY = CY;
            switch (n)
            {
                case 0: tY -= 1; break;
                case 1: tX += 1; break;
                case 2: tY += 1; break;
                case 3: tX -= 1; break;
            }
            if (tX >= 0 && tY >= 0 && tX < H && tY < W)
            {
                if (field[tX, tY] == '_' || field[tX, tY] == 'f')
                {
                    if (isStart)
                    {
                        isStart = false;
                        if ((pX == tX && SY == 0) || (pY == tY && SX == 0))
                            field[CX, CY] = 'I';
                        else
                            field[CX, CY] = 'L';
                    }
                    else
                    {
                        if (pX == tX || pY == tY)
                            field[CX, CY] = 'I';
                        else
                            field[CX, CY] = 'L';
                    }
                    pX = CX; pY = CY;
                    CX = tX; CY = tY;
                }
            }
        }

        private void clearfield()
        {
            for (int i = 0; i < H; i++)
                for (int j = 0; j < W; j++)
                {
                    if (field[i, j] != 'F' && field[i, j] != 'f')
                        field[i, j] = '_';
                }
        }

    }
}
