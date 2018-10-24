using AutoDeployClientService.Settings;
using AutoDeployClientService.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AutoDeployClientService
{
    partial class MainService : ServiceBase
    {
        private Timer refreshTimer;

        public MainService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            LogManager.PrintLogMessage("MainService", "OnStart", "init", DefineManager.LOG_LEVEL_INFO);
            // TODO: 여기에 서비스를 시작하는 코드를 추가합니다.
            SetTimer();
        }

        private void SetTimer()
        {
            refreshTimer = new Timer();
            refreshTimer.Interval = DefineManager.REFRESH_TIME_10_SEC;
            refreshTimer.Elapsed += TimerCallback;
            refreshTimer.Start();

            LogManager.PrintLogMessage("MainService", "SetTimer", "timer started, it will refresh in every " + refreshTimer.Interval + " sec", DefineManager.LOG_LEVEL_DEBUG);
        }

        private void TimerCallback(object sender, ElapsedEventArgs eventObj)
        {
            LogManager.PrintLogMessage("MainService", "TimerCallback", "hello", DefineManager.LOG_LEVEL_INFO);
        }

        protected override void OnStop()
        {
            // TODO: 서비스를 중지하는 데 필요한 작업을 수행하는 코드를 여기에 추가합니다.
        }
    }
}