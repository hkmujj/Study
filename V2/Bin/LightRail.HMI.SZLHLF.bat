set CurrentDir=%cd%

call Common.bat LightRail.HMI.SZLHLF LightRail.HMI.SZLHLF

call "Copy Mmi.Common.DateTimeInterpreter.bat"

xcopy /y /e "%CurrentDir%\Common\Mmi.Common.DateTimeInterpreter.*" "%CurrentDir%\MMI\"

pause