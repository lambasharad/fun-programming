using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "s" && textBox2.Text=="s")
            {
                Form1 ss = new Form1();
                ss.Show();
            }
                
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            string imageName = @"C:\Users\Sharad\Documents\Visual Studio 2015\Projects\WindowsFormsApplication1\WindowsFormsApplication1\images\Login\VIT-Logo.jpg";
            var image = Image.FromFile(imageName);

            pictureBox1.Image = image;

            string imageName2 = @"C:\Users\Sharad\Documents\Visual Studio 2015\Projects\WindowsFormsApplication1\WindowsFormsApplication1\images\Login\positive.png";
            var image2 = Image.FromFile(imageName2);

            pictureBox2.Image = image2;


            string imageName3 = @"C:\Users\Sharad\Documents\Visual Studio 2015\Projects\WindowsFormsApplication1\WindowsFormsApplication1\images\Login\negative.jpg";
            var image3 = Image.FromFile(imageName3);

            pictureBox3.Image = image3;
        }
    }
}
