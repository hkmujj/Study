set CurrentDir=%cd%

call Common.bat ATP300H ATP300H

call "Copy Mmi.Common.DateTimeInterpreter.bat"

xcopy /y /e "%CurrentDir%\3rdDlls\CommonUtil.Rtf.*" "%CurrentDir%\MMI\"
