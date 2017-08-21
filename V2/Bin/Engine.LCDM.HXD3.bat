set CurrentDir=%cd%

call Common.bat Engine.LCDM.HXD3 Engine.LCDM.HXD3

call "Copy Mmi.Common.DateTimeInterpreter.bat"

xcopy /y /e "%CurrentDir%\Common\Mmi.Common.DateTimeInterpreter.*" "%CurrentDir%\MMI\"