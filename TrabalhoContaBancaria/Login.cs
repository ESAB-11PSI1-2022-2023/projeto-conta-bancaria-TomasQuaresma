// Copyright(c) Tomás Quaresma. All rights reserved.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabalhoContaBancaria
{
    public partial class Login : Form
    {
        int X = 0;
        int Y = 0;
        string Email { get; set; }
        string Password { get; set; }

        //Ativar Arredondar Cantos
        public enum DWMWINDOWATTRIBUTE
        {
            DWMWA_WINDOW_CORNER_PREFERENCE = 33
        }
        public enum DWM_WINDOW_CORNER_PREFERENCE
        {
            DWMWCP_DEFAULT = 0,
            DWMWCP_DONOTROUND = 1,
            DWMWCP_ROUND = 2,
            DWMWCP_ROUNDSMALL = 3
        }
        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        internal static extern void DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE attribute, ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute, uint cbAttribute);

        public Login()
        {
            InitializeComponent();
            //Para Mover o Form sem ter borda
            this.MouseDown += new MouseEventHandler(Login_MouseDown);
            this.MouseMove += new MouseEventHandler(Login_MouseMove);

            //Ativar Arredondar Cantos
            var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
            DwmSetWindowAttribute(this.Handle, attribute, ref preference, sizeof(uint));
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
            EsquecerMail.Font = new Font(EsquecerMail.Font, FontStyle.Underline);
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            EsquecerMail.Font = new Font(EsquecerMail.Font, FontStyle.Regular);
        }

        private void Botao1_Click(object sender, EventArgs e)
        {
            switch(Botao1.Text)
            {
                case "Seguinte":
                    char[]mail = TextBox1.Text.ToCharArray();
                    if (mail.Length <= 26)
                    {
                        Mail.Text = TextBox1.Text;
                    }
                    else
                    {
                        for(int i = 0; i <= 23; i++)
                        {
                            Mail.Text += mail[i];
                        }
                        Mail.Text += "...";
                    }

                    AlterarMail.Visible= true;
                    Mostrar.Visible= true;
                    Email = TextBox1.Text;
                    TextBox1.Text = null;
                    TextBox1.PasswordChar = '●';
                    Botao1.Text = "Iniciar sessão";
                    label3.Text = "Palavra-passe";
                    
                    break;
                case "Iniciar sessão":
                    Password = TextBox1.Text;
                    try
                    {
                        string[] linhas = File.ReadAllLines(@"Login.txt");
                        foreach (string linha in linhas)
                        {
                            string[] valores = linha.Split(':');
                            if (valores[0].Equals(Email) && valores[1].Equals(Password))
                            {
                                Inicio form = new Inicio();
                                form.AtualizarOpcoes(Email);
                                form.Show();
                                this.Visible = false;

                                Mail.Text = null;
                                AlterarMail.Visible = false;
                                Mostrar.Visible = false;
                                TextBox1.Text = null;
                                TextBox1.PasswordChar = '\0';
                                Botao1.Text = "Seguinte";
                                label3.Text = "Endereço de e-mail ou número de telemóvel";

                                return;
                            }
                        }
                        MessageBox.Show("Usuário ou senha inválidos!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao ler o arquivo: " + ex.Message);
                    }
                    break;
            }
        }

        private void AlterarMail_MouseEnter(object sender, EventArgs e)
        {
            AlterarMail.Font = new Font(EsquecerMail.Font, FontStyle.Underline);
        }

        private void AlterarMail_MouseLeave(object sender, EventArgs e)
        {
            AlterarMail.Font = new Font(EsquecerMail.Font, FontStyle.Regular);
        }

        private void AlterarMail_Click(object sender, EventArgs e)
        {
            Mail.Text = null;
            AlterarMail.Visible = false;
            Mostrar.Visible = false;
            TextBox1.Text = null;
            TextBox1.PasswordChar = '\0';
            Botao1.Text = "Seguinte";
            label3.Text = "Endereço de e-mail ou número de telemóvel";
            
        }

        private void Mostrar_Click(object sender, EventArgs e)
        {
            switch (Mostrar.Text)
            {
                case "Mostrar":
                    Mostrar.Text = "Ocultar";
                    TextBox1.PasswordChar = '\0';
                    break;
                case "Ocultar":
                    Mostrar.Text = "Mostrar";
                    TextBox1.PasswordChar = '●';
                    break;
            }
        }
    }
}
