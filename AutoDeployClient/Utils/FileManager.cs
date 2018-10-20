using AutoDeployClient.Settings;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public static bool MoveFolderToDest(String originFolder, String destFolder)
        {
            try
            {
                Directory.Move(originFolder, destFolder);
                LogManager.PrintLogMessage("FileManager", "MoveFolderToDest", "folder moved " + originFolder + " -> " + destFolder, DefineManager.LOG_LEVEL_DEBUG);
                return true;
            }
            catch(Exception err)
            {
                LogManager.PrintLogMessage("FileManager", "MoveFolderToDest", "cannot move folder: " + err.Message, DefineManager.LOG_LEVEL_ERROR);
                return false;
            }
        }

        public static bool CallSubProcess(String subProcessName, String arguments, String userName, String password)
        {
            try
            {
                String subProcessAbsolutePath = HttpContext.Current.Server.MapPath(subProcessName);
                var secureStr = new System.Security.SecureString();
                foreach(char secureChar in password)
                {
                    secureStr.AppendChar(secureChar);
                }
                LogManager.PrintLogMessage("FileManager", "CallSubProcess", "call sub process: " + subProcessName + " options: " + arguments, DefineManager.LOG_LEVEL_DEBUG);
                var psi = new ProcessStartInfo
                {
                    FileName = subProcessAbsolutePath,
                    UserName = userName,
                    Password = secureStr,
                    CreateNoWindow = true,
                    Arguments = arguments,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    //Verb = "runas",
                    UseShellExecute = false,
                    WorkingDirectory = Environment.CurrentDirectory,
                };
                Process process = new Process();
                process.StartInfo = psi;
                process.Start();
                process.WaitForExit();
                String output = process.StandardOutput.ReadToEnd();
                String errorOutput = process.StandardError.ReadToEnd();
                if(errorOutput != null)
                {
                    LogManager.PrintLogMessage("FileManager", "CallSubProcess", "error accepted: " + errorOutput, DefineManager.LOG_LEVEL_WARN);
                    throw new Exception();
                }
                LogManager.PrintLogMessage("FileManager", "CallSubProcess", "sub process done", DefineManager.LOG_LEVEL_INFO);
                return true;
            }
            catch(Exception err)
            {
                LogManager.PrintLogMessage("FileManager", "CallSubProcess", "something wrong with sub process: " + err.Message, DefineManager.LOG_LEVEL_ERROR);
            }
            return false;
        }
    }
}