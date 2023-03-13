using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabalhoContaBancaria
{
    public partial class Login : Form
    {
        int X = 0;
        int Y = 0;
        public Login()
        {
            InitializeComponent();
            //Para Mover o Form sem ter borda
            this.MouseDown += new MouseEventHandler(Login_MouseDown);
            this.MouseMove += new MouseEventHandler(Login_MouseMove);
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Codigo para conseguir mover o form mesmo não tendo borda
        /// </summary>
        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            X = this.Left - MousePosition.X;
            Y = this.Top - MousePosition.Y;

        }

        private void Login_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            this.Left = X + MousePosition.X;
            this.Top = Y + MousePosition.Y;
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.Font = new Font(label1.Font, FontStyle.Underline);
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.Font = new Font(label1.Font, FontStyle.Regular);
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
