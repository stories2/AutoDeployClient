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
                        break;
                    default:
                        success = false;
                        break;
                }
            }
            catch(Exception err)
            {
                LogManager.PrintLogMessage("ExecuteManager", "Execute", "cannot execute order: " + err.Message);
            }

            return success;
        }

        void RutineAutoUpdate()
        {
            String downloadedFilePath = FileManager.DownloadWebFile(pushMsgModel.downloadUrl, DefineManager.DIR_UPDATE_PATH, pushMsgModel.fileType);
            String extractedFilePath = null;
            if(downloadedFilePath != null)
            {
                extractedFilePath = FileManager.ExtractZipFile(downloadedFilePath);
                if(extractedFilePath != null)
                {
                    
                }
                else
                {
                    LogManager.PrintLogMessage("ExecuteManager", "RutineAutoUpdate", "extract file failed", DefineManager.LOG_LEVEL_WARN);
                    throw new Exception();
                }
            }
            else
            {
                LogManager.PrintLogMessage("ExecuteManager", "RutineAutoUpdate", "download file failed", DefineManager.LOG_LEVEL_WARN);
                throw new Exception();
            }
        }

        void DebugPushMsgModel(PushMsgModel pushMsgModel)
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