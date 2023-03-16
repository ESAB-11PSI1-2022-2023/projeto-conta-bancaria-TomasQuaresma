// Copyright(c) Tomás Quaresma. All rights reserved.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoContaBancaria
{
    internal class ContaBancaria
    {
        //Número de conta
        public string Numero { get; set; }
        //Email da conta
        public string Email { get; set; }
        //Titular da conta
        public string Titular { get; set;}
        //Saldo da conta
        public decimal Saldo { get; set;}

        public ContaBancaria(string numero,string email,string titular,decimal saldo) 
        { 
            Numero = numero;
            Email = email;
            Titular = titular;
            Saldo = saldo;
        }

        /// <summary>
        /// Metodo Depositar() : efetua um depósito de uma determinada quantia 
        /// e retorna o saldo da conta após a operação
        /// </summary>
        public decimal Depositar(decimal quantia)
        {
            Saldo += quantia;
            return Saldo;
        }

        /// <summary>
        /// Metodo Levantar() : efetuar um levantamento de uma determinada quantia 
        /// e retorna o saldo da conta após a operação
        /// </summary>
        public decimal Levantar(decimal quantia)
        {
            Saldo -= quantia;
            return Saldo;
        }

        /// <summary>
        /// Metodo Transferir() : efetua uma transferência entre contas bancárias, a transferência é efetuada
        ///para uma instância da classe ContaBancaria(parâmetro contaDestino) e retorna o saldo da
        ///conta de origem após a operação
        /// </summary>
        public void Transferir(string conta,string contaDestino, decimal quantia)
        {
            Saldo -= quantia;
            //contaDestino.Saldo += quantia;

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
                if (partes[0] == conta || partes[0] == contaDestino)
                {
                    // Atualizar o saldo na linha
                    if (partes[0] == conta){
                        partes[3] = Convert.ToString(Saldo);
                    }
                    else if (partes[0] == contaDestino)
                    {
                        partes[3] = Convert.ToString(Convert.ToDecimal(partes[3])+quantia);
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
            File.WriteAllText(@"Contas.txt", string.Join(Environment.NewLine, linhasAtualizadas));
        }

        public void Solicitar(string conta,string contaDestino,decimal quantia)
        {
            List<string> linhasAtualizadas = new List<string>();
            // Ler o conteúdo do arquivo em uma variável
            string conteudo = File.ReadAllText(@"Solicitacoes.txt");

            // Dividir o conteúdo em linhas separadas
            string[] linhas = conteudo.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            // Loop pelas linhas do arquivo
            foreach (string linha in linhas)
            {
                // Dividir cada linha em suas partes
                string[] partes = linha.Split(':');

                // Verificar se o e-mail corresponde ao e-mail alvo
                if (partes[0] == contaDestino)
                {
                        partes[0] = partes[0] + ":" + conta +","+quantia;

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
            File.WriteAllText(@"Solicitacoes.txt", string.Join(Environment.NewLine, linhasAtualizadas));
        }
    }
}
