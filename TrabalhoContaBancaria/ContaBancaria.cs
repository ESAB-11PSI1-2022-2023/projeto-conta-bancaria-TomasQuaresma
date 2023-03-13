// Copyright(c) Tomás Quaresma. All rights reserved.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoContaBancaria
{
    internal class ContaBancaria
    {
        //Número de conta
        public string Numero { get; set; }
        //Titular da conta
        public string Titular { get; set;}
        //Saldo da conta
        public decimal Saldo { get; set;}

        public ContaBancaria(string numero,string titular,decimal saldo) 
        { 
            Numero = numero;
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
        public void Transferir(ContaBancaria contaDestino, decimal quantia)
        {
            Saldo -= quantia;
            contaDestino.Saldo += quantia;
        }
    }
}
