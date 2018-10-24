using AutoDeployClient.Database;
using AutoDeployClient.Models;
using AutoDeployClient.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoDeployClient.Utils
{
    public class ADCManager
    {
        public void CreateNewProcess(PushMsgModel pushMsgModel)
        {
            using (AutoDeployClientEntities context = new AutoDeployClientEntities())
            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    ADC_PushData adcPushData = new ADC_PushData();
                    adcPushData.ADC_OrderType = pushMsgModel.orderType;
                    adcPushData.ADC_DownloadUrl = pushMsgModel.downloadUrl;
                    adcPushData.ADC_UpdateTargetPath = pushMsgModel.updateTargetPath;
                    adcPushData.ADC_PushMsg = pushMsgModel.msg;
                    adcPushData.ADC_Version = pushMsgModel.version;
                    adcPushData.ADC_CallbackUrl = pushMsgModel.callbackUrl;
                    adcPushData.ADC_FileType = pushMsgModel.fileType;

                    context.ADC_PushData.Add(adcPushData);
                    context.SaveChanges();

                    ADC_Status adcStatus = new ADC_Status();
                    context.ADC_Status.Add(adcStatus);
                    context.SaveChanges();
                }
                catch (Exception err)
                {
                    tran.Rollback();
                    LogManager.PrintLogMessage("ADCManager", "CreateNewProcess", "cannot create new process: " + err.Message, DefineManager.LOG_LEVEL_ERROR);
                }
            }
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
                }
                catch (Exception err)
                {
                    tran.Rollback();
                    LogManager.PrintLogMessage("ADCManager", "UpdateCurrentProcessStatus", "cannot update process status: " + err.Message, DefineManager.LOG_LEVEL_ERROR);
                }
            }
        }
    }
}