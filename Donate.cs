using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Yandex.Money.Api.Sdk;
using Yandex.Money.Api.Sdk.Authorization;
using Yandex.Money.Api.Sdk.Interfaces;
using Yandex.Money.Api.Sdk.Net;
using Yandex.Money.Api.Sdk.Requests;
using Yandex.Money.Api.Sdk.Responses;
using Yandex.Money.Api.Sdk.Utils;

using Newtonsoft.Json;

namespace ED_Systems
{
    public partial class FormDonate : Form
    {
        private string client_id;
        private string instance_id;
        private string redirect_uri = "https://yandex.ru/";

        private static IHostProvider hp = new DefaultHostsProvider();
        private static Authenticator auth = new Authenticator();
        IHttpClient dhpc = new DefaultHttpPostClient(hp, auth);

        public FormDonate()
        {
            InitializeComponent();

            client_id = Properties.Settings.Default.client_id;
            instance_id = Properties.Settings.Default.instance_id;

            //если неопределен то нужно получить
            //if (instance_id == "") string result = await GetInstanceID();

        }

        private async Task<string> GetInstanceID()
        {
            TaskCompletionSource<string> tcs;
            tcs = new TaskCompletionSource<string>();

            var instanceIdRequest = new InstanceIdRequest<InstanceIdResult>(dhpc, new JsonSerializer<InstanceIdResult>())
            {
                ClientId = client_id
            };

            var instanceIdResult = await instanceIdRequest.Perform();

            if (instanceIdResult.Status == ResponseStatus.Success)
            {
                instance_id = instanceIdResult.InstanceId;
                Properties.Settings.Default.instance_id = instance_id;
                Properties.Settings.Default.Save();
            }

            return tcs.Task.Result;
        }
    }

    public class Authenticator : DefaultAuthenticator
    {
        public override string Token { get; set; }
    }
}
