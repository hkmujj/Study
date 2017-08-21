set CurrentDir=%cd%

set floder= 
IF  "%1%" NEQ ""  (
set floder=%1%
)

COPY /y "%CurrentDir%\Train\Config\ProjectConfig_%floder%.xml" "%CurrentDir%\MMI\Config\ProjectConfig.xml"

move "%CurrentDir%\MMI\Config\IndexDescriptionConfig.%floder%.xml~" "%CurrentDir%\MMI\Config\IndexDescriptionConfig.%floder%.xml" 

del /s /q "%CurrentDir%\MMI\Config\SystemConfig_CRH3C.xml"
del /s /q "%CurrentDir%\MMI\Config\SystemConfig_CRH380BL.xml"
del /s /q "%CurrentDir%\MMI\Config\SystemConfig_CRH380B.xml"

call "Copy Mmi.Common.DateTimeInterpreter.bat"

call "Copy MsgReceiveMod.bat"

xcopy /y /e "%CurrentDir%\Common\MsgReceiveMod.*" "%CurrentDir%\MMI\"

