using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Context.Support;
using Spring.Context;



namespace QN.IIS.Service
{
    /// <summary>
    /// 获得本项目的beanfactory
    /// </summary>
    public class  BeanFactory
    {

        private static string DEFAULT_CONTEXT_LOCATION = "application.xml";

        private static IApplicationContext applicationContext;

        public static object getObject(String beanId)
        {
            if (applicationContext == null)
            {
                lock (DEFAULT_CONTEXT_LOCATION)
                {
                    if (applicationContext == null)
                    {

                        List<string> listConfig = new List<string>();
                        listConfig.Add("~/bin/" + DEFAULT_CONTEXT_LOCATION);
                        /*
                   //导入额外配置文件
                   string ConfigFileNames = System.Configuration.ConfigurationManager.AppSettings["ConfigFileNames"];
                   if (string.IsNullOrEmpty(ConfigFileNames) == false)
                   {
                       string ConfigFolderPath = System.Configuration.ConfigurationManager.AppSettings["ConfigFolderPath"];
                       string folder = "~/bin/";
                       if (string.IsNullOrEmpty(ConfigFolderPath) == false)
                       {
                           try
                           {
                               string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
                               basePath = basePath.Substring(0, basePath.LastIndexOf("\\"));
                               basePath = basePath.Substring(0, basePath.LastIndexOf("\\"));
                               string filePath = basePath + "\\ReleaseSystemConfigTestFile-投产千万不能加此文件.txt";
                               bool isFile = FileObj.FileExists(filePath);
                               if (isFile == true)
                               {
                                   string fileConent = FileObj.ReadFile(filePath).ToString().Trim();
                                   ConfigFolderPath = ConfigFolderPath.Insert(ConfigFolderPath.IndexOf(":/") + 2, fileConent + "/");
                               }
                           }
                           catch { }

                           folder = ConfigFolderPath;
                       }

                       foreach (var fileName in ConfigFileNames.Split(','))
                       {
                           listConfig.Add(folder + fileName);
                       }
                   }
                   else
                   {
                       //为了特殊处理兼容历史站点处理
                       listConfig.Add("~/bin/application-logger.xml");
                       listConfig.Add("~/bin/application-url.xml");
                   }
                  */
                        applicationContext = new XmlApplicationContext(listConfig.ToArray());

                    }
                }
            }
            object obj = applicationContext.GetObject(beanId);
            return obj;
        }
    }
}
