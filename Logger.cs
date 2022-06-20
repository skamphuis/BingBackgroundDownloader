using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace BingBackgroundDownloader
{
    internal class Logger
    {
        public static ILog GetLogger(string name) => LogManager.GetLogger(name);

        public static ILog GetLogger(Type type) => LogManager.GetLogger(type);

        private static ILog _logger = null;
        internal static ILog Instance
        {
            get
            {
                if (_logger == null)
                {
                    _logger = LogManager.GetLogger("BingBackgroundDownloader");

                    XmlConfigurator.Configure(new FileInfo(Path.Combine(AppContext.BaseDirectory, "Log4Net.config")));
                }

                return _logger;
            }
        }
    }
}
