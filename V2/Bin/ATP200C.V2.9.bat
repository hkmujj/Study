set CurrentDir=%cd%

call Common.bat ATP.200C.V2.9 ATP200C

call "Copy Mmi.Common.DateTimeInterpreter.bat"

xcopy /y /e "%CurrentDir%\3rdDlls\CommonUtil.Rtf.*" "%CurrentDir%\MMI\"
