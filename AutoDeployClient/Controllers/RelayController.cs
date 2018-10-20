using AutoDeployClient.Models;
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
            Debug.WriteLine("msg data: " + pushMsgModel.msg);
            return Json(new { success = true});
        }
    }
}
