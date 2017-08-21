set CurrentDir=%cd%

call Common.bat Engine.Dial.Angola Engine.Dial.Angola

call "Copy Mmi.Common.DateTimeInterpreter.bat"

xcopy /y /e "%CurrentDir%\Common\Mmi.Common.DateTimeInterpreter.*" "%CurrentDir%\MMI\"