using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QN.IIS.Service.Model
{
    public class IISModel
    {
        /// <summary>
        /// 站点名称
        /// </summary>
        public string WebSiteName { get; set; }

        /// <summary>
        /// 应用程序池名称
        /// </summary>
        public string AppPoolName { get; set; }

    }
}
