using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace Common
{
    public static class APPSettings
    {
        public static string GetSetting(string name) {
          return   ConfigurationManager.AppSettings[name].ToString();
        }
    }
}
