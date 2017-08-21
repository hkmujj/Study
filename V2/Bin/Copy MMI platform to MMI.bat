set CurrentDir=%cd%

xcopy /y /e "%CurrentDir%\Platform\*.dll" "%CurrentDir%\MMI\"
xcopy /y /e "%CurrentDir%\Platform\*.pdb" "%CurrentDir%\MMI\"
