set CurrentDir=%cd%

xcopy /y /e "%CurrentDir%\Train\Urba.Iran" "%CurrentDir%\MMI\"

xcopy /y /e "%CurrentDir%\Common\Mmi.Communication.Index.Adapter.*" "%CurrentDir%\MMI\"
xcopy /y /e "%CurrentDir%\3rdDlls\Microsoft.Practices.Prism.*" "%CurrentDir%\MMI\"


