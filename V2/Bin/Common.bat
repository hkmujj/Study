set CurrentDir=%cd%

set floder= 
IF  "%1%" NEQ ""  (
set floder=%1%
)

set project= 
IF  "%2%" NEQ ""  (
set project=%2%
)

call "%CurrentDir%\Copy MMI platform to MMI.bat"
call "%CurrentDir%\Copy Prism V4 to MMI.bat"

xcopy /y /e "%CurrentDir%\Train\%floder%" "%CurrentDir%\MMI\"

del /s /q "%CurrentDir%\MMI\Config\SystemConfig_%project%.xml"

if not exist "%CurrentDir%\MMI\Config\SystemConfig.xml" (
call "%CurrentDir%\Copy Null System config.bat"
)

call "%CurrentDir%\SubsystemAdder\SubSysytemConfigEditer.exe" Normal Merge "%CurrentDir%\MMI\Config\SystemConfig.xml;%CurrentDir%\Train\%floder%\Config\SystemConfig_%project%.xml"
