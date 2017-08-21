set CurrentDir=%cd%

call Common.bat ATP300S ATP300S

call "Copy Mmi.Common.DateTimeInterpreter.bat"

xcopy /y /e "%CurrentDir%\3rdDlls\CommonUtil.Rtf.*" "%CurrentDir%\MMI\"
