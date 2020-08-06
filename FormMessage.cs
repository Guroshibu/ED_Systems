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
    public partial class FormMessage : Form
    {
        public string message;
        public FormMessage()
        {
            InitializeComponent();
        }


        private void FormMessage_Load(object sender, EventArgs e)
        {
            lblMessage.Text = message;
        }
    }
}
