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
        public EventLog eventLog { get; set; }

        public void PrintLogMessage(String className = "LogManager", String methodName = "PrintLogMessage", String msg = "Empty log msg", EventLogEntryType eventLogEntryType = EventLogEntryType.Warning)
        {
            String logMsgFormat = "{0} {1} [{2}] <{3}> ({4})", logMsg = "";
            String logLevelStr = DefineManager.LOG_LEVEL_WARN_STR;
            DateTime currentDateTime = DateTime.Now;
            switch (eventLogEntryType)
            {
                case EventLogEntryType.Information:
                    logLevelStr = DefineManager.LOG_LEVEL_INFO_STR;
                    break;

                case EventLogEntryType.Warning:
                    logLevelStr = DefineManager.LOG_LEVEL_WARN_STR;
                    break;

                case EventLogEntryType.Error:
                    logLevelStr = DefineManager.LOG_LEVEL_ERROR_STR;
                    break;

                case EventLogEntryType.SuccessAudit:
                    logLevelStr = DefineManager.LOG_LEVEL_SUCCESS_STR;
                    break;

                case EventLogEntryType.FailureAudit:
                    logLevelStr = DefineManager.LOG_LEVEL_FAIL_STR;
                    break;
            }

            logMsg = String.Format(logMsgFormat, currentDateTime.ToString(), logLevelStr, className, methodName, msg);
            this.eventLog.WriteEntry(logMsg, eventLogEntryType);
        }
    }
}