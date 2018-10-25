using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoDeployClient.Models
{
    public class PushMsgModel
    {
        public int orderType { get; set; }
        public String downloadUrl { get; set; }
        public String updateTargetPath { get; set; }
        public String msg { get; set; }
        public String version { get; set; }
        public String callbackUrl { get; set; }
        public String fileType { get; set; }
        public String pushRegisteredID { get; set; }
        //public String clientSideComputerUserName { get; set; }
        //public String clientSideComputerPassword { get; set; }
    }
}