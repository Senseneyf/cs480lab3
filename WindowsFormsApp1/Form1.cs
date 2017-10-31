using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //numberpad 0
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += "0";
        }

        //numberpad 1
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += "1";
        }

        //numberpad 2
        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text += "2";
        }

        //numberpad 3
        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text += "3";
        }

        //numberpad 4
        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text += "4";
        }

        //numberpad 5
        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text += "5";
        }

        //numberpad 6
        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text += "6";
        }

        //numberpad 7
        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += "7";
        }

        //numberpad 8
        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += "8";
        }

        //numberpad 9
        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text += "9";
        }

        //deciaml point
        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text += ".";
        }

        //addition operator
        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Text += "+";
        }

        //subtraction operator
        private void button13_Click(object sender, EventArgs e)
        {
            textBox1.Text += "-";
        }

        //division operator
        private void button14_Click(object sender, EventArgs e)
        {
            textBox1.Text += "/";
        }

        //multiplication operator
        private void button15_Click(object sender, EventArgs e)
        {
            textBox1.Text += "*";
        }

        //power operator
        private void button16_Click(object sender, EventArgs e)
        {
            textBox1.Text += "^";
        }

        //enter button
        private void button17_Click(object sender, EventArgs e)
        {

        }

        //clear input button
        private void button18_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        //right parentheses button
        private void button19_Click(object sender, EventArgs e)
        {
            textBox1.Text += "(";
        }

        //left parentheses button
        private void button20_Click(object sender, EventArgs e)
        {
            textBox1.Text += ")";
        }
    }
}
