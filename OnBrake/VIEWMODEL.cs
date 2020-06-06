using MahApps.Metro.Controls.Dialogs;

namespace OnBrake
{
    internal class VIEWMODEL
    {
        private IDialogCoordinator instance;

        public VIEWMODEL(IDialogCoordinator instance)
        {
            this.instance = instance;
        }
    }
}