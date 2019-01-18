using NonogramProject;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class NonogramGUI : Form
    {
        private NonogramSolver solver;
        private int row;
        private int column;
        private PictureBox[,] picBox;
        UpdatedEventArgs last;
        ManualResetEvent reset = new ManualResetEvent(false);

        public NonogramGUI(NonogramSolver solver)
        {
            InitializeComponent();
            this.solver = solver;

            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.Padding = new Padding(100);

            row = solver.GetRows();
            column = solver.GetColumns();
            picBox = new PictureBox[row, column];

            Console.WriteLine("column = " + column);
            Console.WriteLine("row = " + row);

            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.Padding = new Padding(0, 0, 0, 0);
            tableLayoutPanel1.Margin = new Padding(0, 0, 0, 0);
            tableLayoutPanel1.ColumnCount = column + 1;
            tableLayoutPanel1.RowCount = row + 1;
            string textClues;

            tableLayoutPanel1.Controls.Add(new Label());

            for (int i = 1; i <= column; i++)
            {
                textClues = "";
                foreach (Clue clue in solver.GetColumn(i).Clues)
                {
                    textClues += "\n" + clue.Value;
                }

                tableLayoutPanel1.Controls.Add(new Label { Text = textClues, AutoSize = true, Font = new Font("Arial", 18) });
            }

            for (int i = 0; i < row; i++)
            {
                textClues = "";
                foreach (Clue clue in solver.GetRow(i + 1).Clues)
                {
                    textClues += clue.Value + "  ";
                }
                tableLayoutPanel1.Controls.Add(new Label { Text = textClues, AutoSize = true, Font = new Font("Arial", 18) }); ;

                for (int j = 0; j < column; j++)
                {
                    Image image;
                    PictureBox tempBox;
                    if (row >= 12 || column >= 12)
                    {
                        Image imager = Properties.Resources.Blank;
                        image = new Bitmap(imager, new Size(25, 25));

                        tempBox = new PictureBox() { Image = image };
                    }
                    else
                        image = Properties.Resources.Blank;

                    tempBox = new PictureBox() { Image = image };
                    tempBox.Size = image.Size;
                    tableLayoutPanel1.Controls.Add(tempBox);

                    picBox[i, j] = tempBox;
                    solver.Updater += Updated;
                }
            }

            solver.Updater += Updated;
            this.Show();
            Application.DoEvents();
            Thread.Sleep(5000);
            solver.Run();
            if (solver.IsFinished())
                label1.Visible = true;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        public void Updated(object sender, UpdatedEventArgs e)
        {
            if (last != null)
            {
                foreach (Tuple<int, int, int> tuple in last.Changes)
                {
                    if (tuple != null)
                    {
                        if (tuple.Item3.Equals(1))
                            picBox[tuple.Item1 - 1, tuple.Item2 - 1].Image = Properties.Resources.Filled;
                        else if (tuple.Item3.Equals(2))
                            picBox[tuple.Item1 - 1, tuple.Item2 - 1].Image = Properties.Resources.Cross;
                    }
                }
            }

            //System.Timers.Timer timer = new System.Timers.Timer(1000);
            //timer.Elapsed += OnTimer;
            //timer.AutoReset = false;
            //timer.Start();

            foreach (Tuple<int, int, int> tuple in e.Changes)
            {
                if (tuple != null)
                {
                    if (tuple.Item3.Equals(1))
                        picBox[tuple.Item1 - 1, tuple.Item2 - 1].Image = Properties.Resources.New_Filled;
                    else if (tuple.Item3.Equals(2))
                        picBox[tuple.Item1 - 1, tuple.Item2 - 1].Image = Properties.Resources.New_Cross;
                }
            }

            last = e;
            Application.DoEvents();
            
        }

        private void OnTimer(Object sender, EventArgs e)
        {
            reset.Set();
            reset.Reset();
            Console.WriteLine("I made it");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
        }
    }
}