set CurrentDir=%cd%

call Common.bat Subway.ShenZhenLine11 Subway.ShenZhenLine11

call "Copy Mmi.Common.DateTimeInterpreter.bat"

xcopy /y /e "%CurrentDir%\Common\Mmi.Common.DateTimeInterpreter.*" "%CurrentDir%\MMI\"