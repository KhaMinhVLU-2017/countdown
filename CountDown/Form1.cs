using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CountDown
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_MEO);
            timer1.Interval = 1000;// 1 giay ak
            btn_Pause.Enabled = false;
            btn_Stop.Enabled = false;
            button1.Enabled = true ;
            button2.Enabled = false;
        }
        int h, m, s;
        Timer timer1;
        private void txt_Hour_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_Minute_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        int count = 0;
        int countMinute = 0;
        int sizeCount = 0;
        float currentSize;
        int xMinute = 0;
        int xHour = 0;
        int xLabel5=0;
        int xLabel3=0;
        int xSecond = 0;
        int fMinute = 0;
        int fHour = 0;
        int fSecond = 0;
        int fLabel5 = 0;
        int fLabel3 = 0;
        private void button1_Click(object sender, EventArgs e)//zoom in
        {
            sizeCount++;
            if (sizeLot != 0)
            {
                sizeLot--;
            }
            // minute
            xMinute = 230 - sizeCount * 4;
            xHour = 145 - sizeCount * 25 - (count - 1) * 20;
            xSecond = 315 + sizeCount * 10;
            xLabel3 = 278 + sizeCount * 10;
            xLabel5 = 193 - sizeCount * 10 + countMinute * 5;
            fMinute = 20 + sizeCount * 10;
            fHour = 20 + sizeCount * 10;
            fSecond = 20 + sizeCount * 10;
            fLabel5 = 20 + sizeCount * 10;
            fLabel3 = 20 + sizeCount * 10;
            if (sizeCount <=4)
            {
                checkZoom = true;
                lbl_minute.Font = new Font("Arial", fMinute);
                this.lbl_minute.Location = new System.Drawing.Point(xMinute, 144);//center
                                                                                              //hour
                lbl_hour.Font = new Font("Arial", fHour);
                this.lbl_hour.Location = new System.Drawing.Point(xHour, 144);//left

                //second
                lbl_Second.Font = new Font("Arial", fSecond);
                this.lbl_Second.Location = new System.Drawing.Point(xSecond, 144);//right
                //label5
                this.label5.Location = new System.Drawing.Point(xLabel5, 144);//left
                label5.Font = new Font("Arial", fLabel5);
                //label3
                label3.Font = new Font("Arial", fLabel3);
                this.label3.Location = new System.Drawing.Point(xLabel3, 144);//phai
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = true;
            }
        }
        string screenh = "";

        private void txt_Hour_KeyDown(object sender, KeyEventArgs e)
        {
            screenh = txt_Hour.Text;
            if (e.KeyCode == Keys.Enter)
            {
                btn_Start_Click(null, null);// phim enter
                txt_Hour.Text = screenh;
            }
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {

            if (txt_Hour.Text == "")
            {
                h = 0;
                txt_Hour.Text = h.ToString();
            }
            else
            {
                try
                {
                    h = int.Parse(txt_Hour.Text);
                }
                catch
                {

                }
            }
            if (txt_Minute.Text == "")
            {
                m = 0;
                txt_Minute.Text = m.ToString();
            }
            else
            {
                try
                {
                    m = int.Parse(txt_Minute.Text);
                }
                catch
                {

                }
            }
            if (h == 0 && m == 0)
            {
                MessageBox.Show("Pease input number in Text");
            }
            else
            {
                if (m > 60)
                {
                    h = m / 60 + h;
                    m = m % 60;
                }
                count = h.ToString().Count();
                countMinute = m.ToString().Count();
                if (checkZoom == false)
                {
                    lbl_hour.Location = new System.Drawing.Point(145 - (count - 1) * 30, 144);
                    this.lbl_minute.Location = new System.Drawing.Point(230 - (countMinute - 1) * 12, 144);
                }
                progressBar1.Maximum = h*60*60+m*60;
                timer1.Start();
                lbl_hour.Text = h.ToString();
                lbl_minute.Text = m.ToString();
                enableMeo(false);
                btn_Pause.Enabled = true;
                btn_Stop.Enabled = true;
            }

        }
        bool checkPause = false;
        bool checkZoom = false;
        private void btn_Pause_Click(object sender, EventArgs e)
        {
            if (checkPause == false)
            {
                timer1.Stop();
                checkPause = true;
                btn_Pause.Text = "Resume";
             
            }
            else
            {
                btn_Pause.Text = "Pause";
                timer1.Start();
                checkPause = false;
            }
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_MEO);
            timer1.Interval = 1000;// 1 giay ak
            enableMeo(true);
            h = 0;m = 0;s = 0;
            lbl_hour.Text = "0";
            lbl_minute.Text = "0";
            lbl_Second.Text = "0";
            btn_Pause.Enabled = false;
            btn_Stop.Enabled = false;
            checkPause = false;
            btn_Pause.Text = "Pause";
            progressBar1.Value=0;
        }
        int sizeLot = 0;
        private void button2_Click(object sender, EventArgs e)//zomm oiut
        {
       
            sizeLot++;// dem de xoa
            sizeCount--;
            // minute
            if (sizeLot <=4)
            {
                checkZoom = true;
                lbl_minute.Font = new Font("Arial", fMinute - sizeLot * 10);
                this.lbl_minute.Location = new System.Drawing.Point(xMinute + sizeLot * 4, 144);//center
                //hour
                 lbl_hour.Font = new Font("Arial", fHour - sizeLot * 10);
                 this.lbl_hour.Location = new System.Drawing.Point(xHour + sizeLot * 30 + (count - 2) * 35, 144);//left
              
                //second
                lbl_Second.Font = new Font("Arial", fSecond - sizeLot * 10);
                this.lbl_Second.Location = new System.Drawing.Point(xSecond - sizeLot * 10, 144);//right
                  //label5
                this.label5.Location = new System.Drawing.Point(xLabel5 + sizeLot * 10 + countMinute , 144);//left
                label5.Font = new Font("Arial", fLabel5 - sizeLot * 10);
                //label3
                label3.Font = new Font("Arial", fLabel3 - sizeLot * 10);
                this.label3.Location = new System.Drawing.Point(xLabel3 - sizeLot * 10, 144);//phai
            }
            //xMinute = 230 - sizeCount * 4;
            //xHour = 145 - sizeCount * 25 - (count - 1) * 20;
            //xSecond = 315 + sizeCount * 10;
            //xLabel3 = 278 + sizeCount * 10;
            //xLabel5 = 193 - sizeCount * 10 + countMinute * 5;
            //fMinute = 20 + sizeCount * 10;
            //fHour = 20 + sizeCount * 10;
            //fSecond = 20 + sizeCount * 10;
            //fLabel5 = 20 + sizeCount * 10;
            //fLabel3 = 20 + sizeCount * 10;
            else
            {
                button2.Enabled = false;
                button1.Enabled = true;
            }
        }

        private void txt_Minute_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

        private void timer1_MEO(object sender, EventArgs e)
        {
            s--;
            if (s == -1)
            {
                m--;
                s = 59;

            }
            if (m == -1)
            {
                h--;
                m = 59;
            }
            if (h == 0 && m == 0 && s == 0)
            {
                timer1.Stop();
            }
            if (h == 0 && m == 59)
            {
                this.lbl_minute.Location = new System.Drawing.Point(230 - 10, 144);
            }

            string hh = Convert.ToString(h);
            string mm = Convert.ToString(m);
            string ss = Convert.ToString(s);
            lbl_hour.Text = hh;
            lbl_minute.Text = mm;
            lbl_Second.Text = ss;
            progressBar1.Increment(1);
           
        }
        private void enableMeo(bool meo)
        {
            btn_Start.Enabled = meo;
            txt_Hour.Enabled = meo;
            txt_Minute.Enabled = meo;
        }
      
    }
}
