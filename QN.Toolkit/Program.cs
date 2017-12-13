using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using Microsoft.Web.Administration;
using QN.IIS.Service;
using QN.IIS.Service.Model;
using QN.IIS.Service.Service;

namespace QN.IIS.Toolkit
{
    public class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("检测程序启动，当网站或其应用池停下后，会自动启动。");

            new Thread(RecoveryWebSite).Start();
        }

        static void RecoveryWebSite()
        {
            IKeepIISAliveService KeepIISAliveService = (IKeepIISAliveService)BeanFactory.getObject("KeepIISAliveService");
            KeepIISAliveService.KeepAlive();


            var config = (RunningConfigModel)BeanFactory.getObject("RunningConfigModel");
            if (config != null && config.RunningMode == "ONETIME")
            {
                Environment.Exit(0);
            }
        }

    }
}