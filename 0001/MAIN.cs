using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace _0001
{
    public partial class MAIN : Form
    {
        Bitmap bmp;
        Canvas canvas;
        FileInfo info;

        public MAIN()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            canvas = new Canvas(PCT_CANVAS);
        }

        private void BTN_EXE_Click(object sender, EventArgs e)
        {               
            using (var previewDialog = FormPreviewDialog.Dialog)
            {               
                previewDialog.ShowDialog();
                if (previewDialog.Result== DialogResult.OK)
                {
                    // Carga y muestra la imagen seleccionada en tu aplicación principal
                    info                = new FileInfo(previewDialog.SelectedImagePath);
                    canvas.Bmp          = new Bitmap(previewDialog.SelectedImagePath);
                    bmp                 = new Bitmap(previewDialog.SelectedImagePath);
                    PCT_THUMBNAIL.Image = bmp;
                }
            }//*/
        }

        private void BTN_INVERT_Click(object sender, EventArgs e)
        {
            canvas.Bits = BitProcess.Invert(canvas.Bits);
        }

        private void BTN_SEPIA_Click(object sender, EventArgs e)
        {
            canvas.Bits = BitProcess.Sepia(canvas.Bits);
        }

        private void BTN_GRAY_Click(object sender, EventArgs e)
        {
            canvas.Bits = BitProcess.Gray(canvas.Bits);
        }

        private void BTN_RELOAD_Click(object sender, EventArgs e)
        {
            canvas.Bmp = new Bitmap(bmp);
        }

        private void BTN_SAVE_Click(object sender, EventArgs e)
        {
            canvas.Bmp.Save(@info.Name, ImageFormat.Png);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics gr;
            int S;
            Random rnd;
            Color colorAleatorio;
            int r, g, b;
            Brush brush;

            S       = 200;
            bmp     = new Bitmap(S, S);
            gr      = Graphics.FromImage(bmp);
            rnd     = new Random();
            info    = new FileInfo("SQUARES.PNG");

            for (int x = 0; x < S; x++)
            {
                for (int y = 0; y < S; y++)
                {
                    r = rnd.Next(256);
                    g = rnd.Next(256);
                    b = rnd.Next(256);
                    colorAleatorio = Color.FromArgb(255, r, g, b);
                    brush = new SolidBrush(colorAleatorio);

                    gr.FillRectangle(brush, x * 10, y * 10, 10, 10);
                    gr.DrawRectangle(Pens.Gray, x * 10, y * 10, 10, 10);
                }
            }
            gr.DrawRectangle(Pens.Gray, 0, 0, S-1, S-1);
            
            canvas.Bmp = bmp;
            PCT_THUMBNAIL.Image = canvas.Bmp;
        }
    }
}
