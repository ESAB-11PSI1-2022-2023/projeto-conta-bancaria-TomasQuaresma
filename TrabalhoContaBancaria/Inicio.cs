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
        ContaBancaria Conta = new ContaBancaria("Teste", "Teste",0);
        public Inicio()
        {
            InitializeComponent();
        }
        public void ReceberInformacoes(string mail)
        {
            string[] linhas = File.ReadAllLines(@"Contas.txt");
            foreach (string linha in linhas)
            {
                string[] valores = linha.Split(':');
                if (valores[0].Equals(mail))
                {
                    string[] nome = valores[2].Split(' ');
                    Conta = new ContaBancaria(valores[1], nome[0], Convert.ToDecimal(valores[3]));
                    return;
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            switch (DateTime.Now.Hour)
            {
                case int Dia when Dia >= 0 && Dia < 12:
                    ReceberUtilizador.Text = "Bom dia " + Conta.Titular;
                    break;
                case int Tarde when Tarde >= 12 && Tarde < 18:
                    ReceberUtilizador.Text = "Boa tarde " + Conta.Titular;
                    break;
                default:
                    ReceberUtilizador.Text = "Boa noite " + Conta.Titular;
                    break;
            }

            Saldo.Text = Conta.Saldo + " EUR";
            

        }
        
        /// <summary>
        /// Botão Fechar
        /// </summary>
        private void Fechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Botão Levantamento, retira a quantidade de saldo escolhida pelo utilizador
        /// </summary>
        private void Levantamento_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                if (Convert.ToInt32(Quantia.Text) < 0)
                {
                    MessageBox.Show("Introduza um valor numérico valido.","Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    Quantia.Text = null;
                }
            }
            catch
            {
                MessageBox.Show("Introduza um valor numérico valido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Quantia.Text = null;
            }

            if (Convert.ToDecimal(Quantia.Text) <= Conta.Saldo)
            {
                Conta.Saldo -=Convert.ToDecimal(Quantia.Text);
            }
            else
            {
                MessageBox.Show("Não tem saldo suficiente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Quantia.Text = null;
            AtualizarOpcoes();
            */
        }

        /// <summary>
        /// Botão Deposito, adiciona a quantidade de saldo escolhida pelo utilizador
        /// </summary>
        private void Deposito_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                if (Convert.ToInt32(Quantia.Text) < 0)
                {
                    MessageBox.Show("Introduza um valor numérico valido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Quantia.Text = null;
                }
            }
            catch
            {
                MessageBox.Show("Introduza um valor numérico valido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Quantia.Text = null;
            }

            Conta.Saldo += Convert.ToDecimal(Quantia.Text);
            
            Quantia.Text = null;
            AtualizarOpcoes();
            */
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           /* 
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    Conta = Conta0;
                    Numero.Text = Conta0.Numero.ToString();
                    Nome.Text = Conta0.Titular.ToString();
                    Saldo.Text = Conta0.Saldo.ToString() + " €";
                    break;
                case 1:
                    Conta = Conta1;
                    Numero.Text = Conta1.Numero.ToString();
                    Nome.Text = Conta1.Titular.ToString();
                    Saldo.Text = Conta1.Saldo.ToString() + " €";
                    break;
                case 2:
                    Conta = Conta2;
                    Numero.Text = Conta2.Numero.ToString();
                    Nome.Text = Conta2.Titular.ToString();
                    Saldo.Text = Conta2.Saldo.ToString() + " €";
                    break;
            }

        }

        public void AtualizarOpcoes()
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    Conta0 = Conta;
                    Numero.Text = Conta0.Numero.ToString();
                    Nome.Text = Conta0.Titular.ToString();
                    Saldo.Text = Conta0.Saldo.ToString() + " €";
                    break;
                case 1:
                    Conta1 = Conta;
                    Numero.Text = Conta1.Numero.ToString();
                    Nome.Text = Conta1.Titular.ToString();
                    Saldo.Text = Conta1.Saldo.ToString() + " €";
                    break;
                case 2:
                    Conta2 = Conta;
                    Numero.Text = Conta2.Numero.ToString();
                    Nome.Text = Conta2.Titular.ToString();
                    Saldo.Text = Conta2.Saldo.ToString() + " €";
                    break;
            }
           */

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

        }
    }
}
