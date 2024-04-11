using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

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
            info    = new FileInfo("BODOQUE.JPG");

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

            // Crear un Bitmap para el histograma
            Bitmap imagen = canvas.Bmp;

            // Crear arreglos para almacenar las frecuencias de los valores de píxeles de cada canal de color
            int[] frecuenciasRojo = new int[256];
            int[] frecuenciasVerde = new int[256];
            int[] frecuenciasAzul = new int[256];

            // Recorrer los píxeles de la imagen y actualizar las frecuencias
            for (int y = 0; y < imagen.Height; y++)
            {
                for (int x = 0; x < imagen.Width; x++)
                {
                    Color color = imagen.GetPixel(x, y);
                    frecuenciasRojo[color.R]++;
                    frecuenciasVerde[color.G]++;
                    frecuenciasAzul[color.B]++;
                }
            }

            // Crear un Bitmap para el histograma
            int histogramaAncho = 256;
            int histogramaAlto = PCT_Histogram.Height;
            Bitmap histograma = new Bitmap(histogramaAncho, histogramaAlto);

            // Dibujar el histograma en el Bitmap
            using (Graphics R = Graphics.FromImage(histograma))
            {
                R.Clear(Color.White);

                int maxFrecuencia = Math.Max(frecuenciasRojo.Max(), Math.Max(frecuenciasVerde.Max(), frecuenciasAzul.Max()));
                float factorEscala = (histogramaAlto - 10) / (float)maxFrecuencia;

                // Dibujar el histograma del canal rojo
                for (int i = 0; i < 256; i++)
                {
                    int altura = (int)(frecuenciasRojo[i] * factorEscala);
                    for (int y = histogramaAlto - altura; y < histogramaAlto; y++)
                    {
                        histograma.SetPixel(i, y, Color.Red);
                    }
                }

                // Dibujar el histograma del canal verde
                for (int i = 0; i < 256; i++)
                {
                    int altura = (int)(frecuenciasVerde[i] * factorEscala);
                    for (int y = histogramaAlto - altura; y < histogramaAlto; y++)
                    {
                        histograma.SetPixel(i, y, Color.Green);
                    }
                }

                // Dibujar el histograma del canal azul
                for (int i = 0; i < 256; i++)
                {
                    int altura = (int)(frecuenciasAzul[i] * factorEscala);
                    for (int y = histogramaAlto - altura; y < histogramaAlto; y++)
                    {
                        histograma.SetPixel(i, y, Color.Blue);
                    }
                }
            }

            // Mostrar el histograma en el PictureBox
            PCT_Histogram.Image = histograma;
        }
    }
}
