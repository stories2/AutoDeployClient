using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace AutoSecurityInspectClientService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();

            serviceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            //Installers.Add(serviceInstaller1);

            this.AfterInstall += new InstallEventHandler(ServiceInstallerAfterInstall);
        }

        private void ServiceInstallerAfterInstall(object sender, InstallEventArgs eventData)
        {

            using (ServiceController serviceController = new ServiceController(serviceInstaller1.ServiceName))
            {
                serviceController.Start();
            }
        }
    }
}
