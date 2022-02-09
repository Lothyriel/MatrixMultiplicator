using Domain.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientWindowsForms
{
    public partial class ShowCalculation : Form
    {
        public Slave Slave { get; }
        public ShowCalculation(string ip , int port)
        {
            InitializeComponent();
            Slave = new(ip, port);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Slave.SendConnectionAttempt();
            MessageBox.Show("Trying to connect to the server", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
