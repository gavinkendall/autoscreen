@echo off
set build_dir=%cd%
rmdir /s /q !autoscreen
rmdir /s /q screenshots
del /s /q autoscreen.conf
del /s /q autoscreen.exe
del /s /q AutoScreenCaptureSetup.msi
del /s /q ..\bin\Release\*.*
rmdir /s /q ..\bin\Release\!autoscreen
rmdir /s /q ..\bin\Release\screenshots
devenv ..\autoscreen_vs2019.sln /Project ..\AutoScreenCapture.csproj /Build Release
signtool sign /f ..\autoscreen.pfx /p Sonic2020! /fd SHA256 ..\bin\Release\AutoScreenCapture.exe
signtool sign /f ..\autoscreen.pfx /p Sonic2020! /fd SHA256 ..\bin\Release\Renci.SshNet.dll
signtool sign /f ..\autoscreen.pfx /p Sonic2020! /fd SHA256 ..\bin\Release\Gavin.Kendall.SFTP.dll
ILMerge ..\bin\Release\AutoScreenCapture.exe /out:..\bin\Release\autoscreen.exe ..\bin\Release\Renci.SshNet.dll ..\bin\Release\Gavin.Kendall.SFTP.dll
del ..\bin\Release\AutoScreenCapture.exe
del ..\bin\Release\autoscreen.pdb
del ..\bin\Release\AutoScreenCapture.exe.config
del ..\bin\Release\Gavin.Kendall.SFTP.dll
del ..\bin\Release\Gavin.Kendall.SFTP.pdb
del ..\bin\Release\Renci.SshNet.dll
del ..\bin\Release\Renci.SshNet.xml

if exist "C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\IDE\CommonExtensions\Microsoft\VSI\DisableOutOfProcBuild\DisableOutOfProcBuild.exe" (
    cd "C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\IDE\CommonExtensions\Microsoft\VSI\DisableOutOfProcBuild\"
    DisableOutOfProcBuild.exe
    cd %build_dir%
)

devenv ..\autoscreen_vs2019.sln /Project ..\AutoScreenCaptureSetup\AutoScreenCaptureSetup.vdproj /Build Release
move ..\bin\Release\autoscreen.exe .
move ..\AutoScreenCaptureSetup\Release\AutoScreenCaptureSetup.msi .
del ..\AutoScreenCaptureSetup\Release\setup.exe
signtool sign /f ..\autoscreen.pfx /p Sonic2020! /fd SHA256 autoscreen.exe
signtool sign /f ..\autoscreen.pfx /p Sonic2020! /fd SHA256 AutoScreenCaptureSetup.msi