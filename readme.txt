Auto Screen Capture by Gavin Kendall
Last updated on 2019-10-23 (October 23, 2019)
[The information presented here refers to the latest version of the application (which is currently 2.2.2.8)]
=============================================================================================================



Summary
-------
Auto Screen Capture is a small and portable screen capture utility for gamers, designers, and testers.

This application enables you to automatically take screenshots at a chosen interval. For example, you may
want to capture the progress of playing through a game's level or just use the application as a diary.

You can also schedule your automated screen capture sessions by specifying when a session starts and
when a session ends on particular days of the week or every day.

A calendar is included to help you keep track of what days screenshots were taken.



Project Website
---------------
https://sourceforge.net/projects/autoscreen/
This is where to download the latest version of the autoscreen.exe binary, read blog posts
on Auto Screen Capture, submit a review for the application, and create a support ticket for any bugs
encountered while using the application.



Source Code
-----------
https://github.com/gavinkendall/autoscreen
Please feel free to fork from the project and create pull requests.



Introduction to Modules
-----------------------
When you open Auto Screen Capture's interface you're going to see a number of tabs on the left side
which I like to call "modules":
Interval
Schedule
Screenshots
Security
Screens
Regions
Editors
Triggers



Modules - Interval
------------------
The Interval module sets the interval value for the timer that will be used when screenshots are being
taken during a screen capture session. You can set the number of hours, minutes, seconds, and milliseconds
that Auto Screen Capture should wait until it takes screenshots of your displays.

For example, an interval of 30 seconds will tell Auto Screen Capture to wait for 30 seconds,
take screenshots, wait for another 30 seconds, take screenshots, wait for another 30 seconds,
take screenshots, wait for another 30 seconds, take screenshots, etc. until the application
is told to stop or the application quits.

(You can also use the "-interval" command line argument to specify the timer's interval.)

"Initial Capture" takes an initial round of screenshots before starting the timer
(and then following the timer's set interval). For example, screenshots will be taken first,
then the timer is started for 30 seconds, and then after 30 seconds the screenshots will be taken again
until the application is told to stop or the application quits.

(You can also use the "-initial" command line argument to enable this option.)

Setting "limit" tells Auto Screen Capture to keep taking screenshots until a specified number of "cycles"
has been reached. For example, a limit of 3 will be three cycles.

This means that screenshots will continue to be taken until Auto Screen Capture reaches the 3rd cycle.
With an interval set for 30 seconds and Initial Capture enabled the application will ...
1) Take the first round of screenshots
2) Wait for 30 seconds (which completes the 1st cycle)
3) Take the next round of screenshots
4) Wait for 30 seconds (to complete the 2nd cycle)
5) Take the next round of screenshots
6) Wait for 30 seconds (to complete the 3rd cycle)
7) Stop taking screenshots

This gives you three cycles. When you look at the Screenshots module you will see three sets of screenshots
taken with a 30 second interval in between each set.

(You can also use the "-limit" command line argument.)

Enabling the "Apply this label to each screenshot" option and entering a label in the text field will assign
the provided text to each screenshot taken. This is useful for when you want to filter your screenshots by
a particular label. A label can represent whatever you feel is necessary and important. For example, 
you could use a label to represent the name of a project you're currently working on. When you start working
on a new project you then change the label to represent the name of the new project.
Auto Screen Capture will keep track of what screenshots were taken during the time a label was applied.



Modules - Schedule
------------------
You can tell Auto Screen Capture when to start and stop taking screenshots at particular times and on
defined days of the week by using the Schedule module.

"Start capture at" tells the application to start taking screenshots at a specified time.
(You can also use the "-startat" command line argument.)

"Stop capture at" tells the application to stop taking screenshots at a specified time.
(You can also use the "-stopat" command line argument.)

"Only on these days" tells the application to consider the specified times on defined days of the week:
Mo (Monday)
Tu (Tuesday)
We (Wednesday)
Th (Thursday)
Fr (Friday)
Sa (Saturday)
Su (Sunday)



Modules - Screenshots
---------------------
This module is a list of the screenshots that were taken on a particular day. You use the calendar to
choose a day and the list will be refreshed based on the chosen day.

Each entry in this list represents a set of screenshots that were taken for
each screen and/or region at the time the screenshots were taken.

Each entry in this list also displays the title of the active window at the time screenshots were taken.
Clicking on an entry will show you the screenshots for each screen and/or region.

The "Keep screenshots for X days" option tells Auto Screen Capture to keep all the image files it
knows about for a specified number of days.

For example, "Keep screenshots for 30 days" will keep the image files on disk for 30 days.
If any image files are found to be older than 30 days then those files will be automatically deleted.



Modules - Security
------------------
You can set a passphrase in order to lock the running screen capture session once the application starts
taking screenshots.

Enter a phrase that you can remember into the text field and then click on the Lock button.
You will see the following message:
"The passphrase you entered has been securely stored as a SHA-512 hash and your session has been locked."
The entered phrase will be cleared from the text field because your passphrase has now been hashed and
stored in the application's user settings file ("user.xml").

When the application is stopped, the interface is shown, or the application is exiting, the user will be
prompted with an "Enter Passphrase" window which has a text field and an Unlock button for the user to
enter the correct passphrase in order to "unlock" the running screen capture session.

If the user correctly enters the passphrase and unlocks the session the application will continue with
the action that the user initiated prior to receiving the prompt.

If the user incorrectly enters the passphrase and attempts to unlock the session the application will
continue to prompt for the passphrase until the prompt window is closed.

(You can also use the "-passphrase" command line argument.)



Modules - Screens
-----------------
This module enables you to setup as many screens as you prefer.

The "Add New Screen ..." button will show you a preview of the currently selected Component
(whether it be the Active Window or an available screen/display/monitor).

The Component drop down list shows you the Active Window and the available screens with their
associated dimensions.

For example, if you have three displays connected to your computer, then Component might show you ...
Active Window
Screen 1 (1920 x 1080)
Screen 2 (1920 x 1080)
Screen 3 (1680 x 1050)
... depending on what your screen setup is.

From the Image group of controls you can specify the format, quality, and resolution ratio of the image
that will be used when a screenshot is taken for that particular screen.

The following image formats are supported:
BMP
EMF
GIF
JPEG
PNG
TIFF
WMF

The "Include mouse pointer" option, when enabled, will include the mouse pointer in the image of the
screenshot that will be taken for that particular screen.



Command Line Arguments
----------------------
-log
Enables logging.
All log files, by default, are stored in the "!autoscreen\debug\logs" folder
(and this folder path is configurable as of version 2.2.1.0).

-debug
This is similar to the -log command line argument.
Logging will be turned on and log files will be stored in the logs folder.
DebugMode will also be set to True.
Be very careful when enabling DebugMode because it's basically verbose logging.
In other words, if you have a lot of screenshots to load, then DebugMode is going to log every detail
about each individual screenshot reference it finds.
I introduced this command line argument in 2.0.6, removed it in 2.2.0.7, and re-introduced it in 2.2.1.0 :)

-hideSystemTrayIcon
The application's system tray icon will be hidden while the application is running.

-interval=hh:mm:ss.nnn
Sets the timer's interval to take a screenshot of each screen and region
every hour (hh), minute (mm), second (ss), and millisecond (nnn).
For example, "-interval=02:30:10.000" sets the timer's interval to take screenshots
every 2 hours, 30 minutes, and 10 seconds.

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
Sets a word or a series of words to be used as the passphrase for challenging the user when the
application is going to show its interface, stop taking screenshots, or exit ensuring that the application
continues taking screenshots until the passphrase is entered.
This locks the screen capture session until the passphrase is successfully entered to unlock the session.
The passphrase is stored as a SHA-512 hash.

-config=filepath
Sets up various paths of the application's folders and files using a specified configuration file.
(where filepath is the path and name of the configuration file to use)
For example, "-config=C:\MyAutoScreenCapture.conf" will start the application using the
config file named "MyAutoScreenCapture.conf" on the C:\ drive.

A configuration file that can be used by Auto Screen Capture should, at a minimum,
contain the following 10 lines representing key-value pairs that will be parsed by the
application upon execution:
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
If only the folder name is given (such as "screenshots") then Auto Screen Capture will parse it
as the "screenshots" folder in the same folder where the executed autoscreen.exe binary is found.
You can also specify a folder name with a trailing backslash but this isn't necessary.
If a file extension is found then Auto Screen Capture will parse the filename as an XML file.
You can tell Auto Screen Capture where to find each XML file by specifying absolute or
relative (sub)folder paths.

As of version 2.2.2.3, if the -config command line argument is not provided and a configuration
file named "autoscreen.conf" is not found in the same folder as autoscreen.exe then the application
will attempt to write out its default "autoscreen.conf" file.
The "autoscreen.conf" file explains what each key-value pair represents.

Examples:
autoscreen.exe -interval=00:01:00.000
Starts the application, waits for 1 minute, and then starts taking screenshots for every minute
until the application is stopped.

autoscreen.exe -interval=00:01:00.000 -initial -limit=10
Starts the application, takes initial screenshots, waits for 1 minute, takes the next set of screenshots,
waits for 1 minute, takes screenshots etc. until the application is stopped or
a limit of 10 "cycles" has been reached.

autoscreen.exe -interval=00:01:00.000 -initial -hideSystemTrayIcon
Starts the application, takes initial screenshots, waits for 1 minute, takes the next set of screenshots,
waits for 1 minute, takes screenshots, etc. until the application is stopped.
Also hides the system tray icon.

autoscreen.exe -interval=00:01:00.000 -initial -startat=13:30:00 -stopat=21:30:00
Starts the application's timer at 1:30pm, takes initial screenshots, waits for 1 minute,
takes the next set of screenshots, waits for 1 minute, etc. until the application's timer
stops at 9:30pm or the application is stopped by the user.