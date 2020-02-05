using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Plumber
{
    public partial class Form1 : Form
    {
        public static char[] DIRECTS = new char[] { 'N', 'E', 'S', 'W' };

        public Form1()
        {
            InitializeComponent();
        }

        public TileGrid tileGrid;
        public TableLayoutPanel fieldGridLayout;
        public static Random rnd = new Random();
        public int H = 10;
        public int W = 10;
        PictureBox startTile;

        private void Form1_Load(object sender, EventArgs e)
        {
            tileGrid = new TileGrid(W, H);
            int tabCount = 100;
            fieldGridLayout = new TableLayoutPanel();
            fieldGridLayout.SuspendLayout();

            fieldGridLayout.ColumnCount = W + 1;
            fieldGridLayout.Location = new System.Drawing.Point(12, 12);
            fieldGridLayout.Name = "fildGridLayout";
            fieldGridLayout.RowCount = H + 1;
            fieldGridLayout.Size = new System.Drawing.Size((W + 1) * 30, (H + 1) * 30);
            fieldGridLayout.Margin = new Padding(0);
            fieldGridLayout.TabIndex = 99;
            fieldGridLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            fieldGridLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            for (int i = 0; i < H; i++)
            {
                fieldGridLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
                for (int j = 0; j < W; j++)
                {
                    fieldGridLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
                    ((System.ComponentModel.ISupportInitialize)(tileGrid.tileArr[i, j])).BeginInit();
                    tileGrid.tileArr[i, j].Location = new Point(0, 0);
                    tileGrid.tileArr[i, j].Name = "tilePicBox-" + i + "-" + j;
                    tileGrid.tileArr[i, j].Size = new Size(30, 30);
                    tileGrid.tileArr[i, j].TabIndex = tabCount;
                    tileGrid.tileArr[i, j].TabStop = false;
                    tileGrid.tileArr[i, j].Margin = new Padding(0);
                    
                    tileGrid.tileArr[i, j].MouseUp += new MouseEventHandler(this.tilePicBox_Click);
                    tabCount++;
                    fieldGridLayout.Controls.Add(tileGrid.tileArr[i, j], i + 1, j + 1);
                    ((System.ComponentModel.ISupportInitialize)(tileGrid.tileArr[i, j])).EndInit();
                }
            }
            startTile = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)(startTile)).BeginInit();
            startTile.Location = new Point(0, 0);
            startTile.Name = "tileStartBox";
            startTile.Size = new Size(30, 30);
            startTile.TabIndex = tabCount;
            startTile.TabStop = false;
            startTile.Margin = new Padding(0);
            if (tileGrid.SX == 0)
            {
                startTile.Image = Properties.Resources.tube_start_X0;
                fieldGridLayout.Controls.Add(startTile, tileGrid.SX, tileGrid.SY + 1);
            }
            else
            {
                startTile.Image = Properties.Resources.tube_start_Y0;
                fieldGridLayout.Controls.Add(startTile, tileGrid.SX + 1, tileGrid.SY);
            }
            
            ((System.ComponentModel.ISupportInitialize)(startTile)).EndInit();

            this.Controls.Add(fieldGridLayout);
            fieldGridLayout.ResumeLayout(false);
            
            tileGrid.checkStartTile();
        }

        public void newGame()
        {
            fieldGridLayout.Controls.Remove(startTile);
            tileGrid.reset();
            if (tileGrid.SX == 0)
            {
                startTile.Image = Properties.Resources.tube_start_X0;
                fieldGridLayout.Controls.Add(startTile, tileGrid.SX, tileGrid.SY + 1);
            }
            else
            {
                startTile.Image = Properties.Resources.tube_start_Y0;
                fieldGridLayout.Controls.Add(startTile, tileGrid.SX + 1, tileGrid.SY);
            }
            tileGrid.checkStartTile();
        }

        private void tilePicBox_Click(object sender, MouseEventArgs e)
        {
            MouseButtons btn = e.Button;
            
            if (btn == MouseButtons.Left)
            {
                if (!tileGrid.victory)
                {
                    string[] s = ((Tile)sender).Name.Split('-');
                    int x = Convert.ToInt32(s[1]);
                    int y = Convert.ToInt32(s[2]);
                    ((Tile)sender).addRotation();
                    tileGrid.checkAdjacentTiles(x, y);
                    if (tileGrid.victory) MessageBox.Show("VICTORY!");
                }
            }
        }

        private void newGameBtn_MouseUp(object sender, MouseEventArgs e)
        {
            if ((Control)sender == GetChildAtPoint(PointToClient(Cursor.Position)))
            {
                newGame();
                newGameBtn.Image = Properties.Resources.button_new_game;
            }
        }

        private void newGameBtn_MouseEnter(object sender, EventArgs e)
        {
            newGameBtn.Image = Properties.Resources.button_new_game_hl;
        }

        private void newGameBtn_MouseLeave(object sender, EventArgs e)
        {
            newGameBtn.Image = Properties.Resources.button_new_game;
        }

        private void newGameBtn_MouseDown(object sender, MouseEventArgs e)
        {
            newGameBtn.Image = Properties.Resources.button_new_game_down;
        }

        private void exitBtn_MouseUp(object sender, MouseEventArgs e)
        {
            if ((Control)sender == GetChildAtPoint(PointToClient(Cursor.Position)))
            {
                exitBtn.Image = Properties.Resources.button_exit;
                this.Close();
            }
        }

        private void exitBtn_MouseEnter(object sender, EventArgs e)
        {
            exitBtn.Image = Properties.Resources.button_exit_hl;
        }

        private void exitBtn_MouseLeave(object sender, EventArgs e)
        {
            exitBtn.Image = Properties.Resources.button_exit;
        }

        private void exitBtn_MouseDown(object sender, MouseEventArgs e)
        {
            exitBtn.Image = Properties.Resources.button_exit_down;
        }
    }
}
