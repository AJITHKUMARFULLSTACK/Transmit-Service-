using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceTest
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
            this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceInstaller1.DisplayName = "Tranzmit_Test";
            this.serviceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
        }

        protected override void OnAfterInstall(IDictionary savedState)
        {

        }
        private void serviceProcessInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {

        }
    }
}
