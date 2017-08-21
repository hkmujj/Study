set CurrentDir=%cd%

call Common.bat Engine.Angola.TCMS Engine.Angola.TCMS

call "Copy Mmi.Common.DateTimeInterpreter.bat"

xcopy /y /e "%CurrentDir%\Common\Mmi.Common.DateTimeInterpreter.*" "%CurrentDir%\MMI\"