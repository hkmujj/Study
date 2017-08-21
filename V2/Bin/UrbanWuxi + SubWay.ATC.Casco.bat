set CurrentDir=%cd%

xcopy /y /e "%CurrentDir%\Train\General" "%CurrentDir%\MMI\"
xcopy /y /e "%CurrentDir%\Train\ATC.Casco" "%CurrentDir%\MMI\"
xcopy /y /e "%CurrentDir%\Train\Urban.Wuxi" "%CurrentDir%\MMI\"

COPY /y "%CurrentDir%\Train\Config\NetDataConfig_C3.xml" "%CurrentDir%\MMI\Config\NetDataConfig.xml"
COPY /y "%CurrentDir%\Train\Config\SystemConfig_Urban.wuxi_ATP.Casco.xml" "%CurrentDir%\MMI\Config\SystemConfig.xml"

xcopy /y /e "%CurrentDir%\Common\Mmi.Communication.Index.Adapter.*" "%CurrentDir%\MMI\"
COPY /y "%CurrentDir%\Train\Config\NetDataConfig_C3.xml" "%CurrentDir%\MMI\Config\NetDataConfig.xml"
COPY /y "%CurrentDir%\Train\Config\SystemConfig_Urban.wuxi_ATP.Casco.xml" "%CurrentDir%\MMI\Config\SystemConfig.xml"



