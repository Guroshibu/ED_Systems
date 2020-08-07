using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;

using Newtonsoft.Json;

namespace ED_Systems
{
    public partial class FormDonate : Form
    {
        private string client_id;
        private string instance_id;
        private string redirect_uri = "https://yandex.ru/";

        public FormDonate()
        {
            InitializeComponent();

            client_id = Properties.Settings.Default.client_id;
            instance_id = Properties.Settings.Default.instance_id;

            //если неопределен то нужно получить
            //if (instance_id == "") string result = await GetInstanceID();

        }

        private void GetInstanceID()
        {
           
        }
    }

    
}
