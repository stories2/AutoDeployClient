using AutoDeployClient.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace AutoDeployClient.Utils
{
    public class WebManager
    {
        public String PostDataToUrl(String url, String json)
        {
            String result = null;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            LogManager.PrintLogMessage("WebManager", "PostDataToUrl", "send to -> " + url + " data: " + json, DefineManager.LOG_LEVEL_DEBUG);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            LogManager.PrintLogMessage("WebManager", "PostDataToUrl", "response: " + result, DefineManager.LOG_LEVEL_DEBUG);

            return result;
        }
    }
}