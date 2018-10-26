using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;

namespace MultipleThreading
{
    public partial class Form1 : Form
    {
        
        private UserAction userAction; 
        private int len;

        public Form1()
        {
            this.userAction = new UserAction();

            InitializeComponent();
            this.processorAmount.Text = Environment.ProcessorCount.ToString();
            this.processorAmount.Enabled = false;
            this.textBox5.Enabled = false;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "średnia")
            {
                this.userAction.setAction(UserAction.MEAN);

            }
            else if(comboBox1.SelectedItem.ToString() == "mediana")
            {
                this.userAction.setAction(UserAction.MEDIAN);
            }
            else
            {
                MessageBox.Show("Errors");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {               
        }

        private void length_TextChanged(object sender, EventArgs e)
        {
            this.len = Int32.Parse(length.Text);
        }

 

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.userAction.setInputThreads(Int32.Parse(textBox3.Text));
            } catch (Exception ex)
            {
            }

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if(Int32.Parse(textBox4.Text) >= this.len)
            {
                MessageBox.Show("Okno musi być mniejsze niż długość wektora");
                return;
            }
            this.userAction.setWindowLength(Int32.Parse(textBox4.Text));
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.userAction.setGenerated(this.len);

            this.userAction.makeAction();
  
            this.textBox5.Text = this.userAction.getElapsedTime();
        }
    }
}
