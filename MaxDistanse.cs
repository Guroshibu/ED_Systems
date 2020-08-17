using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ED_Systems
{
    public partial class FormMaxDistanse : Form
    {
        public double maxDist;
        public bool useLocalBase;
        public FormMaxDistanse()
        {
            InitializeComponent();
        }
        private void FormMaxDistanse_Load(object sender, EventArgs e)
        {
            int i = 100;
            if(useLocalBase)
            {
                while(i <= 30000)
                {
                    if(i <= 900)
                    {
                        cbxMaxDist.Items.Add(i);
                        i += 100;
                    }
                    else if(i >= 1000 && i <= 4500)
                    {
                        cbxMaxDist.Items.Add(i);
                        i += 500;
                    }
                    else if (i >= 5000 && i <= 9000)
                    {
                        cbxMaxDist.Items.Add(i);
                        i += 1000;
                    }
                    else
                    {
                        cbxMaxDist.Items.Add(i);
                        i += 5000;
                    }
                }
            }
            else
            {
                while(i <= 1000)
                {
                    cbxMaxDist.Items.Add(i);
                    i += 100;
                }
            }
            cbxMaxDist.SelectedIndex = cbxMaxDist.FindString(maxDist.ToString());
        }
        private void button1_Click(object sender, EventArgs e)
        {
            maxDist = Convert.ToDouble(cbxMaxDist.SelectedItem);
            this.Close();
        }
    }
}
