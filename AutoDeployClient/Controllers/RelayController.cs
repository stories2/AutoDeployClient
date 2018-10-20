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
            bool processResult = false;
            processResult = ExecuteOrder(pushMsgModel);
            return Json(new { success = processResult });
        }

        bool ExecuteOrder(PushMsgModel pushMsgModel)
        {
            ExecuteManager executeManager = new ExecuteManager();
            executeManager.pushMsgModel = pushMsgModel;
            return executeManager.Execute();
        }
    }
}
