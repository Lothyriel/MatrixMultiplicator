namespace ClientWindowsForms
{
    public partial class Client : Form
    {
        public static Client? Instance { get; private set; }
        public Client()
        {
            Instance = this;
            InitializeComponent();
            OpenFormPanel(new ConnectView());
        }
        public void OpenFormPanel(Form panelForm)
        {
            panelForm.TopLevel = false;
            panelForm.FormBorderStyle = FormBorderStyle.None;
            panelForm.Dock = DockStyle.Fill;

            Panel.Controls.Clear();
            Panel.Controls.Add(panelForm);

            panelForm.BringToFront();
            panelForm.Show();
        }
    }
}