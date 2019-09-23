using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;

namespace Walfrido.Service.FIP
{
    public partial class Service : ServiceBase
    {
        private System.Threading.Timer timer;
        private string lastIP = string.Empty;
        private string currentIP = string.Empty;
        private Email email;
        private int countMail = 1;

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                this.timer = new System.Threading.Timer(new System.Threading.TimerCallback(Timer_tick), null, 1800000, 1800000);
            }
            catch (Exception ex)
            {
                SetLog(ex.Message + ex.StackTrace);
            }
        }

        protected override void OnStop()
        {
        }

        private void Timer_tick(Object sender)
        {
            try
            {
                if (IsNewIP())
                {
                    this.email = new Email(new List<string>() { "suporteti@visomes.com.br" }, this.currentIP, "IP");
                    this.email.SendMail();
                    SetLog("send ip: " + DateTime.Now + " count: " + countMail);
                    countMail++;
                }
            }
            catch (Exception ex)
            {
                SetLog(ex.Message + ex.StackTrace);
            }
        }

        private Boolean IsNewIP()
        {
            string temp = IP.GetIP();
            this.lastIP = this.currentIP;
            this.currentIP = temp;
            return !this.lastIP.Equals(this.currentIP);
        }

        private void SetLog(string value)
        {
            StreamWriter ws = new StreamWriter(System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\log.txt", true);
            ws.WriteLine(value);
            ws.Flush();
            ws.Close();
        }
    }
}
