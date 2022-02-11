namespace ServerWindowsForms
{
    public static class Extensions
    {
        public static Bitmap InitMatrixBitMap(int x, int y)
        {
            var bitmap = new Bitmap(x, y);
            var graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(Brushes.Red, 0, 0, x, y);
            return bitmap;
        }
    }
}