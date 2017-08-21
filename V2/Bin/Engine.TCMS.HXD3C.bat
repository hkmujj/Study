set CurrentDir=%cd%

call Common.bat Engine.TCMS.HXD3C Engine.TCMS.HXD3C

call "Copy Mmi.Common.DateTimeInterpreter.bat"

xcopy /y /e "%CurrentDir%\Common\Mmi.Common.DateTimeInterpreter.*" "%CurrentDir%\MMI\"