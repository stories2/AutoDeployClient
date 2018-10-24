using AutoDeployClientService.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDeployClientService.Utils
{
    internal class LogManager
    {
        public static void PrintLogMessage(String className = "LogManager", String methodName = "PrintLogMessage", String msg = "Empty log msg", int logLevel = DefineManager.LOG_LEVEL_WARN)
        {
            String logMsg = "{0} {1} [{2}] <{3}> ({4})";
            String logLevelStr = DefineManager.LOG_LEVEL_WARN_STR;
            DateTime currentDateTime = DateTime.Now;
            switch (logLevel)
            {
                case DefineManager.LOG_LEVEL_VERBOSE:
                    logLevelStr = DefineManager.LOG_LEVEL_VERBOSE_STR;
                    break;

                case DefineManager.LOG_LEVEL_INFO:
                    logLevelStr = DefineManager.LOG_LEVEL_INFO_STR;
                    break;

                case DefineManager.LOG_LEVEL_DEBUG:
                    logLevelStr = DefineManager.LOG_LEVEL_DEBUG_STR;
                    break;

                case DefineManager.LOG_LEVEL_WARN:
                    logLevelStr = DefineManager.LOG_LEVEL_WARN_STR;
                    break;

                case DefineManager.LOG_LEVEL_ERROR:
                    logLevelStr = DefineManager.LOG_LEVEL_ERROR_STR;
                    break;
            }
            Debug.WriteLine(logMsg, currentDateTime.ToString(), logLevelStr, className, methodName, msg);
        }
    }
}