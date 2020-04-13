# OemToast
OEM toast notification app for Xbox notification after first boot

## Some key points

* **Disabling the app list entry** - The app shouldn't appear in Start/apps list. To achieve that, in the `Package.appxmanifest`, we set `<uap:VisualElements AppListEntry="none"`.
* **Preinstall task** - In order to run code when the app is first installed (so that we can schedule notifications), we use the preinstall task. That's also specified in the manifest (`preInstalledConfigTask`).
* **Two separate apps** - The only way the app logo and app name of the toast can be configured is the app's manifest, we can't dynamically swap out the app logo/name. Therefore, we need to have a separate app for the Xbox toast so it can have an Xbox icon.
* **Square44x44Logo icon** - The Square44x44Logo in the Assets folder is the logo that's used in the left of the notification. But the `altform-unplated` versions (and lightunplated) are the ones that are actually used.
* **Handling app header in Action Center being clicked** - Users can click the top app header inside Action Center, which will launch the app no matter what. We then handle that app activation and launch Xbox. Users will see the app's splash screen and then the app will close after Xbox is launched, so not the perfect experience, but it should be a niche case anyone clicks on the header within Action Center.
* **Testing the preinstall task** - The only way to test the task is to have an OEM create an image with the app and set up the computer/VM from scratch.


## Testing without OEM image

When the app is compiled for **DEBUG**, it includes a SessionConnected background task, which runs every time the user signs in to the computer (note that the user has to be signed out first, not just computer locked). So...

1. Install both apps
1. Sign out of the computer
1. Sign back in
1. [EXPECTED] A debug notification should appear, stating that the notification has been scheduled
1. [EXPECTED] After 15 minutes, Xbox notification should appear. The Xbox notification, when compiled in debug mode, is set to show the notification after 15 minutes rather than after 24 hours, simply for ease of testing.


## Testing with OEM image

Compile the app for **RELEASE** so that the SessionConnected background task above isn't utilized. Include the app as part of the preinstalled apps in the OS image. Set up the new computer.

1. [EXPECTED] 24 hours after the user completes OOBE (or whenever they turn back on their computer if they had it off at that time), they should receive an Xbox notification