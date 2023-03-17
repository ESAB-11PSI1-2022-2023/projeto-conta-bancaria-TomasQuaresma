// Copyright(c) Tomás Quaresma. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace TrabalhoContaBancaria
{
    public partial class Inicio : Form
    {
        ContaBancaria Conta = new ContaBancaria("12345","tomasgquaresma@gmail.com", "Tomás", 10000);
        public Inicio()
        {
            InitializeComponent();
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
                    
                    Conta = new ContaBancaria(valores[1], valores[0], valores[2], Convert.ToDecimal(valores[3]));
                    return;
                }
            }

        }
        public void AtualizarSaldo()
        {
            Saldo.Text = Conta.Saldo + " EUR";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] nome = Conta.Titular.Split(' ');
            switch (DateTime.Now.Hour)
            {
                case int Dia when Dia >= 0 && Dia < 12:
                    ReceberUtilizador.Text = "Bom dia " + nome[0];
                    break;
                case int Tarde when Tarde >= 12 && Tarde < 18:
                    ReceberUtilizador.Text = "Boa tarde " + nome[0];
                    break;
                default:
                    ReceberUtilizador.Text = "Boa noite " + nome[0];
                    break;
            }

            AtualizarSaldo();

            // Cria uma lista vazia
            List<string> lista = new List<string>();

            // Abre o arquivo para leitura
            using (StreamReader sr = new StreamReader(@"Contactos.txt"))
            {
                // Lê cada linha do arquivo
                string linha;
                while ((linha = sr.ReadLine()) != null)
                {
                    // Separa o e-mail e os elementos associados
                    string[] partes = linha.Split(':');
                    string email = partes[0];
                    try
                    {
                        string mail = Conta.Email;
                        AtualizarOpcoes(partes[1]);
                        Contato1.Text = Conta.Titular;
                        AtualizarOpcoes(partes[2]);
                        Contato2.Text = Conta.Titular;
                        AtualizarOpcoes(partes[3]);
                        Contato3.Text = Conta.Titular;
                        AtualizarOpcoes(partes[4]);
                        Contato4.Text = Conta.Titular;

                    }
                    catch
                    {

                    }

                    if (Contato1.Text == "")
                    {
                        IContato1.Visible = false;
                    }
                    if (Contato2.Text == "")
                    {
                        IContato2.Visible = false;
                    }
                    if (Contato3.Text == "")
                    {
                        IContato3.Visible = false;
                    }
                    if (Contato4.Text == "")
                    {
                        IContato4.Visible = false;
                    }
                }
            }

            // Abre o arquivo para leitura
            using (StreamReader sr = new StreamReader(@"Solicitacoes.txt"))
            {
                // Lê cada linha do arquivo
                string linha;
                while ((linha = sr.ReadLine()) != null)
                {
                    // Separa o e-mail e os elementos associados
                    string[] partes = linha.Split(':');
                    string email = partes[0];
                    if (partes[0] == Conta.Email)
                    {
                        string[] dividir;
                        if (partes.Length - 1 >= 1)
                        {
                            dividir = partes[1].Split(',');
                            AtualizarOpcoes(dividir[0]);
                            ISolicitar1.Tag = dividir[0] + "," + dividir[1];
                            Solicitar1.Text = Conta.Titular + " solicitou " + dividir[1] + " Eur";
                        }
                        else
                        {
                            ISolicitar1.Visible= false;
                            Solicitar1.Visible = false;
                        }
                        if (partes.Length - 1 >= 2)
                        {
                            dividir = partes[2].Split(',');
                            AtualizarOpcoes(dividir[0]);
                            ISolicitar2.Tag = dividir[0] + "," + dividir[1];
                            Solicitar2.Text = Conta.Titular + " solicitou " + dividir[1] + " Eur";
                        }
                        else
                        {
                            ISolicitar2.Visible = false;
                            Solicitar2.Visible = false;
                        }
                            
                            AtualizarOpcoes(email);

                    }
                    
                }
            }
        }

        private void CarregarMultibanco_MouseEnter(object sender, EventArgs e)
        {
            CarregarMultibanco.Font = new Font(CarregarMultibanco.Font, FontStyle.Underline);
        }
        private void CarregarMultibanco_MouseLeave(object sender, EventArgs e)
        {
            CarregarMultibanco.Font = new Font(CarregarMultibanco.Font, FontStyle.Regular);
        }

        private void CarregarMultibanco_Click(object sender, EventArgs e)
        {
            Multibanco form = new Multibanco();
            form.ReceberInformacoes(Conta.Email);
            form.Show();
        }

        private void Atualizar_Tick(object sender, EventArgs e)
        {
            AtualizarOpcoes(Conta.Email);
            AtualizarSaldo();
        }

        private void TerminarSessao_Click(object sender, EventArgs e)
        {
            this.Visible= false;
            Login login= new Login();
            login.Visible = true;
        }

        private void DepositoBancario_Click(object sender, EventArgs e)
        {
            Depositos form = new Depositos();
            form.ReceberInformacoes(Conta.Email);
            form.Show();
        }

        private void EnviarSolicitar_Click(object sender, EventArgs e)
        {
            EnviarSolicitar form = new EnviarSolicitar();
            form.AtualizarOpcoes(Conta.Email);
            form.Show();
            this.Visible = false;
        }

        private void Enviar_Click(object sender, EventArgs e)
        {
            EnviarSolicitar form = new EnviarSolicitar();
            form.AtualizarOpcoes(Conta.Email);
            form.Show();
            this.Visible = false;
        }

        private void Solicitar_Click(object sender, EventArgs e)
        {
            EnviarSolicitar form = new EnviarSolicitar();
            form.AtualizarOpcoes(Conta.Email);
            form.Solicitar();
            form.Show();
            this.Visible = false;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Depositos form = new Depositos();
            form.ReceberInformacoes(Conta.Email);
            form.Show();
        }

        private void ISolicitar1_Click(object sender, EventArgs e)
        {
            string[] dividir = Convert.ToString(ISolicitar1.Tag).Split(',');
            EnviarSolicitar form = new EnviarSolicitar();
            form.AtualizarOpcoes(Conta.Email);
            form.AceitarSolicitacoes(dividir[0], Convert.ToDecimal(dividir[1]));
            form.Show();
            this.Visible = false;
        }

        private void ISolicitar2_Click(object sender, EventArgs e)
        {
            string[] dividir = Convert.ToString(ISolicitar2.Tag).Split(',');
            EnviarSolicitar form = new EnviarSolicitar();
            form.AtualizarOpcoes(Conta.Email);
            form.AceitarSolicitacoes(dividir[0], Convert.ToDecimal(dividir[1]));
            form.Show();
            this.Visible = false;
        }
    }
}
