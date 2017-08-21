set CurrentDir=%cd%

xcopy /y /e "%CurrentDir%\Train\General" "%CurrentDir%\MMI\"
xcopy /y /e "%CurrentDir%\Train\General.DialPlate.CRH5A" "%CurrentDir%\MMI\"

call "%CurrentDir%\SubsystemAdder\SubSysytemConfigEditer.exe" Display Merge "%CurrentDir%\MMI\Config\SystemConfig.xml;%CurrentDir%\MMI\Config\SystemConfig.General.DialPlate.xml"

del "%CurrentDir%\MMI\Config\SystemConfig.General.DialPlate.xml"


