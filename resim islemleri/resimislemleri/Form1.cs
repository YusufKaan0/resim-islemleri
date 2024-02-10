using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace resimislemleri
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "resimler|*.BMP;*.JPG;*.GIF";
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            pictureBox1.BackgroundImage = Image.FromFile(openFileDialog1.FileName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap original = new Bitmap(pictureBox1.BackgroundImage);

            Bitmap output = new Bitmap(pictureBox1.BackgroundImage.Width, pictureBox1.BackgroundImage.Height);

            for (int i = 0; i < original.Width; i++)
            {

                for (int j = 0; j < original.Height; j++)
                {

                    Color c = original.GetPixel(i, j);

                    int average = ((c.R + c.B + c.G) / 3);

                    if (average < 200)
                        output.SetPixel(i, j, Color.Black);

                    else
                        output.SetPixel(i, j, Color.White);

                }
            }

            pictureBox1.BackgroundImage = output;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = ReSize(pictureBox1.BackgroundImage, new Size(300, 300));
        }

        public Image ReSize(Image resim, Size hedefBoyut)
        {
            Bitmap bmpResim = new Bitmap(hedefBoyut.Width, hedefBoyut.Height, PixelFormat.Format24bppRgb);
            bmpResim.SetResolution(resim.HorizontalResolution, resim.VerticalResolution);

            Graphics grResim = Graphics.FromImage(bmpResim);
            grResim.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            grResim.FillRectangle(Brushes.White, 0, 0, hedefBoyut.Width, hedefBoyut.Height);

            grResim.DrawImage(resim, new Rectangle(0, 0, hedefBoyut.Width, hedefBoyut.Height), new Rectangle(0, 0, resim.Width, resim.Height), GraphicsUnit.Pixel);
            return bmpResim;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = ReSize(pictureBox1.BackgroundImage, new Size(1280, 768));

        }
        Random rnd = new Random();
        private void button5_Click(object sender, EventArgs e)
        {
            Bitmap original = new Bitmap(pictureBox1.BackgroundImage);

            Bitmap output = new Bitmap(pictureBox1.BackgroundImage.Width, pictureBox1.BackgroundImage.Height);

            for (int i = 0; i < original.Width; i++)
            {

                for (int j = 0; j < original.Height; j++)
                {
                        output.SetPixel(i, j, Color.FromArgb(rnd.Next(0, 256), rnd.Next(200, 256), rnd.Next(200, 256)));
                }
            }

            pictureBox1.BackgroundImage = output;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                pictureBox1.BackgroundImage.Save(saveFileDialog1.FileName);
                MessageBox.Show("Resim Kaydedildi");
            }
            catch (Exception)
            {
                MessageBox.Show("Kaydederken hata oluştu");
            }
        }
    }
}
