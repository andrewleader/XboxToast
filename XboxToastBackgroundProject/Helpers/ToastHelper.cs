using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace XboxToastBackgroundProject.Helpers
{
    public static class ToastHelper
    {
        /// <summary>
        /// Launches the Xbox app to the game pass
        /// </summary>
        private const string ProtocolUrl = "msgamepass:";

        public static string GetProtocolUrl()
        {
            return ProtocolUrl;
        }

        public static void ShowToast()
        {
            var content = new ToastContent()
            {
                Launch = ProtocolUrl,
                ActivationType = ToastActivationType.Protocol,
                Scenario = ToastScenario.Reminder,
                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = "Xbox Game Pass",
                                HintMaxLines = 1
                            },
                            new AdaptiveText()
                            {
                                Text = "Get the game pass!"
                            }
                        }
                    }
                },
                Actions = new ToastActionsCustom()
                {
                    Buttons =
                    {
                        new ToastButton("Try Game Pass", ProtocolUrl)
                        {
                            ActivationType = ToastActivationType.Protocol
                        },
                        new ToastButtonDismiss()
                    }
                }
            };

            var notif = new ToastNotification(content.GetXml())
            {
                Tag = "XboxPromotion"
            };

            ToastNotificationManager.CreateToastNotifier().Show(notif);
        }
    }
}
