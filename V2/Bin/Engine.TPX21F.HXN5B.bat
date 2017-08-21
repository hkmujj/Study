set CurrentDir=%cd%

call Common.bat Engine.TPX21F.HXN5B Engine.TPX21F.HXN5B

call "Copy Mmi.Common.DateTimeInterpreter.bat"

xcopy /y /e "%CurrentDir%\Common\Mmi.Common.DateTimeInterpreter.*" "%CurrentDir%\MMI\"