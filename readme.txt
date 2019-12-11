Auto Screen Capture by Gavin Kendall
Last updated on 2019-12-11 (December 11, 2019)
[The information presented here refers to the latest version of the application (which is currently 2.2.3.2)]
=============================================================================================================



Summary
-------
Auto Screen Capture is a small and portable screen capture utility for gamers, designers, and testers.

The application enables you to automatically take screenshots at a chosen interval. For example, you may
want to capture the progress of playing through a game's level or just use the application as a diary.

You can also schedule your automated screen capture sessions by specifying when a session starts and
when a session stops on particular days of the week.

A calendar is included to help you keep track of what days screenshots were taken.



Project Website
---------------
https://sourceforge.net/projects/autoscreen/
This is where to download the latest version of the autoscreen.exe binary
and create support tickets for any bugs encountered with the application.



Source Code
-----------
https://github.com/gavinkendall/autoscreen
This is where to view the source code, fork from the project, and submit pull requests.



Introduction to Modules
-----------------------
When you open Auto Screen Capture's interface you're going to see a number of tabs on the left side
which are called Modules:
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

(You can also use the "-limit" command line argument.)

This means that screenshots will continue to be taken until Auto Screen Capture reaches the 3rd cycle.

With an interval set for 30 seconds the application will ...
1) Wait for 30 seconds (which completes the 1st cycle)
2) Take screenshots
3) Wait for another 30 seconds (to complete the 2nd cycle)
4) Take screenshots
5) Wait for another 30 seconds (to complete the 3rd cycle)
6) Stop taking screenshots

With an interval set for 30 seconds and Initial Capture enabled the application will ...
1) Take screenshots
2) Wait for 30 seconds (which completes the 1st cycle)
3) Take screenshots
4) Wait for another 30 seconds (to complete the 2nd cycle)
5) Take screenshots
6) Wait for another 30 seconds (to complete the 3rd cycle)
7) Stop taking screenshots

This gives you three cycles. When you look at the Screenshots module you will see three sets of screenshots
taken with a 30 second interval in between each set.

Enabling the "Apply this label to each screenshot" option and entering a label in the text field will assign
the provided text to each screenshot taken. This is useful for when you want to filter your screenshots by
a particular label. A label can represent whatever you feel is necessary and important. For example, 
you could use a label to represent the name of a project you're currently working on. When you start working
on a new project you then change the label to represent the name of the new project.

Auto Screen Capture will keep track of what screenshots were taken during the time a label was applied.

As of version 2.2.3.1 a label can be selected from the "Apply Label" system tray icon menu.
This menu will not be available if the session is locked.



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
(Folders containing image files will not be deleted. This is intentional.)



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

The "Name" text box contains the name of the screen. For a new screen the Name field will
auto-populate with a default name based on the next available screen name that can be used.

The value of the Name field can be retrieved with the %name% macro tag so it could be used
as part of the screenshot's filename. Therefore any characters that are invalid to Windows
will be stripped out of the name (such as /, :, *, ?, \, <, >, and |).

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

The "Preview" image area shows you a preview of the selected Component. The preview refreshes
every 500 milliseconds.

The "Folder" text box refers to the directory in which screenshots will be written to.
You can use the backslash character (\) as part of your folder structure to define sub-folders.
An ending backslash character is recommended, but it will be included automatically upon save.

The "Macro" text box refers to the macro used that defines how each individual file is named.
Any characters invalid to Windows will be stripped out (such as /, :, *, ?, \, <, >, and |).
You cannot use the backslash character (\) as part of your macro.

The "Tags" drop-down control gives you a list of macro tags that can be used for your macro:
%name%           Name of screen or region                The "Name" of the screen
%screen%         Screen number (1, 2, 3, 4 ...)          The screen's associated number
%format%         Image format of screen or region        The format of the image used for the screen
%date%           Date                                    The current date as yyyy-MM-dd
%time%           Time                                    The current time as HH-mm-ss-fff
%year%           Year                                    The current year as yyyy
%month%          Month                                   The current month as MM
%day%            Day                                     The current day as dd
%hour%           Hour                                    The current hour as HH
%minute%         Minute                                  The current minute as mm
%second%         Second                                  The current second as ss
%millisecond%    Millisecond                             The current millisecond as fff
%count%          Number of screen capture cycles during the current screen capture session
%user%           User                                    The name of the logged in user
%machine%        Machine                                 The name of the machine being used
%title%          Title                                   The title of the active window

The "Remove Selected Screens" button is used to remove a selected number of screens in the list.
Select the screens you want to remove and then click the button to remove the selected screens.

The "..." button to the right of a screen name in the list of screens will open the Change Screen
window enabling you to change the settings of that screen.



Modules - Regions
-----------------
This module enables you to setup as many regions as you prefer.

The "Add New Region ..." button will show you, by default, a preview of a region at position 0,0
with the width set to 800 and the height set to 600.

The "Name" text box contains the name of the region. For a new region the Name field will
auto-populate with a default name based on the next available region name that can be used.

The value of the Name field can be retrieved with the %name% macro tag so it could be used
as part of the screenshot's filename. Therefore any characters that are invalid to Windows
will be stripped out of the name (such as /, :, *, ?, \, <, >, and |).

Change the X and Y values in the "Position" section to adjust the region's position.
X = 0 and Y = 0 usually represents the corner of the primary display. You can also use negative
values to adjust the position of the region beyond the boundaries of the available screens.

Change the Width and Height values in the "Size" section to adjust the region's size.

The "Image" section defines the image format, JPEG quality, and resolution ratio.

The "Include mouse pointer" option, when enabled, will include the mouse pointer in the image of the
screenshot that will be taken for that particular region.

You can import the X, Y, Width, and Height values from an available screen by selecting a
screen from the "Import Screen Dimensions" drop-down control.

The "Preview" image area shows you a preview of the specified region using the given X, Y, Width,
and Height values. The preview refreshes every 500 milliseconds.

The "Folder" text box refers to the directory in which screenshots will be written to.
You can use the backslash character (\) as part of your folder structure to define sub-folders.
An ending backslash character is recommended, but it will be included automatically upon save.

The "Macro" text box refers to the macro used that defines how each individual file is named.
Any characters invalid to Windows will be stripped out (such as /, :, *, ?, \, <, >, and |).
You cannot use the backslash character (\) as part of your macro.

The "Tags" drop-down control gives you a list of macro tags that can be used for your macro:
%name%           Name of screen or region                The "Name" of the screen
%screen%         Screen number (1, 2, 3, 4 ...)          The screen's associated number
%format%         Image format of screen or region        The format of the image used for the screen
%date%           Date                                    The current date as yyyy-MM-dd
%time%           Time                                    The current time as HH-mm-ss-fff
%year%           Year                                    The current year as yyyy
%month%          Month                                   The current month as MM
%day%            Day                                     The current day as dd
%hour%           Hour                                    The current hour as HH
%minute%         Minute                                  The current minute as mm
%second%         Second                                  The current second as ss
%millisecond%    Millisecond                             The current millisecond as fff
%count%          Number of screen capture cycles during the current screen capture session
%user%           User                                    The name of the logged in user
%machine%        Machine                                 The name of the machine being used
%title%          Title                                   The title of the active window

The "Remove Selected Regions" button is used to remove a selected number of regions in the list.
Select the regions you want to remove and then click the button to remove the selected regions.

The "..." button to the right of a region name in the list of regions will open the Change Region
window enabling you to change the settings of that region.



Modules - Editors
-----------------
This module enables you to setup your favourite image editors.

The "Add New Editor ..." button will show a window where you can specify the name, application,
and application arguments for the new image editor that you're adding to the list of editors.

The "Name" text box contains the name of the editor. You can name it however you want.

The "Application" text box contains the path where the application binary is located.
For example, "C:\Windows\System32\mspaint.exe" is the path to Microsoft Paint.

(Since version 2.2.1.1 you can also use batch scripts (*.bat) and PowerShell scripts (*.ps1)
as the editor. In fact, you can use any type of file for an editor.)

The "Arguments" text box contains the application's command line arguments that will be used
during the execution of the application. The %screenshot% tag represents the filepath of the
screenshot's image file. This could be the filepath of the screenshot that you're wanting to
edit via the "Edit" menu of the screenshot you're viewing from the Screenshots module or
the filepath of the last screenshot that was taken when a Trigger uses a specified Editor
to open the screenshot in the editor.

The "Remove Selected Editors" button is used to remove a selected number of editors in the list.
Select the editors you want to remove and then click the button to remove the selected editors.

The "..." button to the right of an editor name in the list of editors will open the Change Editor
window enabling you to change the settings of that editor.



Modules - Triggers
------------------
This module enables you to setup triggers.

A trigger performs a specified Action based on a specified Condition to control the behaviour
and workflow of Auto Screen Capture. For example, you could setup a trigger to open your
favourite image editor whenever a screenshot is taken.

The following conditions are available:
ApplicationStartup                    Perform an action when Auto Screen Capture starts
ApplicationExit                       Perform an action when Auto Screen Capture exits
InterfaceClosing                      Perform an action when the interface is closing
InterfaceHiding                       Perform an action when the interface is hiding
InterfaceShowing                      Perform an action when the interface is showing
LimitReached                          Perform an action when the Limit has been reached
ScreenCaptureStarted                  Perform an action when a screen capture session starts
ScreenCaptureStopped                  Perform an action when the running session is stopped
ScreenshotTaken                       Perform an action when a screenshot is taken

The following actions are available:
ExitApplication                       Quits Auto Screen Capture
HideInterface                         Hides the interface
RunEditor                             Runs (executes) a specified Editor
ShowInterface                         Shows the interface
StartScreenCapture                    Starts a screen capture session
StopScreenCapture                     Stops the currently running screen capture session
EmailScreenshot                       Uses the email settings in "application.xml" to email
                                      the last screenshot image that was captured

The following triggers are created by default on the first run of Auto Screen Capture:
Condition = ApplicationStartup -> Action = ShowInterface
Condition = ScreenCaptureStarted -> Action = HideInterface
Condition = InterfaceClosing -> Action = ExitApplication
Condition = LimitReached -> Action = StopScreenCapture



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

** Known Bug **
An issue with parsing command line arguments was accidentally introduced in version 2.2.1.0
whereby user settings were loaded after they were set by command line arguments. This bug was
fixed in version 2.2.3.1 which loads user settings before being set by command line arguments.



Configuration
-------------
Auto Screen Capture's configuration options are varied and very powerful. You can configure the
application to run in a single-user environment or in a multi-user environment.

By default, on the first run, Auto Screen Capture will create its own configuration file named
"autoscreen.conf" in the same directory as where the "autoscreen.exe" binary is executed from.

The default configuration file looks like this ...
=========================================== autoscreen.conf ===========================================
# Auto Screen Capture Configuration File
# Use this file to tell the application what folders and files it should utilize.
# Each key-value pair can be the name of a folder or file or a path to a folder or file.
# If only the folder name is given then it will be parsed as the sub-folder of the folder
# where the executed autoscreen.exe binary is located.


# This is the folder where screenshots will be stored by default.
ScreenshotsFolder=screenshots


# If any errors are encountered then you will find them in this folder when DebugMode is enabled.
DebugFolder=!autoscreen\debug


# Logs are stored in this folder when either Logging or DebugMode is enabled.
LogsFolder=!autoscreen\debug\logs


# The application settings (such as DebugMode).
ApplicationSettingsFile=!autoscreen\settings\application.xml


# Your personal settings.
UserSettingsFile=!autoscreen\settings\user.xml


# References to image editors.
EditorsFile=!autoscreen\editors.xml


# References to regions.
RegionsFile=!autoscreen\regions.xml


# References to screens.
ScreensFile=!autoscreen\screens.xml


# References to triggers.
TriggersFile=!autoscreen\triggers.xml


# References to screenshots.
ScreenshotsFile=!autoscreen\screenshots.xml
=======================================================================================================

As you can see the configuration file defines the folders and XML files the application should use.

You can create your own configuration file as long as it has, at a minimum, the following lines ...
ScreenshotsFolder=screenshots
DebugFolder=!autoscreen\debug
LogsFolder=!autoscreen\logs
ApplicationSettingsFile=!autoscreen\settings\application.xml
UserSettingsFile=!autoscreen\settings\user.xml
EditorsFile=!autoscreen\editors.xml
RegionsFile=!autoscreen\regions.xml
ScreensFile=!autoscreen\screens.xml
TriggersFile=!autoscreen\triggers.xml
ScreenshotsFile=!autoscreen\screenshots.xml

All of these values represent local paths for the computer that Auto Screen Capture is running on, but
it's also possible to use network paths instead.

By default the "!autoscreen" directory is created, and used, for storing XML files ...
application.xml               Setup settings for the application (such as Debug Mode and Email)
user.xml                      Setup settings for the user (such as Interval and Schedule)
editors.xml                   Setup the user's image editors to use when editing screenshots
regions.xml                   Setup regions to capture on the user's computer
screens.xml                   Setup screens to capture on the user's computer
triggers.xml                  Setup triggers to control the application's behavior
screenshots.xml               List the screenshots that have been captured by the application

You can use a network path (rather than a local system path). For example, if you have a server
named "SKYWALKER" and it's accessible by Auto Screen Capture running from a user's computer you
could have that user's settings file be stored on the server ...
UserSettingsFile=\\SKYWALKER\shared\autoscreen\gkendall-zim\gavin\user_settings.xml

You can use the %machine% and %user% tags for the value of a configuration key. For example, you could
specify the path for the user settings file to be used by any user on any machine on your network and
have each user configured to use the same settings stored on a server named "SKYWALKER" ...
UserSettingsFile=\\SKYWALKER\shared\autoscreen\%machine%\%user%\user_settings.xml

Comments can be made in the configuration file with the # symbol.
For example, you could write a comment for explaining the value of the "UserSettingsFile" key ...
# Each user can have their own settings file for the computer they're using
UserSettingsFile=\\SKYWALKER\shared\autoscreen\%machine%\%user%\user_settings.xml

You could have an "autoscreen.conf" file on each user's machine and it will read the XML files
from the server and store the user's screenshots on the server.

For example, each user's "autoscreen.conf" file could look like this ...
=========================================== autoscreen.conf ===========================================
ScreenshotsFolder=\\SKYWALKER\shared\autoscreen\%machine%\%user%\screenshots
DebugFolder=\\SKYWALKER\shared\autoscreen\%machine%\%user%\debug
LogsFolder=\\SKYWALKER\shared\autoscreen\%machine%\%user%\logs
ApplicationSettingsFile=\\SKYWALKER\shared\autoscreen\%machine%\%user%\settings\application.xml
UserSettingsFile=\\SKYWALKER\shared\autoscreen\%machine%\%user%\settings\user.xml
EditorsFile=\\SKYWALKER\shared\autoscreen\%machine%\%user%\editors.xml
RegionsFile=\\SKYWALKER\shared\autoscreen\%machine%\%user%\regions.xml
ScreensFile=\\SKYWALKER\shared\autoscreen\%machine%\%user%\screens.xml
TriggersFile=\\SKYWALKER\shared\autoscreen\%machine%\%user%\triggers.xml
ScreenshotsFile=\\SKYWALKER\shared\autoscreen\%machine%\%user%\screenshots.xml
=======================================================================================================

You don't need to use "autoscreen.conf" as the name for your configuration file.

If you have a configuration file that you want Auto Screen Capture to use when the application starts
you can specify the path and name of the configuration file with the "-config" command line argument.
For example, "-config=C:\MyAutoScreenCapture.conf" will start the application using the
config file named "MyAutoScreenCapture.conf" on the C:\ drive.