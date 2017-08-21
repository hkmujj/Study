set CurrentDir=%cd%

xcopy /y /e "%CurrentDir%\Train\ATC.Casco" "%CurrentDir%\MMI\"
xcopy /y /e "%CurrentDir%\Train\Urban.QingDao3Line" "%CurrentDir%\MMI\"
xcopy /y /e "%CurrentDir%\Train\General" "%CurrentDir%\MMI\"

COPY /y "%CurrentDir%\Train\Config\NetDataConfig_C3.xml" "%CurrentDir%\MMI\Config\NetDataConfig.xml"
COPY /y "%CurrentDir%\Train\Config\SystemConfig_QingDao3Line_VT_Subway.ATC.Casco_General.DialPlate.xml" "%CurrentDir%\MMI\Config\SystemConfig.xml"




