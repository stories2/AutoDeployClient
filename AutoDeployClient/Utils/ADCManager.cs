using AutoDeployClient.Database;
using AutoDeployClient.Models;
using AutoDeployClient.Models.ADCManager;
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

        public List<ADCStatusReportModel> GetLatestADCStatusList(int limit = DefineManager.DEFAULT_ADC_STATUS_REPORT_RETURN_LIMIT)
        {
            List<ADCStatusReportModel> adcStatusReportList = null;

            using (AutoDeployClientEntities context = new AutoDeployClientEntities())
            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    List<ADC_PushData> adcPushDataList = context.ADC_PushData.Where(selectedPushDataItem => selectedPushDataItem.ADC_OrderType != DefineManager.EXECUTE_ORDER_GET_STATUS).ToList();
                    List<ADC_Status> adcStatusList = context.ADC_Status.ToList();

                    adcStatusReportList = adcPushDataList.Select(pushDataItem => new ADCStatusReportModel
                    {
                        adcIndex = pushDataItem.ADC_Index,
                        adcOrderType = pushDataItem.ADC_OrderType,
                        adcDownloadUrl = pushDataItem.ADC_DownloadUrl,
                        adcDownloadedPath = pushDataItem.ADC_DownloadedPath,
                        adcExtractedPath = pushDataItem.ADC_ExtractedPath,
                        adcUpdateTargetPath = pushDataItem.ADC_UpdateTargetPath,
                        adcPushMsg = pushDataItem.ADC_PushMsg,
                        adcVersion = pushDataItem.ADC_Version,
                        adcCallbackUrl = pushDataItem.ADC_CallbackUrl,
                        adcFileType = pushDataItem.ADC_FileType,

                        adcProcessStatus = ADCExtension.IsStatusNotNull(adcStatusList, pushDataItem.ADC_Index) ? ADCExtension.GetStatusItem(adcStatusList, pushDataItem.ADC_Index).ADC_ProcessStatus : DefineManager.NOT_AVAILABLE,
                        adcProcessMsg = ADCExtension.IsStatusNotNull(adcStatusList, pushDataItem.ADC_Index) ? ADCExtension.GetStatusItem(adcStatusList, pushDataItem.ADC_Index).ADC_ProcessMsg : null,
                        adcUpdateDateTime = ADCExtension.IsStatusNotNull(adcStatusList, pushDataItem.ADC_Index) ? ADCExtension.GetStatusItem(adcStatusList, pushDataItem.ADC_Index).ADC_UpdateDateTime.ToString() : DateTime.Now.ToString()
                    }).OrderByDescending(orderPushDataItem => orderPushDataItem.adcIndex).Take(limit).ToList();

                    LogManager.PrintLogMessage("ADCManager", "GetLatestADCStatusList", "status report size: " + adcStatusReportList.Count, DefineManager.LOG_LEVEL_INFO);
                }
                catch (Exception err)
                {
                    tran.Rollback();
                    LogManager.PrintLogMessage("ADCManager", "GetLatestADCStatusList", "cannot get process status: " + err.Message, DefineManager.LOG_LEVEL_ERROR);
                }
            }
            return adcStatusReportList;
        }
    }
}