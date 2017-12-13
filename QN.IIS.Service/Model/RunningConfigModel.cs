using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QN.IIS.Service.Model
{
    public class RunningConfigModel
    {
        public string LogPath { get; set; }

        /// <summary>
        /// ONETIME  一次运行
        /// ALWAYS   持续运行
        /// </summary>
        public string RunningMode { get; set; }

    }
}
