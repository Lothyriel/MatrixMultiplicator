using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientWindowsForms
{
    public partial class Connect : Form
    {
        public Connect()
        {
            InitializeComponent();
        }

        private void tb_IP_TextChanged(object sender, EventArgs e)
        {
            bt_Connect.Enabled = tb_IP.Text != string.Empty;
        }

        private void bt_Connect_Click(object sender, EventArgs e)
        {
            if (!IPAddress.TryParse(tb_IP.Text, out IPAddress ipAddress))
            {
                MessageBox.Show("Invalid IP", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


        }
    }
}