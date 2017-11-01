using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cs480lab3
{
    public partial class Calculator : Form
    {
        public Calculator()
        {
            InitializeComponent();
        }

        //numberpad 0
        private void button1_Click(object sender, EventArgs e)
        {
            textBox_input.Text += "0";
        }

        //numberpad 1
        private void button2_Click(object sender, EventArgs e)
        {
            textBox_input.Text += "1";
        }

        //numberpad 2
        private void button3_Click(object sender, EventArgs e)
        {
            textBox_input.Text += "2";
        }

        //numberpad 3
        private void button4_Click(object sender, EventArgs e)
        {
            textBox_input.Text += "3";
        }

        //numberpad 4
        private void button5_Click(object sender, EventArgs e)
        {
            textBox_input.Text += "4";
        }

        //numberpad 5
        private void button6_Click(object sender, EventArgs e)
        {
            textBox_input.Text += "5";
        }

        //numberpad 6
        private void button7_Click(object sender, EventArgs e)
        {
            textBox_input.Text += "6";
        }

        //numberpad 7
        private void button8_Click(object sender, EventArgs e)
        {
            textBox_input.Text += "7";
        }

        //numberpad 8
        private void button9_Click(object sender, EventArgs e)
        {
            textBox_input.Text += "8";
        }

        //numberpad 9
        private void button10_Click(object sender, EventArgs e)
        {
            textBox_input.Text += "9";
        }

        //deciaml point
        private void button11_Click(object sender, EventArgs e)
        {
            textBox_input.Text += ".";
        }

        //addition operator
        private void button12_Click(object sender, EventArgs e)
        {
            textBox_input.Text += "+";
        }

        //subtraction operator
        private void button13_Click(object sender, EventArgs e)
        {
            textBox_input.Text += "-";
        }

        //division operator
        private void button14_Click(object sender, EventArgs e)
        {
            textBox_input.Text += "/";
        }

        //multiplication operator
        private void button15_Click(object sender, EventArgs e)
        {
            textBox_input.Text += "*";
        }

        //power operator
        private void button16_Click(object sender, EventArgs e)
        {
            textBox_input.Text += "^";
        }

        //enter button
        private void button17_Click(object sender, EventArgs e)
        {
            //create an instance of the rpn object
            Rpn rpn = new Rpn();
            try
            {
                //attempt to evaluate the expression
                double result = rpn.Evaluate(textBox_input.Text);
                textBox_output.Text = result.ToString();
                textBox_input.Text = "";


            }
            catch (Exception ex)
            {
                //print any errors to the output box
                textBox_input.Text = "";
                textBox_output.Text = ex.Message;
            }
        }

        //clear input button
        private void button_clearInput_Click(object sender, EventArgs e)
        {
            textBox_input.Text = "";
            textBox_output.Text = "";
        }

        //right parentheses button
        private void button19_Click(object sender, EventArgs e)
        {
            textBox_input.Text += "(";
        }

        //left parentheses button
        private void button20_Click(object sender, EventArgs e)
        {
            textBox_input.Text += ")";
        }
    }
}