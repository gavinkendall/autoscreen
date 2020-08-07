Auto Screen Capture by Gavin Kendall
Last updated on 2020-08-07 (August 7, 2020)
[The information presented here refers to the latest version of the application (which is currently 2.3.1.8)]
=============================================================================================================



Summary
-------
Auto Screen Capture is a small and portable screen capture utility for gamers, designers, and testers.

The application enables you to automatically take screenshots at a chosen interval. For example, you may
want to capture the progress of playing through a game's level or track your progress on a long project.

You can also schedule your automated screen capture sessions by specifying when a session starts and
when a session stops on particular days of the week.

A calendar is included to help you keep track of what days screenshots were taken.



License
-------
This application comes under version 3 of the GNU General Public License (GPLv3).
You can read the license in full at https://www.gnu.org/licenses/gpl-3.0.en.html



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
Setup
Screenshots
Screens
Regions
Editors
Schedules
Tags
Triggers



Modules - Setup
---------------
The Setup module is divided into a few sections.

*Interval*
This section sets the interval value for the timer that will be used when screenshots are being
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

*Active Window Title*
This is where to specify the text to compare against the active window title so that the application only
takes screenshots if the active window title contains the defined text.

*Region Select / Auto Save*
This section defines the path of the folder and the filename pattern ("macro") for when
you right-click on the system tray icon and click on "Region Select / Auto Save" to
automatically save the captured region of the screen to an image file. By default this
saves a file to the user's desktop - much like doing Command+Shift+4 on a Mac.

*Security*
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

Old screenshots will be deleted every five minutes. New screenshots will be saved every five minutes.
The calendar will also update every five minutes (as of version 2.3.0.0).



Modules - Screens
-----------------
This module enables you to setup as many screens as you prefer.

The green button with the white plus will show you a preview of the currently selected Component
(whether it be the Active Window or an available screen/display/monitor).

The "Name" text field contains the name of the screen. For a new screen the Name field will
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

The "Preview" image area shows you a preview of the selected Component.

The "Folder" text field refers to the directory in which screenshots will be written to.
You can use the backslash character (\) as part of your folder structure to define sub-folders.
An ending backslash character is recommended, but it will be included automatically upon save.

The "Macro" text field refers to the macro used that defines how each individual file is named.
Any characters invalid to Windows will be stripped out (such as /, :, *, ?, <, >, and |).
You can use the backslash character (\) as part of your macro to define sub-folders.

The button with the red cross is used to remove a selected number of screens in the list.
Select the screens you want to remove and then click the button to remove the selected screens.

The button with the cog will open the Change Screen window enabling you to change properties.



Modules - Regions
-----------------
This module enables you to setup as many regions as you prefer.

The green button with the white plus will show you, by default, a preview of a region at position 0,0
with the width set to 800 and the height set to 600.

The "Name" text field contains the name of the region. For a new region the Name field will
auto-populate with a default name based on the next available region name that can be used.

The value of the Name field can be retrieved with the %name% macro tag so it could be used
as part of the screenshot's filename. Therefore any characters that are invalid to Windows
will be stripped out of the name (such as /, :, *, ?, \, <, >, and |).

The "Region Select" button is used to acquire the X, Y, Width, and Height values by
using a mouse-driven region selection on the screen.

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
and Height values.

The "Folder" text field refers to the directory in which screenshots will be written to.
You can use the backslash character (\) as part of your folder structure to define sub-folders.
An ending backslash character is recommended, but it will be included automatically upon save.

The "Macro" text field refers to the macro used that defines how each individual file is named.
Any characters invalid to Windows will be stripped out (such as /, :, *, ?, <, >, and |).
You can use the backslash character (\) as part of your macro to define sub-folders.

The button with the red cross is used to remove a selected number of regions in the list.
Select the regions you want to remove and then click the button to remove the selected regions.

The button with the cog will open the Change Region window enabling you to change properties.



Modules - Editors
-----------------
This module enables you to setup your favourite image editors.

The green button with the white plus will show a window where you can specify the name, application,
and application arguments for the new image editor that you're adding to the list of editors.

The "Name" text field contains the name of the editor. You can name it however you want.

The "Application" text field contains the path where the application binary is located.
For example, "C:\Windows\System32\mspaint.exe" is the path to Microsoft Paint.

(Since version 2.2.1.1 you can also use batch scripts (*.bat) and PowerShell scripts (*.ps1)
as the editor. In fact, you can use any type of file for an editor.)

The "Arguments" text field contains the application's command line arguments that will be used
during the execution of the application. The %filepath% tag represents the filepath of the
screenshot's image file. This could be the filepath of the screenshot that you're wanting to
edit via the "Edit" menu of the screenshot you're viewing from the Screenshots module or
the filepath of the last screenshot that was taken when a Trigger uses a specified Editor
to open the screenshot in the editor.

The "Default" checkbox sets the editor as the default editor to be used when you select
"Capture Now -> Edit" from the system tray icon's menu.

The button with the red cross button is used to remove a selected number of editors in the list.
Select the editors you want to remove and then click the button to remove the selected editors.

The button with the cog will open the Change Editor window enabling you to change properties.



Modules - Tags
--------------
This module enables you to setup macro tags.

A tag is a special value, surrounded by the percentage character (%), which gives you the ability
to acquire certain information that can be used in your macro for the filename pattern.

The green button with the white plus will display a dialog box in order to create a new tag.

The "Name" text field is for the tag's name. This is a required field, but make sure to include
the percentage character ("%") at the beginning and the end of the tag name.

The "Type" drop-down control defines the type of tag being used. There are 10 tag types:
Screen Name                       The "Name" of the screen or region
Screen Number                     The screen's associated number (such as 1, 2, 3, 4)
Image Format                      The format of the image used for the screen
Screen Capture Cycle Count        Number of screen capture cycles during the current screen capture session
Active Window Title               The title of the active window
Date/Time Format                  A value representing a date/time format (such as "yyyy-MM-dd HH-mm-ss-fff")
User                              The name of the logged in user
Machine                           The name of the machine being used
Time of Day                       A specified value based on the time of day
Date/Time Format Expression       A value representing a date/time tag expression (such as "{day-1}")

(The Date/Time Format Expression type is simply the new name for the Date/Time Format Function type previously
used by Auto Screen Capture. The functionality of this type remains the same as before.)

If you select either the Date/Time Format or Date/Time Format Expression type then the Date/Time Format Value
text field will be available to enter a value. This value can be a date/time format (such as "HH-mm-ss")
to represent the current date/time as a defined pattern or a date/time tag expression (such as "{month-1}")
which represents the current date/time modified by an operator and an applied amount of time.

If you select the Time of Day type then the Time of Day group of controls will be available. This includes
three sets of controls that enable you to specify the start time, end time, and value of three time ranges
for what you want Auto Screen Capture to consider as the morning, afternoon, and evening. The value can be
text and/or a series of macro tags. If you specify the evening end time to be beyond 23:59:59 then please
enable the "Evening extends to next morning" option so that the evening value continues to be used for
the following morning. For example, an Evening value between 21:00:00 and 03:00:00 can be used when you
want to dynamically change the filename between 9pm and 3am the next morning.

Macro tags available by default are ...
%name%           Screen Name
%screen%         Screen Number
%format%         Image Format
%date%           Date/Time Format               Current date as "yyyy-MM-dd"
%time%           Date/Time Format               Current time as "HH-mm-ss-fff"
%year%           Date/Time Format               Current year as "yyyy"
%month%          Date/Time Format               Current month as "MM"
%day%            Date/Time Format               Current day as "dd"
%hour%           Date/Time Format               Current hour as "HH"
%minute%         Date/Time Format               Current minute as "mm"
%second%         Date/Time Format               Current second as "ss"
%millisecond%    Date/Time Format               Current millisecond as "fff"
%lastyear%       Date/Time Format Expression    Current year minus 1 with expression "{year-1}"
%lastmonth%      Date/Time Format Expression    Current month minus 1 with expression "{month-1}"
%yesterday%      Date/Time Format Expression    Current day minus 1 with expression "{day-1}"
%tomorrow%       Date/Time Format Expression    Current day plus 1 with expression "{day+1}"
%6hoursbehind%   Date/Time Format Expression    Current hour minus 6 with expression "{hour-6}"
%6hoursahead%    Date/Time Format Expression    Current hour plus 6 with expression "{hour+6}"
%count%          Screen Capture Cycle Count
%user%           User
%machine%        Machine
%title%          Active Window Title
%timeofday%      Time of Day

You can add, edit, or remove tags. Each tag needs a tag name and a tag type.

You may also need to specify the date/time format value and/or the time of day values
based on the chosen tag type.

So, typically, any date/time value would be associated with the Date/Time Format type.
This means that you would need to specify a date/time format value which can include ...
fff            for milliseconds
ss             for seconds
mm             for minutes
HH             for hours
dd             for days
MM             for months
yyyy           for years
For example, a date/time format value of "yyyy-MM-dd HH-mm-ss" would be translated
by your Date/Time Format tag as the current year, month, day, hour, minute, and second.

A tag using the "Time of Day" tag type would need to have ...
- Morning start time, morning end time, and morning value
- Afternoon start time, afternoon end time, and afternoon value
- Evening start time, evening end time, and evening value
... so that (depending on the time of day) your tag's value would be replaced by
the value defined for morning, afternoon, or evening given the associated time range.

You can use tags in the morning, afternoon, and evening fields of a "Time of Day" tag.
For example, you could have %hour%, %minute%, and %second% tags in any of the fields
if you need to customize your filename pattern even further. A "Time of Day" tag can
also be called by another "Time of Day" tag for creating an interesting chain of tags
that respond to particular times of the day. For example, you can create a "Time of Day"
tag which returns the value of %day% before midnight and %yesterday% after midnight
and then create a second "Time of Day" tag that calls the first "Time of Day" tag
between the hours of late evening and the early hours of the next morning.

A special type of date/time format tag called a date/time format expression tag
(or just "date/time tag expression") can be used to define an applied amount of time
either behind or ahead the current date/time.

A date/time expression tag is specified by a date/time part
(year, month, day, hour, minute, or second), an operator (either "-" or "+"),
and the amount of time applied.
For example ...
{year-1}       for last year
{day-1}        for yesterday
{day+1}        for tomorrow
{month-2}      for 2 months ago
{hour+6}       for 6 hours ahead

As of version 2.3.0.0, Date/Time Format Function was renamed to Date/Time Format Expression
to better define its purpose.



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
Date/Time                             Perform an action when a defined date and time has been met
Time                                  Perform an action when a defined time has been met on a daily basis

The following actions are available:
ExitApplication                       Quits Auto Screen Capture
HideInterface                         Hides the interface
RunEditor                             Runs (executes) a specified Editor
ShowInterface                         Shows the interface
StartScreenCapture                    Starts a screen capture session
StopScreenCapture                     Stops the currently running screen capture session
EmailScreenshot                       Uses the email settings in "application.xml" to email
                                      the last screenshot image that was captured
Set Screen Capture Interval           Sets (or changes) the screen capture interval to the defined interval

The following triggers are created by default on the first run of Auto Screen Capture:
Condition = ApplicationStartup -> Action = ShowInterface
Condition = ScreenCaptureStarted -> Action = HideInterface
Condition = ScreenCaptureStopped -> Action = ShowInterface
Condition = InterfaceClosing -> Action = HideInterface
Condition = LimitReached -> Action = StopScreenCapture



Command Line Arguments
----------------------
You can run the Auto Screen Capture "autoscreen.exe" binary with command line arguments.
Simply running "autoscreen.exe" without arguments will open Auto Screen Capture and not
necessarily start a screen capture session unless a trigger is setup to do so, the -start
command has been given, or the AutoStartFromCommandLine setting is enabled.
("AutoStartFromCommandLine" will be enabled if you're upgrading from a version that's
older than version 2.3.0.0 to emulate the old behaviour of running from the command line)

As of version 2.3.0.0 most commands can be given to Auto Screen Capture while it's running.
This gives you the opportunity to control a running instance of the application.

-start
Starts the timer and begins running a screen capture session.
("AutoStartFromCommandLine" will be ignored)

-stop
Stops the timer and stops the currently running screen capture session.
("AutoStartFromCommandLine" will be ignored)

-exit
Quits Auto Screen Capture.

-capture
Takes screenshots in a single capture cycle at the time the command is given.

-interval=hh:mm:ss.nnn
Sets the timer's interval to take a screenshot of each screen and region
every hour (hh), minute (mm), second (ss), and millisecond (nnn).
For example, "-interval=02:30:10.000" sets the timer's interval to take screenshots
every 2 hours, 30 minutes, and 10 seconds.

-log
Toggles logging. As of version 2.3.0.0 this command toggles logging on and off rather than
simply enabling (or turning on) logging so be aware how frequently you use this command.
For example, if logging is currently off then using "-log" will turn logging on and using "-log"
again will turn it off. All log files, by default, are stored in the "!autoscreen\debug\logs" folder
(and this folder path is configurable as of version 2.2.1.0). As of version 2.3.0.0 you can issue
this command during a running instance of Auto Screen Capture.

-log=on
Enables logging.

-log=off
Disables logging.

-debug
Toggles "DebugMode". When enabled this command logs all debugging information.
Be very careful when enabling DebugMode because it's basically verbose logging.
I introduced this command line argument in 2.0.6, removed it in 2.2.0.7, and re-introduced it in 2.2.1.0 :)

-debug=on
Turns on "DebugMode".

-debug=off
Turns off "DebugMode".

-showSystemTrayIcon
Show the application's system tray icon.

-hideSystemTrayIcon
Hides the application's system tray icon.

-initial
Toggles "Initial Capture".
When enabled this command takes a screenshot of each screen and region before starting the timer.

-initial=on
Turns on "Initial Capture".

-initial=off
Turns off "Initial Capture".

-captureat=hh:mm:ss
Takes screenshots at the specified time.

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
contain the following 11 lines representing key-value pairs that will be parsed by the
application upon execution:
ScreenshotsFolder=screenshots
DebugFolder=!autoscreen\debug
LogsFolder=!autoscreen\logs
CommandFile=!autoscreen\command.txt
ApplicationSettingsFile=application.xml
UserSettingsFile=user.xml
EditorsFile=!autoscreen\editors.xml
RegionsFile=!autoscreen\regions.xml
ScreensFile=!autoscreen\screens.xml
TriggersFile=!autoscreen\triggers.xml
ScreenshotsFile=!autoscreen\screenshots.xml
TagsFile=!autoscreen\tags.xml

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

If you run into any issues with the application you can safely delete the "autoscreen.conf" file
and try again. However, some issues may require you to remove the "!autoscreen" directory which is
usually used to store the application's various XML files for its internal data system.

The default configuration file looks like this (the paths, however, might be different for you) ...
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
LogsFolder=!autoscreen\logs

# This file is monitored by the application for commands issued from the command line while it's running.
CommandFile=!autoscreen\command.txt

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

# References to tags.
TagsFile=!autoscreen\tags.xml
=======================================================================================================

As you can see the configuration file defines the folders and XML files the application should use.

You can create your own configuration file as long as it has, at a minimum, the following lines ...
ScreenshotsFolder=screenshots
DebugFolder=!autoscreen\debug
LogsFolder=!autoscreen\logs
CommandFile=!autoscreen\command.txt
ApplicationSettingsFile=!autoscreen\settings\application.xml
UserSettingsFile=!autoscreen\settings\user.xml
EditorsFile=!autoscreen\editors.xml
RegionsFile=!autoscreen\regions.xml
ScreensFile=!autoscreen\screens.xml
TriggersFile=!autoscreen\triggers.xml
ScreenshotsFile=!autoscreen\screenshots.xml
TagsFile=!autoscreen\tags.xml

All of these values represent local paths for the computer that Auto Screen Capture is running on, but
it's also possible to use network paths instead.

By default the "!autoscreen" directory is created, and used, for storing XML files ...
application.xml               Setup settings for the application (such as Debug Mode and Email)
user.xml                      Setup settings for the user (such as Interval and Schedule)
command.txt                   The command file is used to parse commands issued to a running instance
editors.xml                   Setup the user's image editors to use when editing screenshots
regions.xml                   Setup regions to capture on the user's computer
screens.xml                   Setup screens to capture on the user's computer
triggers.xml                  Setup triggers to control the application's behavior
screenshots.xml               List the screenshots that have been captured by the application
tags.xml                      Setup tags to use in a macro for customizing filenames

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
Commandfile=\\SKYWALKER\shared\autoscreen\%machine%\%user%\command.txt
ApplicationSettingsFile=\\SKYWALKER\shared\autoscreen\%machine%\%user%\settings\application.xml
UserSettingsFile=\\SKYWALKER\shared\autoscreen\%machine%\%user%\settings\user.xml
EditorsFile=\\SKYWALKER\shared\autoscreen\%machine%\%user%\editors.xml
RegionsFile=\\SKYWALKER\shared\autoscreen\%machine%\%user%\regions.xml
ScreensFile=\\SKYWALKER\shared\autoscreen\%machine%\%user%\screens.xml
TriggersFile=\\SKYWALKER\shared\autoscreen\%machine%\%user%\triggers.xml
ScreenshotsFile=\\SKYWALKER\shared\autoscreen\%machine%\%user%\screenshots.xml
TagsFile=\\SKYWALKER\shared\autoscreen\%machine%\%user%\tags.xml
=======================================================================================================

You don't need to use "autoscreen.conf" as the name for your configuration file.

If you have a configuration file that you want Auto Screen Capture to use when the application starts
you can specify the path and name of the configuration file with the "-config" command line argument.
For example, "-config=C:\MyAutoScreenCapture.conf" will start the application using the
config file named "MyAutoScreenCapture.conf" on the C:\ drive.



Data System
-----------
Auto Screen Capture stores its data in various XML files and the location of these XML files are
usually defined by the "autoscreen.conf" configuration file. Each XML data file will likely have
a root node that shows the version number and codename of the application such as ...
<autoscreen app:version="2.2.4.6" app:codename="Dalek" xmlns:app="autoscreen">

Auto Screen Capture's data system isn't very complex, but it's helpful to learn how it works.

application.xml
This file is for application settings that controls how the application is going
to perform when it's started. Should logging be enabled? What percentage of disk space
is acceptable until the application stops taking screenshots? Should the application
exit when it encounters an error or continues to run regardless of any errors?
A few examples of application setting nodes in application.xml:
<setting>
    <key>DebugMode</key>
    <value>False</value>
</setting>
<setting>
    <key>Logging</key>
    <value>False</value>
</setting>
<setting>
    <key>LowDiskPercentageThreshold</key>
    <value>1</value>
</setting>
<setting>
    <key>ExitOnError</key>
    <value>False</value>
</setting>

user.xml
This file is for user settings such as screen capture interval, setting a limit on how
many screenshots should be taken, and how many days old screenshots should be kept for.
A few examples of user setting nodes in user.xml:
<setting>
    <key>IntScreenCaptureInterval</key>
    <value>60000</value>
</setting>
<setting>
    <key>IntCaptureLimit</key>
    <value>100</value>
</setting>
<setting>
    <key>IntKeepScreenshotsForDays</key>
    <value>30</value>
</setting>

screens.xml
This file defines the screens (monitors or displays) the application should consider
and the properties of each screen, where screenshots should be written, and what
macro should be used when writing screenshot files in a particular image format.
The "component" indicates if it's the active window or a screen. If the component
value is 0 then the application will capture the active window otherwise it will
capture an available screen based on its number; 1, 2, 3, 4, 5, 6, etc.
An example of a screen node in screens.xml:
<screen>
    <active>True</active>
    <viewid>32e576b6-6ca4-4159-9256-11e8a2248d4c</viewid>
    <name>Screen 1</name>
    <folder>screenshots\</folder>
    <macro>%date%\%name%\%date%_%time%.%format%</macro>
    <component>1</component>
    <format>JPEG</format>
    <jpeg_quality>100</jpeg_quality>
    <resolution_ratio>100</resolution_ratio>
    <mouse>True</mouse>
</screen>

regions.xml
This file defines the regions you have setup. Like a screen, a region includes a
folder path, macro, image format, resolution ratio, JPEG quality, and if the mouse
should be included in the image. A region also includes the X, Y, Width, and Height
values to determine the area of the screen it should capture.
An example of a region node in regions.xml:
<region>
    <active>True</active>
    <viewid>c0a75b62-70af-4b4e-bba0-74a937cba83e</viewid>
    <name>Region 1</name>
    <folder>screenshots\</folder>
    <macro>%date%\%name%\%date%_%time%.%format%</macro>
    <format>JPEG</format>
    <jpeg_quality>100</jpeg_quality>
    <resolution_ratio>100</resolution_ratio>
    <mouse>True</mouse>
    <x>0</x>
    <y>0</y>
    <width>800</width>
    <height>600</height>
</region>

screenshots.xml
This file stores the references to screenshots that have been taken. These references
are useful for the application to know which screen a screenshot is associated with,
where each screenshot is located on the file system, when a screenshot was taken,
the format of the screenshot, the active window title, process name, and label.
The "viewid" identifies the screen or region which the screenshot is associated with.
An example of a screenshot node in screenshots.xml:
<screenshot>
    <version>2.3.0.0</version>
    <viewid>32e576b6-6ca4-4159-9256-11e8a2248d4c</viewid>
    <date>2020-02-03</date>
    <time>18:43:21.391</time>
    <path>screenshots\2020-02-03\Screen 1\2020-02-03_18-43-21-391.jpeg</path>
    <format>JPEG</format>
    <component>1</component>
    <slidename>{date=2020-02-03}{time=18:43:21.391}</slidename>
    <slidevalue>18:43:21.391 [Auto Screen Capture]</slidevalue>
    <windowtitle>Auto Screen Capture</windowtitle>
    <processname>autoscreen.exe</processname>
    <label>
    </label>
</screenshot>

editors.xml
This file contains all the image editors to use.
An example of an editor node in editors.xml:
<editor>
    <name>Microsoft Paint</name>
    <application>C:\Windows\System32\mspaint.exe</application>
    <arguments>%filepath%</arguments>
    <notes />
</editor>

tags.xml
This file determines what tags should be used in a macro when a screenshot is written
to an image file. Tags like %date% and %time% dynamically change value depending
on the date and time a macro is parsed and an image file is processed.
An example of a tag node in tags.xml:
<tag>
    <active>True</active>
    <name>%time%</name>
    <description>The current time (%time%)</description>
    <notes />
    <type>DateTimeFormat</type>
    <datetime_format_value>HH-mm-ss-fff</datetime_format_value>
    <time_of_day_morning_start>6/1/2020 12:00:00 AM</time_of_day_morning_start>
    <time_of_day_morning_end>6/1/2020 11:59:59 AM</time_of_day_morning_end>
    <time_of_day_afternoon_start>6/1/2020 12:00:00 PM</time_of_day_afternoon_start>
    <time_of_day_afternoon_end>6/1/2020 5:59:59 PM</time_of_day_afternoon_end>
    <time_of_day_evening_start>6/1/2020 6:00:00 PM</time_of_day_evening_start>
    <time_of_day_evening_end>6/1/2020 11:59:59 PM</time_of_day_evening_end>
    <time_of_day_morning_value>morning at %hour%-%minute%-%second%</time_of_day_morning_value>
    <time_of_day_afternoon_value>afternoon at %hour%-%minute%-%second%</time_of_day_afternoon_value>
    <time_of_day_evening_value>evening at %hour%-%minute%-%second%</time_of_day_evening_value>
    <evening_extends_to_next_morning>False</evening_extends_to_next_morning>
</tag>

triggers.xml
This file contains the various triggers that control the behaviour of the application.
Each trigger is based on a condition to consider and an action to take if the condition is met.
An example of a trigger node in triggers.xml:
<trigger>
    <active>True</active>
    <name>Interface Closing -&gt; Exit</name>
    <condition>InterfaceClosing</condition>
    <action>ExitApplication</action>
    <date>5/31/2020 9:32:19 PM</date>
    <time>5/31/2020 9:32:19 PM</time>
    <screen_capture_interval>0</screen_capture_interval>
    <module_item />
</trigger>



Troubleshooting and Debugging
-----------------------------
If things just seem weird or can't be easily explained beyond the normal usage of Auto Screen Capture
then you can always run the application in its Debug Mode. You can enable Debug Mode by either running
"autoscreen.exe -debug" from a command prompt or changing the DebugMode application setting to "True".
While the application is in Debug Mode it will write out logging messages to the logs directory and be
a lot more verbose with its logging compared to what you usually get with the Logging option enabled.

If all else fails and you still can't figure out what's going on then the best approach is the classic
"Have you tried turning it off and on again?" approach. In the case of Auto Screen Capture that means
deleting the "!autoscreen" directory (that was created on the first run according to its config file)
and running the application again. Every reference to screens, regions, screenshots, tags, and triggers
will be reset to its default state as if Auto Screen Capture was being run for the very first time.



Version History
---------------
Auto Screen Capture has a long history (10+ years) and it's advanced a lot since the first version.
I started writing the code for the application in 2008 after being frustrated that no screen capture
program, at the time, could take screenshots at a chosen interval while I was playing a game.

1.0
The very first version was a simple desktop application that took screenshots at an interval which
could only be specified in milliseconds. It also had a small list of screenshots called "Time Slices".
It was uploaded to SourceForge in 2008 and then picked up, and distributed, by Brothersoft.

2.0 Series
A calendar was introduced and, by version 2.0.5, you could capture up to four displays. The interval
was also updated in order for the user to specify hours, minutes, seconds, and milliseconds.

2.1 Series ("Clara")
You were now able to define how your files would be named by using a macro and macro tags instead
of relying on the application's default filename pattern. The series was codenamed "Clara" because
I would be watching Doctor Who (featuring Clara Oswald) while working on Auto Screen Capture :)

2.2 Series ("Dalek")
The application took a major step forward with the ability to capture screenshots from multiple displays,
filter screenshots, and automatically remove old screenshots. You can control the application's behaviour
with Triggers and customize filenames with your own Tags. You can also replace an old "autoscreen.exe"
binary with any 2.2 binary and it will upgrade its data system so that old screenshot references are
updated with the newest reference schema.

2.3 Series ("Boombayah")
Commands can now be issued to a running instance of the application!
These include ...
-interval=hh:mm:ss.nnn
-start
-startat=hh:mm:ss
-stop
-stopat=hh:mm:ss
-capture
-captureat=hh:mm:ss
-debug
-debug=on
-debug=off
-log
-log=on
-log=off
-hideSystemTrayIcon
-showSystemTrayIcon
Also introduced is the ability to activate and deactivate screens, regions, schedules, tags, and triggers.
You can now set an image editor to be your default editor when using "Capture Now -> Edit".
Another amazing enhancement are the multiple schedules that you can create and being able to have better
control over the application's workflow with more trigger conditions and trigger actions.
The system tray icon turns red if an error has been encountered.
The system tray icon turns yellow if a drive being used is running low on available disk space.
You can define length of filepaths with the FilepathLengthLimit application setting.
It's also much faster at startup (even with a lot of screenshot references being available).
The codename for this version is based on "Boombayah" by BLACKPINK (https://youtu.be/bwmSjveL3Lc)
since this was constantly playing in the background while I was writing the code for 2.3.0.0 :)