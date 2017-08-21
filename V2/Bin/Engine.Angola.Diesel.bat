set CurrentDir=%cd%

call Common.bat Engine.Angola.Diesel Engine.Angola.Diesel

call "Copy Mmi.Common.DateTimeInterpreter.bat"

xcopy /y /e "%CurrentDir%\Common\Mmi.Common.DateTimeInterpreter.*" "%CurrentDir%\MMI\"