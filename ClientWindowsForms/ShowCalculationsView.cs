using Domain.Application;

namespace ClientWindowsForms
{
    public partial class ShowCalculationsView : Form
    {
        public ShowCalculationsView(Slave slave)
        {
            InitializeComponent();
            Slave = slave;
            slave.ExitHandler = ShowExit;
            Slave.Start();
            MessageBox.Show("Ready to receive multiplication requests!!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public Slave Slave { get; }

        public void ShowExit()
        {
            Invoke(Show);

            static void Show()
            {
                MessageBox.Show("The matrix multiplication finished, thank you!!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
