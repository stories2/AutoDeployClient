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
        private LogManager logManager;
        private ADCManager adcManager;

        public MainService()
        {
            InitializeComponent();

            //Setup Service
            this.ServiceName = "ADC Service";
            this.CanStop = true;
            this.CanPauseAndContinue = true;

            //Setup logging
            this.AutoLog = false;

            ((ISupportInitialize)this.EventLog).BeginInit();
            if (!EventLog.SourceExists(this.ServiceName))
            {
                EventLog.CreateEventSource(this.ServiceName, "Application");
            }
            ((ISupportInitialize)this.EventLog).EndInit();

            this.EventLog.Source = this.ServiceName;
            this.EventLog.Log = "Application";

            OnInit();
        }

        private void OnInit()
        {
            logManager = new LogManager();
            adcManager = new ADCManager();

            logManager.eventLog = this.EventLog;
            adcManager.logManager = logManager;
        }

        protected override void OnStart(string[] args)
        {
            logManager.PrintLogMessage("MainService", "OnStart", "init", EventLogEntryType.Information);
            // TODO: 여기에 서비스를 시작하는 코드를 추가합니다.
            SetTimer();
        }

        private void SetTimer()
        {
            refreshTimer = new Timer();
            refreshTimer.Interval = DefineManager.REFRESH_TIME_5_MIN;
            refreshTimer.Elapsed += TimerCallback;
            refreshTimer.Start();

            logManager.PrintLogMessage("MainService", "SetTimer", "timer started, it will refresh in every " + refreshTimer.Interval + " mil", EventLogEntryType.Information);
        }

        private void TimerCallback(object sender, ElapsedEventArgs eventObj)
        {
            //logManager.PrintLogMessage("MainService", "TimerCallback", "hello", EventLogEntryType.Information);
            try
            {
                RunAutoDeployRoutine();
            }
            catch(Exception err)
            {
                logManager.PrintLogMessage("MainService", "TimerCallback", "some process has error: " + err.Message, EventLogEntryType.Error);
            }
        }

        private void RunAutoDeployRoutine()
        {
            AutoDeployRoutineManager autoDeployRoutineManager = new AutoDeployRoutineManager();
            autoDeployRoutineManager.adcManager = adcManager;
            autoDeployRoutineManager.logManager = logManager;
            autoDeployRoutineManager.StartRoutine();
        }

        protected override void OnStop()
        {
            // TODO: 서비스를 중지하는 데 필요한 작업을 수행하는 코드를 여기에 추가합니다.
        }
    }
}