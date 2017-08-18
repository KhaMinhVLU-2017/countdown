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
        int sum = 0;
        private void btn_Start_Click(object sender, EventArgs e)
        {

            if (txt_Hour.Text == "")
            {
                h = 0;
                txt_Hour.Text = h.ToString().Trim();
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
                txt_Minute.Text = m.ToString().Trim();
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
                    m = m % 60;                }
                count = h.ToString().Count();
                countMinute = m.ToString().Count();
                sum = h * 60 * 60 + m * 60;
                progressBar1.Maximum = h*60*60+m*60;
                timer1.Start();
   
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
        
            btn_Pause.Enabled = false;
            btn_Stop.Enabled = false;
            checkPause = false;
            btn_Pause.Text = "Pause";
            progressBar1.Value=0;
        }
        int sizeLot = 0;
        private void button2_Click(object sender, EventArgs e)//zomm oiut
        {
       
          
        }

        private void txt_Minute_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //StartFrom meo = new StartFrom();// chua toi uu close
            //meo.CloseMeo();
            // Process process = Process.Start("CountDown.exe");
         
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
           

            string hh = Convert.ToString(h);
            string mm = Convert.ToString(m);
            string ss = Convert.ToString(s);
            progressBar1.Increment(1);
            txt_screen.Text = string.Format("{0}:{1}:{2}", hh, mm, ss);

            if (progressBar1.Value <= sum / 3)
            {
                progressBar1.ForeColor = Color.Green;
            }

            if (progressBar1.Value <= sum*2/3&& progressBar1.Value>=sum/3)
            {
                progressBar1.ForeColor = Color.Blue;
            }
            if (progressBar1.Value <=sum && progressBar1.Value>=sum*2/3)
            {
                progressBar1.ForeColor = Color.Red;
            }
         

        }
        private void enableMeo(bool meo)
        {
            btn_Start.Enabled = meo;
            txt_Hour.Enabled = meo;
            txt_Minute.Enabled = meo;
        }
      
    }
}
