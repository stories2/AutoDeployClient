using AutoDeployClient.Database;
using AutoDeployClient.Models;
using AutoDeployClient.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoDeployClient.Utils
{
    public class ExecuteManager
    {
        public PushMsgModel pushMsgModel { get; set; }
        public ADCManager adcManager { get; set; }
        public ADC_Status adcStatus { get; set; }

        public ExecuteManager()
        {
        }

        public bool Execute()
        {
            bool success = false;
            try
            {
                DebugPushMsgModel(pushMsgModel);
                switch (pushMsgModel.orderType)
                {
                    case DefineManager.EXECUTE_ORDER_AUTO_UPDATE:
                        RutineAutoUpdate();
                        success = true;
                        break;

                    case DefineManager.EXECUTE_ORDER_GET_STATUS:
                        RutineGetStatus();
                        success = true;
                        break;

                    default:
                        success = false;
                        break;
                }
            }
            catch (Exception err)
            {
                adcStatus.ADC_ProcessMsg = err.Message;
                adcManager.UpdateCurrentProcessStatus(adcStatus);
                LogManager.PrintLogMessage("ExecuteManager", "Execute", "cannot execute order: " + err.Message);
            }

            return success;
        }

        private void RutineGetStatus()
        {
        }

        private void RutineAutoUpdate()
        {
            adcStatus.ADC_ProcessStatus = DefineManager.STATUS_CODE_DOWNLOADING_DEPLOY_FILE;
            adcManager.UpdateCurrentProcessStatus(adcStatus);

            String downloadedFilePath = FileManager.DownloadWebFile(pushMsgModel.downloadUrl, DefineManager.DIR_UPDATE_PATH, pushMsgModel.fileType);
            String extractedFilePath = null;
            if (downloadedFilePath != null)
            {
                adcStatus.ADC_ProcessStatus = DefineManager.STATUS_CODE_EXTRACTING_DEPLOY_FILE;
                adcManager.UpdateCurrentProcessStatus(adcStatus);

                extractedFilePath = FileManager.ExtractZipFile(downloadedFilePath);
                if (extractedFilePath != null)
                {
                    ADC_PushData adcPushData = new ADC_PushData();
                    adcPushData.ADC_Index = adcStatus.ADC_Index;
                    adcPushData.ADC_DownloadedPath = HttpContext.Current.Server.MapPath(downloadedFilePath);
                    adcPushData.ADC_ExtractedPath = extractedFilePath;
                    adcManager.UpdateCurrentProcessInfo(adcPushData);

                    adcStatus.ADC_ProcessStatus = DefineManager.STATUS_CODE_DEPLOY_FILE_IS_READY;
                    adcManager.UpdateCurrentProcessStatus(adcStatus);
                }
                else
                {
                    adcStatus.ADC_ProcessStatus = DefineManager.STATUS_CODE_ERROR_WHILE_EXTRACTING_DEPLOY_FILE;
                    LogManager.PrintLogMessage("ExecuteManager", "RutineAutoUpdate", "extract file failed", DefineManager.LOG_LEVEL_WARN);
                    throw new Exception();
                }
            }
            else
            {
                adcStatus.ADC_ProcessStatus = DefineManager.STATUS_CODE_ERROR_WHILE_DOWNLOADING_DEPLOY_FILE;
                LogManager.PrintLogMessage("ExecuteManager", "RutineAutoUpdate", "download file failed", DefineManager.LOG_LEVEL_WARN);
                throw new Exception();
            }
        }

        private void DebugPushMsgModel(PushMsgModel pushMsgModel)
        {
            LogManager.PrintLogMessage("RelayController", "DebugPushMsgModel", "push msg model -> orderType: " + pushMsgModel.orderType, DefineManager.LOG_LEVEL_DEBUG);
            LogManager.PrintLogMessage("RelayController", "DebugPushMsgModel", "push msg model -> downloadUrl: " + pushMsgModel.downloadUrl, DefineManager.LOG_LEVEL_DEBUG);
            LogManager.PrintLogMessage("RelayController", "DebugPushMsgModel", "push msg model -> updateTargetPath: " + pushMsgModel.updateTargetPath, DefineManager.LOG_LEVEL_DEBUG);
            LogManager.PrintLogMessage("RelayController", "DebugPushMsgModel", "push msg model -> msg: " + pushMsgModel.msg, DefineManager.LOG_LEVEL_DEBUG);
            LogManager.PrintLogMessage("RelayController", "DebugPushMsgModel", "push msg model -> version: " + pushMsgModel.version, DefineManager.LOG_LEVEL_DEBUG);
            LogManager.PrintLogMessage("RelayController", "DebugPushMsgModel", "push msg model -> callbackUrl: " + pushMsgModel.callbackUrl, DefineManager.LOG_LEVEL_DEBUG);
        }
    }
}