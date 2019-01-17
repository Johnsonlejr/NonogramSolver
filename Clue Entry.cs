using NonogramProject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private int row;
        private int column;
        private bool loaded;

        private List<TextBox> rowbox;
        private List<TextBox> columnbox;

        public Form1(int row, int column, bool loaded)
        {
            this.row = row;
            this.column = column;
            this.loaded = loaded;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String rowString = "";
            String columnString = "";
            string[] temp;
            label2.Visible = false;
            label3.Visible = false;

            foreach (TextBox box in rowbox)
            {
                temp = box.Text.Split(' ');
                int num = 0;
                int total = 0;

                foreach (string c in temp)
                {
                    if (!Int32.TryParse(c, out num))
                    {
                        label2.Visible = true;
                    }
                    else
                    {
                        Int32.TryParse(c, out num);
                        total += num;
                    }
                }

                total += temp.Length - 1;
                if (total > column)
                {
                    label3.Visible = true;
                }
            }

            foreach (TextBox box in columnbox)
            {
                temp = box.Text.Split(' ');
                int num = 0;
                int total = 0;

                foreach (string c in temp)
                {
                    if (!Int32.TryParse(c, out num))
                    {
                        label2.Visible = true;
                    }
                    else
                    {
                        Int32.TryParse(c, out num);
                        total += num;
                    }
                }

                total += temp.Length - 1;
                Console.WriteLine(total);
                if (total > row)
                    label3.Visible = true;
            }

            if (!label2.Visible && !label3.Visible)
            {
                foreach (TextBox box in rowbox)
                {
                    rowString += box.Text + "; ";
                }

                foreach (TextBox box in columnbox)
                {
                    columnString += box.Text + "; ";
                }

                save();

                NonogramSolver solver = new NonogramSolver(rowString, columnString);
                Console.WriteLine(rowString);
                this.Hide();
                NonogramGUI form = new NonogramGUI(solver);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            rowbox = new List<TextBox>();
            columnbox = new List<TextBox>();
            int i;

            for (i = 0; i < row; i++)
            {
                tableLayoutPanel1.AutoSize = true;
                tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Row " + (i + 1) });
                rowbox.Add(new TextBox());
                tableLayoutPanel1.Controls.Add(rowbox[i]);
            }

            for (i = 0; i < column; i++)
            {
                tableLayoutPanel2.AutoSize = true;
                tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                tableLayoutPanel2.Controls.Add(new Label() { Text = "Column " + (i + 1) });
                columnbox.Add(new TextBox());
                tableLayoutPanel2.Controls.Add(columnbox[i]);
            }

            if (loaded)
                load();
        }

        public void save()
        {
            FileStream f = new FileStream("C:\\Users\\lajjr_000\\Documents\\Visual Studio 2015\\Projects\\Nonogram GUI\\Start\\save.txt", FileMode.Create);
            StreamWriter stream = new StreamWriter(f);

            stream.WriteLine(row);
            stream.WriteLine(column);

            foreach (TextBox box in rowbox)
            {
                stream.WriteLine(box.Text);
            }

            stream.WriteLine();

            foreach (TextBox box in columnbox)
            {
                stream.WriteLine(box.Text);
            }
            stream.Flush();
            stream.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            load();
        }

        private void load()
        {
            FileStream f = new FileStream("C:\\Users\\lajjr_000\\Documents\\Visual Studio 2015\\Projects\\Nonogram GUI\\Start\\save.txt", FileMode.Open);
            StreamReader reader = new StreamReader(f);

            int count = 0;
            String temp;

            reader.ReadLine();
            reader.ReadLine();

            while ((temp = reader.ReadLine()) != "")
            {
                rowbox[count].Text = temp;
                count++;
            }

            count = 0;

            while ((temp = reader.ReadLine()) != null)
            {
                columnbox[count].Text = temp;
                count++;
            }

            reader.Close();
        }
    }
}