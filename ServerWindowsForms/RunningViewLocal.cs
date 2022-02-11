using Domain.MatrixMultiplication;

namespace ServerWindowsForms
{
    public partial class RunningViewLocal : Form
    {
        public RunningViewLocal(LocalMultiplicator multiplicator)
        {
            InitializeComponent();

            BitMap = Extensions.InitMatrixBitMap(multiplicator.MatrixA.X + 1, multiplicator.MatrixB.Y + 1);
            pb_Bitmap.Image = BitMap;
            Multiplicator = multiplicator;
            multiplicator.ExitHandler = ExitHandler;
            multiplicator.ResultHandler = SetPixel;
        }

        public Bitmap BitMap { get; }
        public LocalMultiplicator Multiplicator { get; }

        private void SetPixel(int x, int y)
        {
            Invoke(() => Set(x, y));

            void Set(int x, int y)
            {
                BitMap.SetPixel(x, y, Color.Green);
            }
        }

        private void ExitHandler(TimeSpan elapsed)
        {
            Invoke(() => MessageBox.Show($"Multiplication finished, it took {elapsed:mm\\:ss} minutes"));
            var dialogResult = MessageBox.Show("Would you like to save the result file?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Task.Run(() => Multiplicator.Result!.SaveFile());
            }
            Invoke(() => Server.Instance!.OpenFormPanel(new StartView()));
        }

        private void bt_Start_Click(object sender, EventArgs e)
        {
            if (timer_Redraw.Enabled)
            {
                MessageBox.Show("Already running", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Action func = cb_MultiThreaded.Checked ? Multiplicator.MultiplyMultiThreaded : Multiplicator.MultiplySingleThreaded;
            Task.Run(() => func());
            timer_Redraw.Start();
        }

        private void timer_Redraw_Tick(object sender, EventArgs e)
        {
            pb_Bitmap.Refresh();
        }
    }
}