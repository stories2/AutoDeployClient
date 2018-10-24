using AutoDeployClientService.Database;
using AutoDeployClientService.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDeployClientService.Utils
{
    internal class ADCManager
    {
        public LogManager logManager { get; set; }

        public List<ADC_PushData> GetReadyForDeployFileList()
        {
            List<ADC_PushData> readyForDeployFileList = null;

            using (AutoDeployClientEntities context = new AutoDeployClientEntities())
            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    readyForDeployFileList = context.ADC_PushData.Where(selectedPushMsgDataItem => selectedPushMsgDataItem.ADC_OrderType == DefineManager.ORDER_TYPE_NORMAL_DEPLOY &&
                                                                            selectedPushMsgDataItem.ADC_Status.ADC_ProcessStatus == DefineManager.STATUS_CODE_DEPLOY_FILE_IS_READY).ToList();
                    logManager.PrintLogMessage("ADCManager", "GetReadyForDeployFileList", "ready for deploy file list size: " + readyForDeployFileList.Count, System.Diagnostics.EventLogEntryType.Information);
                }
                catch (Exception err)
                {
                    logManager.PrintLogMessage("ADCManager", "GetReadyForDeployFileList", "cannot get ready for deploy flie list: " + err.Message, System.Diagnostics.EventLogEntryType.Error);
                }
            }
            return readyForDeployFileList;
        }

        public void UpdateCurrentProcessStatus(ADC_Status adcStatus)
        {
            using (AutoDeployClientEntities context = new AutoDeployClientEntities())
            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    ADC_Status selectedADCStatus = context.ADC_Status.Where(selectedADCStatusItem => selectedADCStatusItem.ADC_Index == adcStatus.ADC_Index).FirstOrDefault();
                    selectedADCStatus.ADC_ProcessMsg = adcStatus.ADC_ProcessMsg;
                    selectedADCStatus.ADC_ProcessStatus = adcStatus.ADC_ProcessStatus;
                    selectedADCStatus.ADC_UpdateDateTime = DateTime.Now;

                    context.SaveChanges();
                    tran.Commit();

                    logManager.PrintLogMessage("ADCManager", "UpdateCurrentProcessStatus", "process status updated, status: " + adcStatus.ADC_StatusCode, System.Diagnostics.EventLogEntryType.Information);
                }
                catch (Exception err)
                {
                    tran.Rollback();
                    logManager.PrintLogMessage("ADCManager", "UpdateCurrentProcessStatus", "cannot update process status: " + err.Message, System.Diagnostics.EventLogEntryType.Error);
                }
            }
        }
    }
}