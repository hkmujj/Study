set CurrentDir=%cd%

call Common.bat Subway.XiaMenLine1 Subway.XiaMenLine1

call "Copy Mmi.Common.DateTimeInterpreter.bat"

xcopy /y /e "%CurrentDir%\Common\Mmi.Common.DateTimeInterpreter.*" "%CurrentDir%\MMI\"