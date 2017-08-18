using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace CountDown
{
    public partial class StartFrom : Form
    {
        public StartFrom()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar2.Width += 15;
            if (progressBar2.Width >= 463)
            {
                timer1.Stop();
                Form1 meo = new Form1();
                meo.Show();
                this.Hide();
            }
        }
     
    }
}
