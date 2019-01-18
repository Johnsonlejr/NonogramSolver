using System;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int row;
            int column;

            if (Int32.TryParse(textBox1.Text, out row) && Int32.TryParse(textBox2.Text, out column) && !row.Equals(0) && !column.Equals(0))
            {
                Form1 form = new Form1(row, column, false);
                form.Show();
                this.Hide();
            }
            else
            {
                label5.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileStream f = new FileStream("save.txt", FileMode.Open);
            StreamReader reader = new StreamReader(f);
            int row, column;

            Int32.TryParse(reader.ReadLine(), out row);
            Int32.TryParse(reader.ReadLine(), out column);
            reader.Close();
            f.Close();
            Form1 form = new Form1(row, column, true);
            form.Show();
            this.Hide();
        }

        private void Start_Load(object sender, EventArgs e)
        {
        }
    }
}