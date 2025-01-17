Documentation: https://github.com/TarasOsiris/android-goodies-docs-PRO/wiki
Support: support@ninevastudios.com
Ask us anything on our Discord server: https://discord.gg/SuJP9fY

---

### CHANGELOG

## v1.8.3

+ ADDED Functionality to pick multiple images from the gallery at once

---

## v1.8.2

+ FIXED Saving image to gallery not working on newest Android versions
+ IMPROVED Clean up source code a bit

---

## v1.8.1

+ FIXED Strange crashes when calling `getImportance` method.
+ FIXED Printing HTML functionality


---

## v1.8.0

+ FIXED File providers path now includes all the files. This fixes certain issues like opening PDF files.
+ IMPROVED Demo for video picker
+ ADDED `AGContext.cs` file to access files dir
+ REMOVED Google dependency manager from the package, `Dependencies.xml` files is still present.
+ FIXED Various picker bug fixes and improvements

---

## v1.7.1

+ ADDED Method to send SMS in background. See `AGShare.SendSmsSilently()`
+ IMPROVED `AGUIMisc.ShowStatusBar()` now accepts the color as a parameter
+ FIXED audio and file picking on Android Q

---

## v1.7.0

+ IMPROVED Plugin now uses Play Services Resolver plugin for Android dependencies resolution. Read more: https://github.com/googlesamples/unity-jar-resolver

---

## v1.6.4

+ FIXED Proguard configuration included with plugin aar files
+ FIXED Issue with picking video from camera
+ FIXED NPR when picking some files
+ FIXED Issue with `AGGPS.RequestLocationUpdates()` params validation
+ IMPROVED Write external storage is no longer required for sharing images
+ ADDED `AGPrintHelper` class for images and HTML pages printing

---

## v1.6.3

IMPORTANT! In this version the provider in AndroidManifest.xml that is required for image/camera/sharing etc. is now moved to library and is no longer required in your AndroidManifest.xml. So delete the <provider> from your AndroidManifest.xml.

WARNING! Minimal supported version of Unity Editor is now 2017.1.5f1.

---

## v1.6.2

+ IMPROVED Added runtime permission checks inside methods that require them
+ IMPROVED Picking images no longer requires WRITE_EXTERNAL_STORAGE_PERMISSION
+ IMPROVED Now notification params can be extracted even when app was brought to foreground
+ FIXED Picking images from Downloads folder on Android 8 or higher
+ FIXED scheduling multiple notifications issue with showing only the latest one

---

## v1.6.1

+ UPDATED The provider authority is not using ${applicationId} placeholder `AndroidManifest.xml`
+ ADDED the OpenPdf method to `AGApps.cs`
+ FIXED the behaviour of some sharing methods in `AGShare.cs`

---

## v1.6.0

+ ADDED Support for Launcher shortcuts (Check new `AGShortcutManager.cs`).
+ ADDED Method to show available WI-FI networks `AGNetwork.ShowAvailableWifiNetworks()`, methods to enable/disable WI-FI, connect/disconnect to known WI-FI networks.
+ ADDED methods for status bar hiding/showing to `AGUIMisc.cs`.
+ UPDATED `AGTelephony.cs` with new methods and properties.
+ UPDATED `AGBattery.cs` with new properties.
+ ADDED methods for hiding/showing of the application icon in the Launcher to `AGUtils.cs`
+ UPDATED `AGDateTimePicker.cs` with method for picking a date within a limited range
+ UPDATED `AGWallpaperManager.cs`, so it now supports lock screen management on devices with API level 24+
+ ADDED the possibility to work with image tags via ExifInterface (see `AGExifInterface.cs`)


---

## v1.5.0

+ ADDED Support for new vibration effects API!
+ ADDED Support for new notifications API (Check new `AGNotificationManager.cs`). 
    * Now create and manage channels and channel groups
    * Pass data along with notification and check if app was opened with notification
    * All possible UI options for notifications you can imagine, let me know if something is missing

+ ADDED Feature to check the SIM card state `AGTelephony.CurrentSimState`
+ ADDED method to check if user has camera app installed before picking photos
+ ADDED Altitude to the location object returned by GPS
+ UPDATED Android Support Library to version 27.1.1

---

## v1.4.1

This release introduces fixes and internal changes:

FIXED Notifications not working on Android O and above
FIXED Migration problem with Application.bundleIdentifier -> Application.identifier for older Unity versions
CHANGED Now minimum SDK version is 14 not 10!
CHANGED Replaced one support-v4 jar with modular new support libs (See Plugins/Android folder)
CHANGED Now all files picked images/photos/audio/camera etc. are stored under external storage app directory so these files will be ones that get deleted first when the device runs low on storage and there is no guarantee when these files will be deleted. Previously these files were stored directly in external storage public directory.

---

## v1.4.0

ADDED Fingerprint scanner functionality (See `AGFingerprintScanner.cs`)
FIXED Issue when references to demo scripts were sometimes lost in example scene
FIXED Tweeting not working due to Twitter renaming their activity all the time
FIXED Some dialogs not working properly when bytecode stripping is enabled

---

## v1.3.0

ADDED Method to make newly added image file to appear in the gallery 
ADDED Method to get Ethernet MAC address (useful for AndroidTV)
IMPROVED Added proguard configuration to avoid stripping plugin class when using proguard
FIXED Some methods crashing on older Android versions (`AGFileUtils.DataDir`)
FIXED Issue with dialogs dismissing

---

## v1.2.2

FIXED Cleaned up some files

---

## v1.2.1

ADDED Several methods to `AGFileUtils.cs` class to get app cache and other directories
ADDED `AGMediaRecorder.cs` class record audio files
ADDED `AGWallpaperManager.cs` class to set device wallpaper images
ADDED Method to copy text to system clipboard

---

## v1.2.0

IMPROVED When showing dialog in immersive mode the app stays in immersive mode
IMPROVED Adde more options for local notifications (color, small icon etc.)
IMPROVED Now local notifications can be scheduled in the future
ADDED Support for repeating local notifications
ADDED Picking audio files from device. See `AGFilePicker.cs`
ADDED Picking video files from device. See `AGFilePicker.cs`
ADDED Picking arbitrary files by specifying mime type. See `AGFilePicker.cs`
ADDED Recording video file from camera and receiving it. See `AGCamera.cs`

---

## v1.1.10

FIXED Incorrect orientation when receiving the taken photo
FIXED Sharing in Twitter not working after Twitter updated composer activity class
FIXED When saving screenshot image filename extension is added automatically now
IMPROVED Creating calendar event not working on some devices properly

---

## v1.1.9

BREAKING CHANGES: Photo and image pickers have now different method signatures, please check the examples

ADDED Function to send MMS
ADDED Functions to get/set system screen brightness
ADDED Functions to open 'Can modify system settings' system screen where user can allow app to modify system settings
CHANGED Completely reworked pickers to make them more reliable

---

## v1.1.8

ADDED Contact picker to pick contacts from address book
ADDED Options to use Intent.ACTION_OPEN_DOCUMENT for image picker
ADDED Method to load image into `Texture2D` provided with string Android URI
ADDED Method to open Facebook and Twitter user profile by profile ID
ADDED Time Picker - now can choose if to show 24h or 12h format
FIXED Image display name not being correct when picking image
FIXED Scaling issues when picking image from gallery

---

## v1.1.7

ADDED options to send text/image directly via Facebook Messenger, WhatsApp, Telegram, Viber, SnapChat
ADDED Feature to open instagram profile in the native app
ADDED Feature to uninstall the app by package
ADDED Feature to install the APK file from SD card
FIXED Minor bugs in the editor

---

## v1.1.6

FIXED not taking into consideration local time when creating calendar event

---

## v1.1.5

- ADDED support for native SharedPreferences (get, set, clear all, get all)
- ADDED Method to get installed packages on device `AGDeviceInfo.GetInstalledPackages`
- ADDED Method to share/upload video to YouTube/Facebook etc. `AGShare.ShareVideo`
- FIXED Flashlight not always working on latest android versions

---

## v1.1.4

- Reworked how saving image to gallery works to refresh gallery after saving
- Fixed how sending SMS works

---

## v1.1.3

- Added `AGCamera` class to take photos and receive result as `Texture2D`.
- Added `AGPermissions.RequestPermissions` method to request runtime permissions

**Breaking changes:**

- `ImagePickResult` and `ImageResultSize` classes are not inner any more. Change `AGGallery.ImagePickResult` and `AGGallery.ImageResultSize` to `ImagePickResult` and `ImageResultSize` respectively in your code.

---

## v1.1.2b HOTFIX RELEASE

---

- Fixed Android support lib v7 was packaged into `goodies-bridge-release.jar` causing conflicts if support lib v7 was already in project

---

## v1.1.2

- Added `Timestamp`, `HasSpeed`, `Speed`, `HasBearing`, `Bearing`, `IsFromMockProvider` properties to `Location` object when getting GPS updates.
- `AGGPS.DistanceBetween` and `Location.DistanceTo` to compute distance between locations
- `AGGPS.DeviceHasGPS` - convenience method to check if device has GPS module.

---

## v1.1.1

- Added cc and bcc recipients when sending email.
- Added `<uses-feature android:name="android.hardware.location.gps" android:required="false" />` to manifest and added GPS class reference docs.
- Added ability to manage [runtime permissions](https://developer.android.com/training/permissions/requesting.html).

---

## v1.1.0

- Added method to launch any installed app on device by providing package (`AGApps.OpenOtherAppOnDevice()`)
- Ability to pick photo from gallery and receive it as `Texture2D` (`AGGallery.PickImageFromGallery()`)

---

## v1.0.0 Initial PRO Version Release

- Create new note
- Sharing image (Texture2D)
- Sharing Screenshot
- Saving image to gallery
- Added progress bar and spinner loading dialogs
- Added theme choice for dialogs (Light/Dark)
- Check battery charge level
- Get application package
- Create calendar event with info
- Open calendar at date
- Get external storage directory and all almost all [`android.os.Environment`](https://developer.android.com/reference/android/os/Environment.html) methods.
  - Get standard directories (data dir, download cache dir, external storage dir etc.)
  - Get external storage state
  - Check if external storage removable
  - Check if external storage emulated
  - Etc. check `AndroidEnvironment.cs` API for full info
- Open settings or any required section of settings
- Open alarms/Set alarm.
- Set timer

## v0.1

+ Initial Release

Features:

- Different dialogs (message, radiobuttons, checkboxes)
- Time and Date picker
- Show Toast
- Open Map locations/addresses
- Native share text/tweet/send email/sms
- Checking if app is installed
- Enable immersive mode
- Get device info (model, manufacturer, android version etc.)

---
