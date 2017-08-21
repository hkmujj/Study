set CurrentDir=%cd%

call Common.bat ATP300T.V4.0 ATP300T

call "Copy Mmi.Common.DateTimeInterpreter.bat"
call "Copy MsgReceiveMod.bat"
call "Copy CommonUtil.Rtf.bat"

pause