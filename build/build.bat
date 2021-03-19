del /s /q ..\bin\Release\*.*
rmdir /s /q ..\bin\Release\!autoscreen
rmdir /s /q ..\bin\Release\screenshots
msbuild ..\autoscreen_vs2019.sln /p:Configuration=Release -restore
ILMerge ..\bin\Release\AutoScreenCapture.exe /out:..\bin\Release\autoscreen.exe ..\bin\Release\Renci.SshNet.dll ..\bin\Release\Gavin.Kendall.SFTP.dll
del ..\bin\Release\AutoScreenCapture.exe
del ..\bin\Release\autoscreen.pdb
del ..\bin\Release\AutoScreenCapture.exe.config
del ..\bin\Release\Gavin.Kendall.SFTP.dll
del ..\bin\Release\Gavin.Kendall.SFTP.pdb
del ..\bin\Release\Renci.SshNet.dll
del ..\bin\Release\Renci.SshNet.xml
signtool sign /f ..\autoscreen.pfx /p Sonic2020! /fd SHA256 ..\bin\Release\autoscreen.exe