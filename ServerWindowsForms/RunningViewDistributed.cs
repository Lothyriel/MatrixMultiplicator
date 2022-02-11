using Domain.Application;
using Domain.Exceptions;

namespace ServerWindowsForms
{
    public partial class RunningViewDistributed : Form
    {
        public RunningViewDistributed(Master master)
        {
            InitializeComponent();
            Master = master;
            Master.Start(SetPixel);

            var multiplicator = master.Multiplicator;
            BitMap = Extensions.InitMatrixBitMap(multiplicator.MatrixA.X, multiplicator.MatrixB.Y);
            pb_Bitmap.Image = BitMap;
        }

        private void SetPixel(int x, int y)
        {
            Invoke(() => Set(x, y));

            void Set(int x, int y)
            {
                BitMap.SetPixel(x, y, Color.Green);
            }
        }

        public Master Master { get; }
        public Bitmap BitMap { get; }

        private void bt_Start_Click(object sender, EventArgs e)
        {
            try
            {
                Master.StartSendingRequests();
            }
            catch (NoClientsConnected ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}