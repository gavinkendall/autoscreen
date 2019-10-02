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
-log
Enables logging.
All log files, by default, are stored in the "!autoscreen\debug\logs" folder (and this folder path is configurable as of version 2.2.1.0).
This command line argument is new for 2.2.1.0 so please don't expect it to work with older versions.

-debug
This is similar to the -log command line argument.
Logging will be turned on and log files will be stored in the logs folder. DebugMode will also be set to True.
Be very careful when enabling DebugMode (either by turning it on in the application settings file or by using this argument) because it's basically verbose logging.
In other words, if you have a lot of screenshots to load, then DebugMode is going to log every detail about each individual screenshot reference it finds.
I introduced this command line argument in 2.0.6, removed it in 2.2.0.7, and re-introduced it in 2.2.1.0 :)

-hideSystemTrayIcon
The application's system tray icon will be hidden while the application is running.

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

-config=filepath
Sets up various paths of the application's folders and files using a specified configuration file.
(where filepath is the path and name of the configuration file to use)
For example, "-config=C:\MyAutoScreenCapture.conf" will start the application using the config file named "MyAutoScreenCapture.conf" on the C:\ drive.

A configuration file that can be used by Auto Screen Capture should, at a minimum, contain the following 10 lines
representing key-value pairs that will be parsed by the application upon execution:
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

As you can see, each line specifies either the name of a folder or a file.
If only the folder name is given (such as "screenshots") then Auto Screen Capture will parse it as the "screenshots" folder in the same folder where the executed autoscreen.exe binary is found.
You can also specify a folder name with a trailing backslash but this isn't necessary.
If a file extension is found then Auto Screen Capture will parse the filename as an XML file.
You can tell Auto Screen Capture where to find each XML file by specifying absolute or relative (sub)folder paths.

As of version 2.2.2.3, if the -config command line argument is not provided and a configuration file named "autoscreen.conf" is not found in the same folder as autoscreen.exe then the application will attempt to write out its default "autoscreen.conf" file.
The "autoscreen.conf" file explains what each key-value pair represents.

Examples:
autoscreen.exe -interval=00:01:00.000
Starts the application, waits for 1 minute, and then starts taking screenshots for every minute until the application is stopped.

autoscreen.exe -interval=00:01:00.000 -initial -limit=10
Starts the application, takes initial screenshots, waits for 1 minute, takes the next set of screenshots, waits for 1 minute, takes screenshots etc. until the application is stopped or a limit of 10 "cycles" has been reached.

autoscreen.exe -interval=00:01:00.000 -initial -hideSystemTrayIcon
Starts the application, takes initial screenshots, waits for 1 minute, takes the next set of screenshots, waits for 1 minute, takes screenshots, etc. until the application is stopped. Also hides the system tray icon.

autoscreen.exe -interval=00:01:00.000 -initial -startat=13:30:00 -stopat=21:30:00
Starts the application's timer at 1:30pm, takes initial screenshots, waits for 1 minute, takes the next set of screenshots, waits for 1 minute, etc. until the application's timer stops at 9:30pm or the application is stopped by the user.