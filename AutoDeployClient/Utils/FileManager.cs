using AutoDeployClient.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace AutoDeployClient.Utils
{
    public class FileManager
    {
        public static String DownloadWebFile(String downloadFilePath, String saveFilePath)
        {
            String saveFileAs = null;
            try
            {
                saveFileAs = saveFilePath + Guid.NewGuid().ToString();
                using (var client = new WebClient())
                {
                    client.DownloadFile(downloadFilePath, saveFileAs);
                }
                LogManager.PrintLogMessage("FileManager", "DownloadWebFile", "file downloaded at: " + saveFileAs, DefineManager.LOG_LEVEL_DEBUG);
            }
            catch(Exception err)
            {
                LogManager.PrintLogMessage("FileManager", "DownloadWebFile", "cannot download file: " + err.Message, DefineManager.LOG_LEVEL_ERROR);
            }
            return saveFileAs;
        }
    }
}