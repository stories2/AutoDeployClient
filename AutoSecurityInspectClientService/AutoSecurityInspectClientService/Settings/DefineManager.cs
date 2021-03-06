﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSecurityInspectClientService.Settings
{
    class DefineManager
    {
        public const int
            LOG_LEVEL_VERBOSE = 0,
            LOG_LEVEL_INFO = 1,
            LOG_LEVEL_DEBUG = 2,
            LOG_LEVEL_WARN = 3,
            LOG_LEVEL_ERROR = 4,

            REFRESH_TIME_10_SEC = 10 * 1000,
            REFRESH_TIME_5_MIN = 5 * 60 * 1000,
            
            ORDER_TYPE_NORMAL_SECURITY_INSPECT = 3,

            STATUS_CODE_IDLE = 0,
            STATUS_CODE_RECEIVED_PUSH_MSG = 100,
            STATUS_CODE_DEPLOY_FILE_IS_READY = 202,
            STATUS_CODE_PUBLISHING = 300,
            STATUS_CODE_PUBLISH_DONE = 301,
            STATUS_CODE_PUBLISH_ERROR = 350,
            STATUS_CODE_SECURITY_INSPECTION = 400,
            STATUS_CODE_WRITE_REPORT_OF_SECURITY_INSPECTION = 401,
            STATUS_CODE_END_OF_SECURITY_INSPECT = 402,
            STATUS_CODE_SECURITY_INSPECTION_ERROR = 450,

            ZERO = 0;

        public static readonly String
            LOG_LEVEL_VERBOSE_STR = "V",
            LOG_LEVEL_INFO_STR = "I",
            LOG_LEVEL_DEBUG_STR = "D",
            LOG_LEVEL_WARN_STR = "W",
            LOG_LEVEL_ERROR_STR = "E",
            LOG_LEVEL_SUCCESS_STR = "S",
            LOG_LEVEL_FAIL_STR = "F",

            BUILD_VERSION = "1.0.0",
            
            EVENT_LOG_CATEGORY_APPLICATION = "Application",
            
            APPLICATION_NAME = "ASIC Service";
    }
}
