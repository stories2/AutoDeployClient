using AutoDeployClient.Settings;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
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
                relativeSavePath = null;
            }
            return relativeSavePath;
        }

        public static String ExtractZipFile(String relativeFilePath)
        {
            String folderName = Guid.NewGuid().ToString();
            String extractPath = HttpContext.Current.Server.MapPath(DefineManager.DIR_EXTRACT_PATH) + folderName;
            String absoluteFileDownloadedPath = HttpContext.Current.Server.MapPath(relativeFilePath);
            try
            {
                if(!Directory.Exists(extractPath))
                {
                    Directory.CreateDirectory(extractPath);
                }
                using (ZipFile zipFile = ZipFile.Read(absoluteFileDownloadedPath))
                {
                    zipFile.ExtractAll(extractPath, ExtractExistingFileAction.DoNotOverwrite);
                }
                LogManager.PrintLogMessage("FileManager", "ExtractZipFile", "file extracted at: " + extractPath, DefineManager.LOG_LEVEL_DEBUG);
            }
            catch(Exception err)
            {
                LogManager.PrintLogMessage("FileManager", "ExtractZipFile", "cannot extract file: " + err.Message, DefineManager.LOG_LEVEL_ERROR);
                extractPath = null;
            }
            return extractPath;
        }
    }
}