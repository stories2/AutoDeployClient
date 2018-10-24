using AutoDeployClient.Database;
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

        private bool ExecuteOrder(PushMsgModel pushMsgModel)
        {
            ADC_Status adcStatus = new ADC_Status();
            ADCManager adcManager = new ADCManager();
            adcStatus.ADC_Index = adcManager.CreateNewProcess(pushMsgModel);
            adcStatus.ADC_ProcessStatus = DefineManager.STATUS_CODE_RECEIVE_PUSH_MSG;

            ExecuteManager executeManager = new ExecuteManager();
            executeManager.pushMsgModel = pushMsgModel;
            executeManager.adcManager = adcManager;
            executeManager.adcStatus = adcStatus;

            adcManager.UpdateCurrentProcessStatus(adcStatus);

            return executeManager.Execute();
        }
    }
}