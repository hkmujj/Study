set CurrentDir=%cd%

set ProjectNmae=CRH2A
IF  "%1%" NEQ ""  (
set ProjectNmae=%1%
)

call Common.bat CRH2 CRH2

call "Copy Mmi.Common.DateTimeInterpreter.bat"

call "%CurrentDir%\MMI\CRH2TrainTypeSelector.exe" %ProjectNmae%
