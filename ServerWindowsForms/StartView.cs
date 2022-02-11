using Domain.Application;
using Domain.Matrices;
using Domain.MatrixMultiplication;
using Domain.MatrixMultiplicators;

namespace ServerWindowsForms
{
    public partial class StartView : Form
    {
        private Master? Master { get; set; }

        private Task<Matrix>? MatrixA = null;
        private Task<Matrix>? MatrixB = null;

        public StartView()
        {
            InitializeComponent();
        }

        private void bt_MatrixA_Click(object sender, EventArgs e)
        {
            var dialogResult = FileOpener.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                MatrixA = Task.Run(() => MatrixReader.Resolve(FileOpener.FileName));
                lb_MatrixA.Text = FileOpener.FileName.Split("\\")[^1];
            }
        }

        private void bt_MatrixB_Click(object sender, EventArgs e)
        {
            var dialogResult = FileOpener.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                MatrixB = Task.Run(() => MatrixReader.Resolve(FileOpener.FileName));
                lb_MatrixB.Text = FileOpener.FileName.Split("\\")[^1];
            }
        }

        private void bt_Start_Click(object sender, EventArgs e)
        {
            if (MatrixA is null || MatrixB is null)
            {
                MessageBox.Show($"Select both matrices files", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!MatrixA.IsCompleted || !MatrixB.IsCompleted)
            {
                MessageBox.Show("Loading matrices, wait a little!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var multiplicator = new DistributedMultiplicatorMaster(MatrixA.Result, MatrixB.Result);
                Master = new(multiplicator, tb_IP.Text, int.Parse(tb_Port.Text));
                Server.Instance!.OpenFormPanel(new RunningViewDistributed(Master));
            }
            catch (Exception)
            {
                MessageBox.Show($"Error trying to connect to {tb_IP.Text}:{tb_Port.Text}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bt_StartLocal_Click(object sender, EventArgs e)
        {
            if (MatrixA is null || MatrixB is null)
            {
                MessageBox.Show($"Select both matrices files", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!MatrixA.IsCompleted || !MatrixB.IsCompleted)
            {
                MessageBox.Show("Loading matrices, wait a little!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var localMultiplicator = new LocalMultiplicator(MatrixA.Result, MatrixB.Result);
            Server.Instance!.OpenFormPanel(new RunningViewLocal(localMultiplicator));
        }
    }
}