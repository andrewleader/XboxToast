using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using XboxToastBackgroundProject.Helpers;

namespace XboxToastBackgroundProject
{
    public sealed class TimeTriggerTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            // This task runs once, 24 hours after the user completes OOBE, which is when we want to show the toast
            ToastHelper.ShowToast();
        }
    }
}
