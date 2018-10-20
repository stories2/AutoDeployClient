using AutoDeployClient.Models;
using AutoDeployClient.Settings;
using AutoDeployClient.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutoDeployClient.Controllers
{
    public class RelayController : ApiController
    {
        [HttpPost]
        public IHttpActionResult PassPushMsg([FromBody] PushMsgModel pushMsgModel)
        {
            DebugPushMsgModel(pushMsgModel);
            return Json(new { success = true});
        }

        void DebugPushMsgModel(PushMsgModel pushMsgModel)
        {
            LogManager.PrintLogMessage("RelayController", "DebugPushMsgModel", "push msg model -> orderType: " + pushMsgModel.orderType, DefineManager.LOG_LEVEL_DEBUG);
            LogManager.PrintLogMessage("RelayController", "DebugPushMsgModel", "push msg model -> downloadUrl: " + pushMsgModel.downloadUrl, DefineManager.LOG_LEVEL_DEBUG);
            LogManager.PrintLogMessage("RelayController", "DebugPushMsgModel", "push msg model -> updateTargetPath: " + pushMsgModel.updateTargetPath, DefineManager.LOG_LEVEL_DEBUG);
            LogManager.PrintLogMessage("RelayController", "DebugPushMsgModel", "push msg model -> msg: " + pushMsgModel.msg, DefineManager.LOG_LEVEL_DEBUG);
            LogManager.PrintLogMessage("RelayController", "DebugPushMsgModel", "push msg model -> version: " + pushMsgModel.version, DefineManager.LOG_LEVEL_DEBUG);
            LogManager.PrintLogMessage("RelayController", "DebugPushMsgModel", "push msg model -> callbackUrl: " + pushMsgModel.callbackUrl, DefineManager.LOG_LEVEL_DEBUG);
        }
    }
}
