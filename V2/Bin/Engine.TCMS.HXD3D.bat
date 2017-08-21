set CurrentDir=%cd%

call Common.bat Engine.TCMS.HXD3D Engine.TCMS.HXD3D
call "Copy MsgReceiveMod.bat" 

call "Copy Mmi.Common.DateTimeInterpreter.bat"

xcopy /y /e "%CurrentDir%\Common\Mmi.Common.DateTimeInterpreter.*" "%CurrentDir%\MMI\"