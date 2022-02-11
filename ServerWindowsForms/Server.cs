namespace ServerWindowsForms
{
    public partial class Server : Form
    {
        public static Server? Instance { get; private set; }
        public Server()
        {
            Instance = this;
            InitializeComponent();
            OpenFormPanel(new StartView());
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