set CurrentDir=%cd%

xcopy /y /e "%CurrentDir%\..\Common\Prism\V4\Bin\Desktop\*.dll" "%CurrentDir%\MMI\"
xcopy /y /e "%CurrentDir%\..\Common\Prism\V4\Bin\Desktop\*.pdb" "%CurrentDir%\MMI\"
