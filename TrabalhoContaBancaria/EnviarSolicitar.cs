using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TrabalhoContaBancaria
{
    public partial class EnviarSolicitar : Form
    {
        ContaBancaria Conta = new ContaBancaria("Teste", "tomas@gmail.com", "Teste", 10);
        string EmailDestinatario { get; set; }
        public EnviarSolicitar()
        {
            InitializeComponent();
        }

        private void EnviarSolicitar_Load(object sender, EventArgs e)
        {

        }

        public void AtualizarOpcoes(string mail)
        {
            string[] linhas;
            using (StreamReader sr = new StreamReader("Contas.txt"))
            {
                linhas = sr.ReadToEnd().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            }

            foreach (string linha in linhas)
            {
                string[] valores = linha.Split(':');
                if (valores[0].Equals(mail))
                {
                    string[] nome = valores[2].Split(' ');
                    Conta = new ContaBancaria(valores[1], valores[0], nome[0], Convert.ToDecimal(valores[3]));
                    return;
                }
            }

        }

        private void ApagarTextBox_Click(object sender, EventArgs e)
        {
            EmailDestino.Text = null;
        }

        private void ValidarEmail_Click(object sender, EventArgs e)
        {
            try
            {
                string[] linhas = File.ReadAllLines(@"Contas.txt");
                foreach (string linha in linhas)
                {
                    string[] valores = linha.Split(':');
                    if (valores[0].Equals(EmailDestino.Text))
                    {
                        EmailDestinatario = EmailDestino.Text;
                        label1.Text = valores[2];
                        Continuar.Visible = true;
                        button10.Visible= true;
                        label3.Visible= true;
                        label4.Visible= true;
                        panel1.BorderStyle= BorderStyle.None;
                        tableLayoutPanel3.Visible = false;
                        tableLayoutPanel18.Visible = false;
                        ValidarEmail.Visible = false;
                        label1.TextAlign = ContentAlignment.MiddleCenter;
                        EmailDestino.TextAlign = HorizontalAlignment.Center;
                        EmailDestino.Text = "0,00";
                        return;
                    }
                }
                MessageBox.Show("Email invalido");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao ler o arquivo: " + ex.Message);
            }
        }

        private void EmailDestino_Click(object sender, EventArgs e)
        {
            if (EmailDestino.Text == "0,00")
            {
                EmailDestino.Text = null;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            label1.Text = "Enviar dinheiro";
            Continuar.Visible = false;
            button10.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel3.Visible = true;
            tableLayoutPanel18.Visible = true;
            ValidarEmail.Visible = true;
            label1.TextAlign = ContentAlignment.MiddleLeft;
            EmailDestino.TextAlign = HorizontalAlignment.Left;
            EmailDestino.Text = null;
        }

        private void Continuar_Click(object sender, EventArgs e)
        {
            try
            {


                if (Convert.ToDecimal(EmailDestino.Text) >= 0)
                {
                    Conta.Transferir(Conta.Email,EmailDestinatario,Convert.ToDecimal(EmailDestino.Text));
                    this.Visible = false;
                    Inicio form = new Inicio();
                    form.Visible= true;

                }
                else
                {
                    MessageBox.Show("Por favor, insira um número válido maior que 0.");
                }
            }
            catch
            {
                MessageBox.Show("Os caracteres introduzidos não são validos");
            }
        }
    }
}
