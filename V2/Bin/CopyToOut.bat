set CurrentDir=%cd%

set ProjectPath= 
IF  "%1%" NEQ ""  (
set ProjectPath=%1%
)

xcopy /y /e "%CurrentDir%\Train\%ProjectPath%" "%CurrentDir%\MMI\"