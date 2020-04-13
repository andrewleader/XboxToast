using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;
using XboxToastBackgroundProject.Helpers;

namespace XboxToastBackgroundProject
{
    public sealed class PreinstallTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            // This task will only run once, after OOBE is completed and user is logged into the desktop.

            // Schedule the TimeTrigger task
            var deferral = taskInstance.GetDeferral();

            try
            {
#if DEBUG
                // When in debug, schedule for 15 minutes so we can see it work sooner
                await SchedulingHelper.ScheduleTimeTriggerTaskAsync(15);

                // Also show a debug notification so that we know it ran
                var content = new ToastContentBuilder().AddText("[Debug] PreinstallTask ran").AddText($"Xbox toast scheduled for {DateTime.Now.AddMinutes(15).ToShortTimeString()} (15 minutes from now)").GetToastContent();
                ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(content.GetXml()) { ExpirationTime = DateTime.Now.AddMinutes(16) });
#else
                // Otherwise it gets scheduled for 24 hours from now
                await SchedulingHelper.ScheduleTimeTriggerTaskAsync();

#if VERBOSE
                // Also show a debug notification so that we know it ran
                var content = new ToastContentBuilder().AddText("[Debug] PreinstallTask ran").AddText($"Xbox toast scheduled for {DateTime.Now.AddHours(24).ToShortTimeString()} (24 hours from now)").GetToastContent();
                ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(content.GetXml()) { ExpirationTime = DateTime.Now.AddHours(25) });
#endif
#endif
            }
            catch (Exception ex)
            {
#if VERBOSE
                var content = new ToastContentBuilder().AddText("[Debug] PreinstallTask exception occurred").AddText(ex.ToString(), hintWrap: true).GetToastContent();
                ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(content.GetXml()) { ExpirationTime = DateTime.Now.AddMinutes(5) });
#endif
            }

            deferral.Complete();
        }
    }
}
