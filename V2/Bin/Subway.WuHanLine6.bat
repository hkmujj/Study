set CurrentDir=%cd%

call Common.bat Subway.WuHanLine6 Subway.WuHanLine6

call "Copy Mmi.Common.DateTimeInterpreter.bat"

xcopy /y /e "%CurrentDir%\Common\Mmi.Common.DateTimeInterpreter.*" "%CurrentDir%\MMI\"