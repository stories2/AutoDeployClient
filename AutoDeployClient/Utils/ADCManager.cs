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
        public int CreateNewProcess(PushMsgModel pushMsgModel)
        {
            int newProcessId = DefineManager.NOT_AVAILABLE;

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

                    newProcessId = adcPushData.ADC_Index;

                    ADC_Status adcStatus = new ADC_Status();
                    adcStatus.ADC_Index = newProcessId;
                    adcStatus.ADC_ProcessStatus = DefineManager.STATUS_CODE_DEFAULT;
                    adcStatus.ADC_UpdateDateTime = DateTime.Now;

                    context.ADC_Status.Add(adcStatus);
                    context.SaveChanges();

                    tran.Commit();

                    LogManager.PrintLogMessage("ADCManager", "CreateNewProcess", "process created, id: " + newProcessId, DefineManager.LOG_LEVEL_DEBUG);
                }
                catch (Exception err)
                {
                    tran.Rollback();
                    LogManager.PrintLogMessage("ADCManager", "CreateNewProcess", "cannot create new process: " + err.Message, DefineManager.LOG_LEVEL_ERROR);
                }
            }
            return newProcessId;
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

                    LogManager.PrintLogMessage("ADCManager", "UpdateCurrentProcessStatus", "process status updated, status: " + adcStatus.ADC_StatusCode, DefineManager.LOG_LEVEL_DEBUG);
                }
                catch (Exception err)
                {
                    tran.Rollback();
                    LogManager.PrintLogMessage("ADCManager", "UpdateCurrentProcessStatus", "cannot update process status: " + err.Message, DefineManager.LOG_LEVEL_ERROR);
                }
            }
        }

        public void UpdateCurrentProcessInfo(ADC_PushData adcPushData)
        {
            using (AutoDeployClientEntities context = new AutoDeployClientEntities())
            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    ADC_PushData selectedADCPushData = context.ADC_PushData.Where(selectedADCPushDataItem => selectedADCPushDataItem.ADC_Index == adcPushData.ADC_Index).FirstOrDefault();
                    selectedADCPushData.ADC_DownloadedPath = adcPushData.ADC_DownloadedPath;
                    selectedADCPushData.ADC_ExtractedPath = adcPushData.ADC_ExtractedPath;

                    context.SaveChanges();
                    tran.Commit();

                    LogManager.PrintLogMessage("ADCManager", "UpdateCurrentProcessInfo", "process info updated, downloaded path: " + selectedADCPushData.ADC_DownloadedPath + " extracted path: " + selectedADCPushData.ADC_ExtractedPath, DefineManager.LOG_LEVEL_DEBUG);
                }
                catch (Exception err)
                {
                    tran.Rollback();
                    LogManager.PrintLogMessage("ADCManager", "UpdateCurrentProcessInfo", "cannot update process status: " + err.Message, DefineManager.LOG_LEVEL_ERROR);
                }
            }
        }
    }
}