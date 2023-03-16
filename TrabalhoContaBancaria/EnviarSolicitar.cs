// Copyright(c) Tomás Quaresma. All rights reserved.

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

        /// <summary>
        ///Menu inicial superior 1
        /// </summary>
        private void Dashboard_Click(object sender, EventArgs e)
        {
            Inicio form = new Inicio();
            form.AtualizarOpcoes(Conta.Email);
            form.Show();
            this.Visible = false;
        }
        private void TerminarSessao_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Login login = new Login();
            login.Visible = true;
        }

        /// <summary>
        ///Menu inicial superior 2
        /// </summary>

        public void Enviar()
        {
            Titulo1.Text = "Enviar dinheiro";
            Continuar.Visible = false;
            Cancelar.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel3.Visible = true;
            tableLayoutPanel18.Visible = true;
            ValidarEmail.Visible = true;
            Titulo1.TextAlign = ContentAlignment.MiddleLeft;
            EmailDestino.TextAlign = HorizontalAlignment.Left;
            EmailDestino.Text = null;
        }
        public void Solicitar()
        {
            Titulo1.Text = "Solicitar dinheiro";
        }
        public void Contactos()
        {

        }

        private void MenuEnviar_Click(object sender, EventArgs e)
        {
            Enviar();
        }

        private void MenuSolicitar_Click(object sender, EventArgs e)
        {
            Solicitar();
        }

        private void MenuContactos_Click(object sender, EventArgs e)
        {
            Contactos();
        }

        /// <summary>
        ///Menu inicial central
        /// </summary>

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
                            Titulo1.Text = valores[2];
                            Continuar.Visible = true;
                            Cancelar.Visible = true;
                            label3.Visible = true;
                            label4.Visible = true;
                            panel1.BorderStyle = BorderStyle.None;
                            tableLayoutPanel3.Visible = false;
                            tableLayoutPanel18.Visible = false;
                            ValidarEmail.Visible = false;
                            Titulo1.TextAlign = ContentAlignment.MiddleCenter;
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
        private void Cancelar_Click(object sender, EventArgs e)
        {
            if(MenuEnviar.ForeColor == Color.MidnightBlue)
            {
                Enviar();
            }
            else if (MenuSolicitar.ForeColor == Color.MidnightBlue)
            {
                Solicitar();
            }else if (MenuContactos.ForeColor == Color.MidnightBlue)
            {
                Contactos();
            }
            
        }

        private void ApagarTextBox_Click(object sender, EventArgs e)
        {
            EmailDestino.Text = null;
        }

        private void EmailDestino_Click(object sender, EventArgs e)
        {
            if (EmailDestino.Text == "0,00")
            {
                EmailDestino.Text = null;
            }
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

        private void Continuar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MenuEnviar.ForeColor == Color.MidnightBlue)
                {
                    if (Convert.ToDecimal(EmailDestino.Text) >= 0 && Conta.Saldo - Convert.ToDecimal(EmailDestino.Text) >= 0)
                    {
                        Conta.Transferir(Conta.Email, EmailDestinatario, Convert.ToDecimal(EmailDestino.Text));
                        AtualizarOpcoes();
                        this.Visible = false;
                        Inicio form = new Inicio();
                        form.Visible = true;

                    }
                    else
                    {
                        MessageBox.Show("Por favor, insira um número válido maior que 0.");
                    }
                }
                else if (MenuSolicitar.ForeColor == Color.MidnightBlue)
                {
                    if (Convert.ToDecimal(EmailDestino.Text) >= 0)
                    {
                        Conta.Solicitar(Conta.Email, EmailDestinatario, Convert.ToDecimal(EmailDestino.Text));
                        AtualizarOpcoes();
                        this.Visible = false;
                        Inicio form = new Inicio();
                        form.Visible = true;

                    }
                    else
                    {
                        MessageBox.Show("Por favor, insira um número válido maior que 0.");
                    }
                }
                else if (MenuContactos.ForeColor == Color.MidnightBlue)
                {

                }

                
            }
            catch
            {
                MessageBox.Show("Os caracteres introduzidos não são validos");
            }
        }

        public void AtualizarOpcoes()
        {
            List<string> linhasAtualizadas = new List<string>();
            // Ler o conteúdo do arquivo em uma variável
            string conteudo = File.ReadAllText(@"Contactos.txt");

            // Dividir o conteúdo em linhas separadas
            string[] linhas = conteudo.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            // Loop pelas linhas do arquivo
            foreach (string linha in linhas)
            {
                // Dividir cada linha em suas partes
                string[] partes = linha.Split(':');

                // Verificar se o e-mail corresponde ao e-mail alvo
                if (partes[0] == Conta.Email)
                {
                    // Atualizar o saldo na linha
                    bool JaExiste = false;
                    foreach (string i in partes)
                    {
                        if (i == EmailDestinatario)
                        {
                            JaExiste= true;
                        }
                    }
                    if(JaExiste)
                    {
                        partes[0] = partes[0];
                    }
                    else
                    {
                        partes[0] = partes[0] + ":" + EmailDestinatario;
                    }
                    
                    // Armazenar a linha atualizada em uma nova variável
                    string linhaAtualizada = string.Join(":", partes);

                    // Adicionar a linha atualizada à lista de linhas atualizadas
                    linhasAtualizadas.Add(linhaAtualizada);
                }
                else
                {
                    // Armazenar a linha original em uma nova variável
                    linhasAtualizadas.Add(linha);
                }
            }

            // Escrever as linhas atualizadas de volta no arquivo de bloco de notas
            File.WriteAllText(@"Contactos.txt", string.Join(Environment.NewLine, linhasAtualizadas));

        }


    }
}

