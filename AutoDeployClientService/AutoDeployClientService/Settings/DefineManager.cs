using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDeployClientService.Settings
{
    internal class DefineManager
    {
        public const int
            LOG_LEVEL_VERBOSE = 0,
            LOG_LEVEL_INFO = 1,
            LOG_LEVEL_DEBUG = 2,
            LOG_LEVEL_WARN = 3,
            LOG_LEVEL_ERROR = 4,

            REFRESH_TIME_10_SEC = 10 * 1000,
            REFRESH_TIME_5_MIN = 5 * 60 * 1000,

            ORDER_TYPE_NORMAL_DEPLOY = 1,

            STATUS_CODE_DEPLOY_FILE_IS_READY = 202,

            ZERO = 0;

        public static readonly String
            LOG_LEVEL_VERBOSE_STR = "V",
            LOG_LEVEL_INFO_STR = "I",
            LOG_LEVEL_DEBUG_STR = "D",
            LOG_LEVEL_WARN_STR = "W",
            LOG_LEVEL_ERROR_STR = "E",
            LOG_LEVEL_SUCCESS_STR = "S",
            LOG_LEVEL_FAIL_STR = "F";
    }
}