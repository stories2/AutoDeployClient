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
        public static String DownloadWebFile(String downloadFilePath, String saveFilePath, String fileType)
        {
            String absoluteSavePath = HttpContext.Current.Server.MapPath(saveFilePath);
            String fileName = Guid.NewGuid().ToString() + "." + fileType;
            String relativeSavePath = saveFilePath;
            try
            {
                absoluteSavePath = absoluteSavePath + fileName;
                relativeSavePath = relativeSavePath + fileName;
                using (var client = new WebClient())
                {
                    client.DownloadFile(downloadFilePath, absoluteSavePath);
                }
                LogManager.PrintLogMessage("FileManager", "DownloadWebFile", "file downloaded at: " + relativeSavePath, DefineManager.LOG_LEVEL_DEBUG);
            }
            catch(Exception err)
            {
                LogManager.PrintLogMessage("FileManager", "DownloadWebFile", "cannot download file: " + err.Message, DefineManager.LOG_LEVEL_ERROR);
            }
            return relativeSavePath;
        }
    }
}