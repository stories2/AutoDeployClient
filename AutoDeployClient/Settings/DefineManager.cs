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

            EXECUTE_ORDER_AUTO_UPDATE = 1,
            EXECUTE_ORDER_GET_STATUS = 2,

            NOT_AVAILABLE = -1,

            STATUS_CODE_RECEIVE_PUSH_MSG = 100,
            STATUS_CODE_DOWNLOADING_DEPLOY_FILE = 200,
            STATUS_CODE_EXTRACTING_DEPLOY_FILE = 201,
            STATUS_CODE_DEPLOY_FILE_IS_READY = 202,
            STATUS_CODE_DEFAULT = 0,
            STATUS_CODE_ERROR_WHILE_DOWNLOADING_DEPLOY_FILE = 250,
            STATUS_CODE_ERROR_WHILE_EXTRACTING_DEPLOY_FILE = 251;

        public static readonly String
            LOG_LEVEL_VERBOSE_STR = "V",
            LOG_LEVEL_INFO_STR = "I",
            LOG_LEVEL_DEBUG_STR = "D",
            LOG_LEVEL_WARN_STR = "W",
            LOG_LEVEL_ERROR_STR = "E",

            DIR_UPDATE_PATH = "~/Content/Update/",

            DIR_EXTRACT_PATH = "~/Content/Extract/",

            DIR_MOVE_FOLDER_BATCH_PATH = "~/Content/Executeable/CopyFiles.exe";
    }
}