//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 템플릿에서 생성되었습니다.
//
//     이 파일을 수동으로 변경하면 응용 프로그램에서 예기치 않은 동작이 발생할 수 있습니다.
//     이 파일을 수동으로 변경하면 코드가 다시 생성될 때 변경 내용을 덮어씁니다.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutoDeployClient.Database
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
        public string ADC_CallbackUrl { get; set; }
        public string ADC_FileType { get; set; }
    }
}