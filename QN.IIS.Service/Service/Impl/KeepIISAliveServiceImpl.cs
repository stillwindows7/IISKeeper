using Microsoft.Web.Administration;
using QN.IIS.Service.Model;
using QN.IIS.Service.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QN.IIS.Service 
{
    public class KeepIISAliveServiceImpl : IKeepIISAliveService
    {
        public List<IISModel> IISModelList { get; set; }

        public RunningConfigModel RunningConfigModel { get; set; }

        public void KeepAlive()
        {

            if (IISModelList == null || IISModelList.Count == 0)
                return;
            ServerManager sm = new ServerManager();
            foreach (var item in IISModelList)
            {

                try
                {
                    var pool = sm.ApplicationPools[item.AppPoolName];
                    if (pool != null && pool.State == ObjectState.Stopped)
                    {
                        WriteLog("检测到应用池" + item.AppPoolName + "停止服务");
                        WriteLog("正在启动应用池" + item.AppPoolName);
                        if (pool.Start() == ObjectState.Started)
                        {
                            WriteLog("成功启动应用池" + item.AppPoolName);
                        }
                        else
                        {
                            WriteLog("启动应用池" + item.AppPoolName + "失败. ");
                        }
                    }

                    var site = sm.Sites[item.WebSiteName];
                    if (site != null && site.State == ObjectState.Stopped)
                    {
                        WriteLog("检测到网站" + item.WebSiteName + "停止服务");
                        WriteLog("正在启动网站" + item.WebSiteName);
                        if (site.Start() == ObjectState.Started)
                        {
                            WriteLog("成功启动网站" + item.WebSiteName);
                        }
                        else
                        {
                            WriteLog("启动网站" + item.WebSiteName + "失败. ");
                        }
                    }
                }
                catch (Exception ex)
                {
                    WriteLog(ex.Message.ToString());
                }

                GC.Collect();
            }
        }


        private void WriteLog(string msg)
        {
            var fPath = "c:\\RecoveryWebsiteLog.txt";
            if (!string.IsNullOrEmpty(RunningConfigModel.LogPath))
            {
                fPath = RunningConfigModel.LogPath;
            }
            if (!File.Exists(fPath))
            {
                File.Create(fPath).Close();
            }

            using (StreamWriter sw = new StreamWriter(fPath, true, Encoding.UTF8))
            {
                sw.WriteLine(string.Format("{0} , 时间{1}", msg, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            }
            Console.WriteLine(msg);

        }
    }
}
