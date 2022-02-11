using Domain.Application;

namespace ClientWindowsForms
{
    public partial class ConnectView : Form
    {
        public Slave? Slave { get; set; }
        public ConnectView()
        {
            InitializeComponent();
        }

        private void bt_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                Slave = new(tb_IP.Text, int.Parse(tb_Port.Text));
                Client.Instance!.OpenFormPanel(new ShowCalculationsView(Slave));
            }
            catch (Exception)
            {
                MessageBox.Show($"Error trying to connect to {tb_IP.Text}:{tb_Port.Text}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}