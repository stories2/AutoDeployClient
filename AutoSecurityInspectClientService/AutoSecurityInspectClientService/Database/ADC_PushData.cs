//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutoSecurityInspectClientService.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class ADC_PushData
    {
        public int ADC_Index { get; set; }
        public Nullable<int> ADC_OrderType { get; set; }
        public string ADC_DownloadUrl { get; set; }
        public string ADC_DownloadedPath { get; set; }
        public string ADC_ExtractedPath { get; set; }
        public string ADC_UpdateTargetPath { get; set; }
        public string ADC_PushMsg { get; set; }
        public string ADC_Version { get; set; }
        public string ADC_CallbackUrl { get; set; }
        public string ADC_FileType { get; set; }
    
        public virtual ADC_Status ADC_Status { get; set; }
    }
}
