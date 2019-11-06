using System.ServiceProcess;
using FSyncLib;

namespace FSyncService
{
    public partial class FSyncService : ServiceBase
    {
        private FSync _fSync;

        public FSyncService()
        {
            InitializeComponent();
            CanPauseAndContinue = true;
            _fSync = new FSync();
        }

        protected override void OnStart(string[] args)
        {
            _fSync.StartChecking();
        }

        protected override void OnStop()
        {
            _fSync.StopChecking();
        }

        protected override void OnPause()
        {
            _fSync.StopChecking();
        }

        protected override void OnContinue()
        {
            _fSync.StartChecking();
        }
    }
}
