// Copyright(c) Tomás Quaresma. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabalhoContaBancaria
{
    public partial class Multibanco : Form
    {
        Random rnd = new Random();
        ContaBancaria Conta = new ContaBancaria("Teste","Email", "Teste", 0);
        public Multibanco()
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
                    Conta = new ContaBancaria(valores[1], valores[0], nome[0], Convert.ToDecimal(valores[3]));
                    return;
                }
            }
        }
        private void Multibanco_Load(object sender, EventArgs e)
        {
            label3.Text = Conta.Email;

            // Gerar número de 5 dígitos
            int numeroDeCincoDigitos = rnd.Next(10000, 100000);

            // Gerar número de 9 dígitos com espaço a cada 3 dígitos
            string numeroDeNoveDigitosComEspaco = "";
            for (int i = 0; i < 9; i++)
            {
                numeroDeNoveDigitosComEspaco += rnd.Next(0, 10);
                if ((i + 1) % 3 == 0 && i != 8)
                {
                    numeroDeNoveDigitosComEspaco += " ";
                }
            }

            label6.Text = Convert.ToString(numeroDeCincoDigitos);
            label7.Text = Convert.ToString(numeroDeNoveDigitosComEspaco);
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                decimal valor = Convert.ToDecimal(textBox1.Text);
                
                if (valor >= 10)
                {
                    Conta.Depositar(valor);
                    AtualizarOpcoes();
                    textBox1.Text = null;
                    this.Visible = false;
                }
                else
                {
                    MessageBox.Show("Por favor, insira um número válido maior que 10.");
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show("O valor inserido não é válido.");
            }
            catch (OverflowException)
            {
                MessageBox.Show("O valor inserido é maior do que o máximo permitido.");
            }
            catch (FormatException)
            {
                MessageBox.Show("O texto inserido não está em um formato válido para um número decimal.");
            }
        }

        public void AtualizarOpcoes()
        {
            List<string> linhasAtualizadas = new List<string>();
            // Ler o conteúdo do arquivo em uma variável
            string conteudo = File.ReadAllText(@"Contas.txt");

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
                    partes[3] = Conta.Saldo.ToString();

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
            File.WriteAllText(@"Contas.txt", string.Join(Environment.NewLine, linhasAtualizadas));

        }
    }
}
