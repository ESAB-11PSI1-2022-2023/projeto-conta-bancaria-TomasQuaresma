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
    public partial class Multibanco : Form
    {
        Random rnd = new Random();
        public Multibanco()
        {
            InitializeComponent();
        }

        private void Multibanco_Load(object sender, EventArgs e)
        {
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
    }
}
