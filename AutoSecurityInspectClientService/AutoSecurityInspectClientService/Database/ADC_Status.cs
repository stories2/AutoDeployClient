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
    
    public partial class ADC_Status
    {
        public int ADC_Index { get; set; }
        public int ADC_ProcessStatus { get; set; }
        public string ADC_ProcessMsg { get; set; }
        public System.DateTime ADC_UpdateDateTime { get; set; }
    
        public virtual ADC_PushData ADC_PushData { get; set; }
        public virtual ADC_StatusCode ADC_StatusCode { get; set; }
    }
}