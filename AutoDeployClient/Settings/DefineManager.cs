using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoDeployClient.Settings
{
    public class DefineManager
    {
        public const int
            LOG_LEVEL_VERBOSE = 0,
            LOG_LEVEL_INFO = 1,
            LOG_LEVEL_DEBUG = 2,
            LOG_LEVEL_WARN = 3,
            LOG_LEVEL_ERROR = 4,
            
            EXECUTE_ORDER_AUTO_UPDATE = 1;

        public static readonly String
            LOG_LEVEL_VERBOSE_STR = "V",
            LOG_LEVEL_INFO_STR = "I",
            LOG_LEVEL_DEBUG_STR = "D",
            LOG_LEVEL_WARN_STR = "W",
            LOG_LEVEL_ERROR_STR = "E",
            
            DIR_UPDATE_PATH = "~/Content/Update/";
    }
}