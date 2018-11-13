using AutoSecurityInspectClientService.Settings;
using AutoSecurityInspectClientService.Utils;
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

namespace AutoSecurityInspectClientService
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
            this.ServiceName = DefineManager.APPLICATION_NAME;
            this.CanStop = true;
            this.CanPauseAndContinue = true;

            //Setup logging
            this.AutoLog = false;

            ((ISupportInitialize)this.EventLog).BeginInit();
            if (!EventLog.SourceExists(this.ServiceName))
            {
                EventLog.CreateEventSource(this.ServiceName, DefineManager.EVENT_LOG_CATEGORY_APPLICATION);
            }
            ((ISupportInitialize)this.EventLog).EndInit();

            this.EventLog.Source = this.ServiceName;
            this.EventLog.Log = DefineManager.EVENT_LOG_CATEGORY_APPLICATION;

            OnInit();
        }

        private void OnInit()
        {

            try
            {
                logManager = new LogManager();
                adcManager = new ADCManager();

                logManager.eventLog = this.EventLog;
                adcManager.logManager = logManager;

                adcManager.OpenDBTest();

                logManager.PrintLogMessage("MainService", "OnInit", "init ok, build ver: " + DefineManager.BUILD_VERSION, EventLogEntryType.SuccessAudit);
            }
            catch (Exception err)
            {
                logManager.PrintLogMessage("MainService", "OnInit", "init failed: " + err.Message, EventLogEntryType.Error);
            }
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
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
                
            }
            catch (Exception err)
            {
                logManager.PrintLogMessage("MainService", "TimerCallback", "some process has error: " + err.Message, EventLogEntryType.Error);
            }
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
