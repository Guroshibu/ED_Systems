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
using System.IO;

using Newtonsoft.Json;

namespace ED_Systems
{
    public partial class FormDonate : Form
    {
        public class InstansIdResponce
        {
            public string status { get; set; } = "";
            public string instance_id { get; set; } = "";
            public string error { get; set; } = "";
        }
        public class ExternalPaymentResponce
        {
            public string status { get; set; } = "";
            public string request_id { get; set; } = "";
            public double contract_amount { get; set; } = 0;
            public string title { get; set; } = "";
            public string error { get; set; } = "";
        }
        public class AcsParams
        {
            public string MD { get; set; } = "";
            public string PaReq { get; set; } = "";
        }
        public class MoneySource
        {
            public string type { get; set; } = "";
            public string payment_card_type { get; set; } = "";
            public string pan_fragment { get; set; } = "";
            public string money_source_token { get; set; } = "";
        }

        public class ProcessExternalPaymentResponce
        {
            public string status { get; set; } = "";
            public string error { get; set; } = "";
            public string acs_uri { get; set; } = "";
            public AcsParams acs_params { get; set; } = new AcsParams();
            public MoneySource money_source { get; set; } = new MoneySource();
            public int next_retry { get; set; } = 0;
            public string invoice_id { get; set; } = "";
        }

        private string client_id;
        private string instance_id;
        private string wallet;
        private string redirect_uri = "https://yandex.ru/";
        private double amount;
        private double contractAmount;

        public FormDonate()
        {
            InitializeComponent();

            client_id = Properties.Settings.Default.client_id;
            instance_id = Properties.Settings.Default.instance_id;
            wallet = Properties.Settings.Default.wallet;

            //System.Uri url = new Uri(@"ms-appx-web:///html/Donate.html");
            System.Uri url = new Uri("http://edspace.hostronavt.ru/index.php/donate");
            webView.Navigate(url);
            //если неопределен то нужно получить
            //todo отключено для отладки 
            //if (instance_id == "") GetInstanceID();
        }

        private string PostRequest(string[] data, string method)
        {
            WebRequest request;
            WebResponse response;
            Stream dataStream;
            string responseFromServer;

            request = WebRequest.Create("https://money.yandex.ru/api/" + method);
            request.Method = "POST";
            // данные для отправки
            string sendData = String.Join("&", data);
            // устанавливаем тип содержимого - параметр ContentType
            request.ContentType = "application/x-www-form-urlencoded";
            // преобразуем данные в массив байтов
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(sendData);
            // Устанавливаем заголовок Content-Length запроса - свойство ContentLength
            request.ContentLength = byteArray.Length;
            //записываем данные в поток запроса
            using (dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            response = request.GetResponse();
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            responseFromServer = reader.ReadToEnd();
            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

        private void GetInstanceID()
        {
            string[] data = new string[1];
            data[0] = "client_id=" + client_id;

            string responce = PostRequest(data, "instance-id");
            InstansIdResponce instansIdResponce = new InstansIdResponce();

            instansIdResponce = JsonConvert.DeserializeObject<InstansIdResponce>(responce);
            if(instansIdResponce.status == "success")
            {
                instance_id = instansIdResponce.instance_id;
                Properties.Settings.Default.instance_id = instance_id;
                Properties.Settings.Default.Save();
            }
            else
            {
                //lblResult.Text = "Error: " + instansIdResponce.error;
            }
        }
        private void ProcessExternalPayment(string[] data, string requestId)
        {

        }
        private void btnDonate_Click(object sender, EventArgs e)
        {
            /*bool isNum = double.TryParse(tbxAmount.Text, out amount);
            if (!isNum)
            {
                MessageBox.Show("Invalid amount");
                return;
            }*/
            //Создание платежа и проверка его параметров
            string[] data = new string[5];
            data[0] = "pattern_id=p2p";
            data[1] = "instance_id=" + instance_id;
            data[2] = "to=" + wallet;
            data[3] = "amount_due=" + amount.ToString().Replace(",", ".");
            data[4] = "message=ED_Systems";

            string responce = PostRequest(data, "request-external-payment");
            ExternalPaymentResponce externalPaymentResponce = new ExternalPaymentResponce();
            externalPaymentResponce = JsonConvert.DeserializeObject<ExternalPaymentResponce>(responce);
            if (externalPaymentResponce.status == "success")
            {
                //Проведение платежа
                data = new string[4];
                data[0] = "request_id=" + externalPaymentResponce.request_id;
                data[1] = "instance_id=" + instance_id;
                data[2] = "ext_auth_success_uri=" + "";
                data[3] = "ext_auth_fail_uri=" + "";

                responce = PostRequest(data, "process-external-payment");
                ProcessExternalPaymentResponce processExternalPaymentResponce = new ProcessExternalPaymentResponce();
                processExternalPaymentResponce = JsonConvert.DeserializeObject<ProcessExternalPaymentResponce>(responce);
                while(processExternalPaymentResponce.status != "success")
                {
                    if(processExternalPaymentResponce.status != "refused")
                    {
                        //ошибка
                        //lblResult.Text = "Error: " + processExternalPaymentResponce.error;
                        return;
                    }
                    if (processExternalPaymentResponce.status != "in_progress")
                    {
                        //повторный запрос
                        System.Threading.Thread.Sleep(processExternalPaymentResponce.next_retry);
                        responce = PostRequest(data, "process-external-payment");
                        processExternalPaymentResponce = new ProcessExternalPaymentResponce();
                        processExternalPaymentResponce = JsonConvert.DeserializeObject<ProcessExternalPaymentResponce>(responce);
                    }

                }

            }
            else
            {
                //lblResult.Text = "Error: " + externalPaymentResponce.error;
            }
        }
    }
}
