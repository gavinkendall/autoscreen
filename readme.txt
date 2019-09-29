Auto Screen Capture
Gavin Kendall
===================



Summary
-------
Auto Screen Capture is a small and portable automated screen capture utility for gamers, designers, and testers.

This application enables you to automatically take screenshots at a chosen interval.
For example, you may want to capture the progress of playing through a game's level or perhaps just use the application as a digital diary.

You can also schedule your automated screen capture sessions by specifying when a session starts and when a session ends on particular days of the week or every day.

A calendar is included to help you keep track of what days screenshots were taken.



Project Website
---------------
https://sourceforge.net/projects/autoscreen/
This is where to download the latest version of the autoscreen.exe binary, read blog posts on Auto Screen Capture, submit a review for the application,
and create a support ticket for any bugs encountered while using the application.



Source Code
-----------
https://github.com/gavinkendall/autoscreen
Please feel free to fork from the project and create pull requests.



Command Line Arguments
----------------------
-interval=hh:mm:ss.nnn
Sets the timer's interval to take a screenshot of each screen and region every hour (hh), minute (mm), second (ss), and millisecond (nnn).
For example, "-interval=02:30:10.000" sets the timer's interval to take screenshots every 2 hours, 30 minutes, and 10 seconds.

-initial
Takes a screenshot of each screen and region before starting the timer.

-startat=hh:mm:ss
Schedules the application to start taking screenshots at a specified time.

-stopat=hh:mm:ss
Schedules the application to stop taking screenshots at a specified time.

-limit=x
Stops taking screenshots when the specified limit has been reached where x is any number.
For example, "-limit=10" will take screenshots for 10 cycles and then stop after the 10th cycle.
Each cycle represents taking screenshots of every screen and region for a single tick of the timer.

-passphrase=x
Sets a word or a series of words to be used as the passphrase for challenging the user when the application is going to show its interface, stop taking screenshots, or exit ensuring that the application continues taking screenshots until the passphrase is entered.
This locks the screen capture session until the passphrase is successfully entered to unlock the session. The passphrase is stored as a SHA-512 hash.

-hideSystemTrayIcon
The application's system tray icon will be hidden while the application is running.

-config=filename_to_use
Sets up various paths of the application's folders and files using a specified configuration file.
(where filename_to_use is the path and name of the configuration file to use)
For example, "-config=C:\MyAutoScreenCapture.conf" will start the application using the config file
named "MyAutoScreenCapture.conf" on the C:\ drive.

A configuration file that can be used by Auto Screen Capture should, at a minimum, contain the following lines:
ScreenshotsFolder=screenshots
DebugFolder=!autoscreen\debug
LogsFolder=!autoscreen\debug\logs
ApplicationSettingsFile=application.xml
UserSettingsFile=user.xml
EditorsFile=!autoscreen\editors.xml
RegionsFile=!autoscreen\regions.xml
ScreensFile=!autoscreen\screens.xml
TriggersFile=!autoscreen\triggers.xml
ScreenshotsFile=!autoscreen\screenshots.xml

By default (as of version 2.2.1.0), if the -config command line argument is not provided and
a configuration file named "autoscreen.conf" is not found in the "!autoscreen" folder then the
application will attempt to write out its "autoscreen.conf" file in the "!autoscreen" folder.

Examples:
autoscreen.exe -interval=00:01:00.000
Starts the application, waits for 1 minute, and then starts taking screenshots for every minute until the application is stopped.

autoscreen.exe -interval=00:01:00.000 -initial -limit=10
Starts the application, takes initial screenshots, waits for 1 minute, takes the next set of screenshots, waits for 1 minute, takes screenshots etc. until the application is stopped or a limit of 10 "cycles" has been reached.

autoscreen.exe -interval=00:01:00.000 -initial -hideSystemTrayIcon
Starts the application, takes initial screenshots, waits for 1 minute, takes the next set of screenshots, waits for 1 minute, takes screenshots, etc. until the application is stopped. Also hides the system tray icon.

autoscreen.exe -interval=00:01:00.000 -initial -startat=13:30:00 -stopat=21:30:00
Starts the application's timer at 1:30pm, takes initial screenshots, waits for 1 minute, takes the next set of screenshots, waits for 1 minute, etc. until the application's timer stops at 9:30pm or the application is stopped by the user.