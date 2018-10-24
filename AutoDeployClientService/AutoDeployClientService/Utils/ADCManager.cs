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
                }
                catch (Exception err)
                {
                    logManager.PrintLogMessage("ADCManager", "GetReadyForDeployFileList", "cannot get ready for deploy flie list: " + err.Message, System.Diagnostics.EventLogEntryType.Error);
                }
            }
            return readyForDeployFileList;
        }
    }
}