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
                Scenario = ToastScenario.Reminder, // Reminder keeps the toast on screen until user dismisses it
                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        HeroImage = new ToastGenericHeroImage()
                        {
                            Source = "Assets/HeroImage.gif",
                            AlternateText = "A GIF of games that can be streamed in the Xbox app"
                        },

                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = "Check out the Xbox app",
                                HintMaxLines = 1
                            },
                            new AdaptiveText()
                            {
                                Text = "Discover and download new games, see what your friends are playing and chat with them across PC, mobile, and console"
                            },
                            new AdaptiveGroup()
                            {
                                Children =
                                {
                                    new AdaptiveSubgroup()
                                    {
                                        Children =
                                        {
                                            new AdaptiveText()
                                            {
                                                Text = "Get unlimited access to over 100 high-quality PC games with Xbox Game Pass. Join the fun in new games or catch up on a recent hit. There’s always something new to play.",
                                                HintWrap = true,
                                                HintStyle = AdaptiveTextStyle.CaptionSubtle
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                Actions = new ToastActionsCustom()
                {
                    Buttons =
                    {
                        new ToastButton("Launch the app", ProtocolUrl)
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
