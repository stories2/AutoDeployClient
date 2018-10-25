using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoDeployClient.Models.ADCManager
{
    public class ADCStatusReportModel
    {
        public int adcIndex { get; set; }
        public int? adcOrderType { get; set; }
        public String adcDownloadUrl { get; set; }
        public String adcDownloadedPath { get; set; }
        public String adcExtractedPath { get; set; }
        public String adcUpdateTargetPath { get; set; }
        public String adcPushMsg { get; set; }
        public String adcVersion { get; set; }
        public String adcCallbackUrl { get; set; }
        public String adcFileType { get; set; }
        public int adcProcessStatus { get; set; }
        public String adcProcessMsg { get; set; }
        public DateTime adcUpdateDateTime { get; set; }
    }
}