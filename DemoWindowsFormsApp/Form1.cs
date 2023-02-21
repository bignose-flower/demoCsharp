using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoWindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpenDialog_Click(object sender, EventArgs e)
        {
            //ダイアログボックスの表示
            if (openFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK )
            {
                //選択されたファイルをテキストボックスに表示する
                txtSelectedFile.Text = openFileDialog2.FileName;
            }
        }

        private void btnSendFile_Click(object sender, EventArgs e)
        {
            string url = string.Format("http://localhost:8080/students/upload");
            var content = new MultipartFormDataContent();
            var fileName = txtSelectedFile.Text;
            var fileContent = new StreamContent(File.OpenRead(fileName));
            fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = Path.GetFileName(fileName)
            };

            //ファイル和文繰り返す
            content.Add(fileContent);

            var httpClient = new HttpClient();
            HttpResponseMessage response = httpClient.PostAsync(url, content).Result;
            Boolean status = response.IsSuccessStatusCode;
            if (status)
            {
                label4.Text = "OK";
                label4.ForeColor = System.Drawing.Color.Blue;
                label4.Visible = true;
            }
            else
            {
                label4.Text = "NG";
                label4.ForeColor = System.Drawing.Color.Red;
                label4.Visible = true;
            }
            
        }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }
    }
}
