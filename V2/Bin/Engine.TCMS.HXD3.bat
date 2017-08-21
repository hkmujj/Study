set CurrentDir=%cd%

call Common.bat Engine.TCMS.HXD3 Engine.TCMS.HXD3

call "Copy Mmi.Common.DateTimeInterpreter.bat"

xcopy /y /e "%CurrentDir%\Common\Mmi.Common.DateTimeInterpreter.*" "%CurrentDir%\MMI\"