set CurrentDir=%cd%

call Common.bat Subway.ShenZhenLine9 Subway.ShenZhenLine9

call "Copy Mmi.Common.DateTimeInterpreter.bat"

xcopy /y /e "%CurrentDir%\Common\Mmi.Common.DateTimeInterpreter.*" "%CurrentDir%\MMI\"