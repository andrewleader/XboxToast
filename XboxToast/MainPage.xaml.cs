using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using XboxToastBackgroundProject.Helpers;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace XboxToast
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

#if DEBUG
            // When in debug we add the session connected test, so we can test behavior when user logs in, rather than having to test installing the OS image from scratch.
            RegisterDebugSessionConnectedTask();
#endif
        }

#if DEBUG
        private void RegisterDebugSessionConnectedTask()
        {
            // This task runs every time the user logs in, which then invokes the same code as the preinstall task would. Used only for testing purposes.
            var trigger = new SystemTrigger(SystemTriggerType.SessionConnected, oneShot: false);
            BackgroundTasksHelper.RegisterBackgroundTask("XboxToastBackgroundProject.PreinstallTask", "SessionConnectedTask", trigger, null);
        }
#endif

        private async void ButtonSchedule15Minutes_Click(object sender, RoutedEventArgs e)
        {
            await SchedulingHelper.ScheduleTimeTriggerTaskAsync(15);

            await new MessageDialog("Scheduled!").ShowAsync();
        }

        private async void ButtonSchedule24Hours_Click(object sender, RoutedEventArgs e)
        {
            await SchedulingHelper.ScheduleTimeTriggerTaskAsync();

            await new MessageDialog("Scheduled!").ShowAsync();
        }

        private void ButtonShowNow_Click(object sender, RoutedEventArgs e)
        {
            ToastHelper.ShowToast();
        }
    }
}
