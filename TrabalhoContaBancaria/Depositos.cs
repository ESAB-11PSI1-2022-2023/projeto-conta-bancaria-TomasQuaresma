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

namespace TrabalhoContaBancaria
{
    public partial class Depositos : Form
    {
        ContaBancaria Conta = new ContaBancaria("Teste", "tomas@gmail.com", "Teste", 0);
        public Depositos()
        {
            InitializeComponent();
        }

        private void Depositos_Load(object sender, EventArgs e)
        {

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

        private void Aplicar_Click(object sender, EventArgs e)
        {
            try { 
            if (Convert.ToDecimal(textBox1.Text) >= 10 && Conta.Saldo - Convert.ToDecimal(textBox1.Text)>=0)
            {
                Conta.Levantar(Convert.ToDecimal(textBox1.Text));
                textBox1.Text = null;
                AtualizarOpcoes();
                this.Visible = false;
            }
            else
            {
                MessageBox.Show("Não foi possivel realizar a operação");
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
