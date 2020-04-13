using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Foundation;

namespace XboxToastBackgroundProject.Helpers
{
    public static class SchedulingHelper
    {
        public static IAsyncAction ScheduleTimeTriggerTaskAsync()
        {
            return ScheduleTimeTriggerTaskHelperAsync(60 * 1440).AsAsyncAction();
        }
        public static IAsyncAction ScheduleTimeTriggerTaskAsync(uint minutesFromNow)
        {
            return ScheduleTimeTriggerTaskHelperAsync(minutesFromNow).AsAsyncAction();
        }

        private static async Task ScheduleTimeTriggerTaskHelperAsync(uint minutesFromNow)
        {
            await BackgroundExecutionManager.RequestAccessAsync();

            // Remove any existing scheduled time trigger
            var existing = BackgroundTaskRegistration.AllTasks.FirstOrDefault(i => i.Value.Name == "TimeTriggerNotification").Value;
            if (existing != null)
            {
                existing.Unregister(cancelTask: false);
            }

            TimeTrigger timeTrigger = new TimeTrigger(minutesFromNow, oneShot: true);
            var condition = new SystemCondition(SystemConditionType.UserPresent);
            BackgroundTasksHelper.RegisterBackgroundTask("XboxToastBackgroundProject.TimeTriggerTask", "TimeTriggerNotification", timeTrigger, condition);
        }
    }
}
