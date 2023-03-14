// Copyright(c) Tomás Quaresma. All rights reserved.

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
    public partial class Inicio : Form
    {
        ContaBancaria Conta { get; set; }
        ContaBancaria Conta0 = new ContaBancaria("AB0001-661", "Tintim",10000);
        ContaBancaria Conta1 = new ContaBancaria("0658187233661", "Tintim", 10000);
        ContaBancaria Conta2 = new ContaBancaria("UT0023-110", "Tintim", 10000);
        public Inicio()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
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
        }

        /// <summary>
        /// Botão Deposito, adiciona a quantidade de saldo escolhida pelo utilizador
        /// </summary>
        private void Deposito_Click(object sender, EventArgs e)
        {
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
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
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

        }
    }
}
