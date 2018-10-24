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
                    RunDeployProcess(adcPushData);
                }
            }
        }

        private void RunDeployProcess(ADC_PushData adcPushData)
        {
            ADC_Status adcStatus = adcManager.FindCurrentStatus(adcPushData);
            try
            {
                if (adcStatus == null)
                {
                    logManager.PrintLogMessage("AutoDeployRoutineManager", "RunDeployProcess", "cannot find adc status, process #" + adcPushData.ADC_Index, System.Diagnostics.EventLogEntryType.Warning);
                    return;
                }
                adcStatus.ADC_ProcessStatus = DefineManager.STATUS_CODE_PUBLISHING;
                adcManager.UpdateCurrentProcessStatus(adcStatus);

                adcStatus.ADC_ProcessStatus = DefineManager.STATUS_CODE_PUBLISH_DONE;
                adcManager.UpdateCurrentProcessStatus(adcStatus);
                logManager.PrintLogMessage("AutoDeployRoutineManager", "RunDeployProcess", "deploy successfully #" + adcPushData.ADC_Index, System.Diagnostics.EventLogEntryType.SuccessAudit);
            }
            catch (Exception err)
            {
                if (adcStatus != null)
                {
                    adcStatus.ADC_ProcessStatus = DefineManager.STATUS_CODE_PUBLISH_ERROR;
                    adcStatus.ADC_ProcessMsg = err.Message;
                    adcManager.UpdateCurrentProcessStatus(adcStatus);
                }
                logManager.PrintLogMessage("AutoDeployRoutineManager", "RunDeployProcess", "cannot deploy current process #" + adcPushData.ADC_Index, System.Diagnostics.EventLogEntryType.Error);
            }
        }
    }
}