using AutoDeployClientService.Database;
using AutoDeployClientService.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDeployClientService.Utils
{
    internal class AutoDeployRoutineManager
    {
        public ADCManager adcManager { get; set; }
        public LogManager logManager { get; set; }

        public void StartRoutine()
        {
            List<ADC_PushData> adcPushDataList = adcManager.GetReadyForDeployFileList();
            if (adcPushDataList.Count > DefineManager.ZERO)
            {
                foreach (ADC_PushData adcPushData in adcPushDataList)
                {
                    logManager.PrintLogMessage("AutoDeployRoutineManager", "StartRoutine", "process #" + adcPushData.ADC_Index + " is ready", System.Diagnostics.EventLogEntryType.Information);
                }
            }
        }
    }
}