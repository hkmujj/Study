echo "run CopyFileToConfigPath.bat"
SET ProjectPath=%cd%\Src\CRH2MMI\

IF  "%1%" NEQ ""  (
set ProjectPath=%1%\
)

copy /Y "%ProjectPath%..\..\..\Bin\Common\MMICommon\*.dll" "%ProjectPath%..\MMI\"
copy /Y "%ProjectPath%..\..\..\Bin\MMI\*.dll" "%ProjectPath%..\MMI\"
copy /Y "%ProjectPath%..\..\..\Bin\MMI\*.exe" "%ProjectPath%..\MMI\"
copy /Y "%ProjectPath%..\..\..\Bin\MMI\*.config" "%ProjectPath%..\MMI\"

copy /Y "%ProjectPath%.\Config\DetailConfig\*.xml" "%ProjectPath%..\MMI\CRH2\config\"
copy /Y "%ProjectPath%.\Config\DetailConfig\*.xml" "%ProjectPath%..\MMI\CRH2_2\config\"
:: copy /Y "%ProjectPath%.\Config\*.xml" "%ProjectPath%..\MMI\config\"


