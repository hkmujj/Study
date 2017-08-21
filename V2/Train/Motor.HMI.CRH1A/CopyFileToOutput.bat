echo "run CopyFileToConfigPath.bat"
echo OFF
SET ProjectPath=%cd%\GT_HMI\

IF  "%1%" NEQ ""  (
set ProjectPath=%1%\
)


copy /Y "%ProjectPath%..\..\..\Bin\Development outputs\*.dll" "%ProjectPath%..\Development outputs\"
copy /Y "%ProjectPath%..\..\..\Bin\Development outputs\*.exe" "%ProjectPath%..\Development outputs\"
copy /Y "%ProjectPath%..\..\..\Bin\Development outputs\*.config" "%ProjectPath%..\Development outputs\"

copy /Y "%ProjectPath%.\Config\CRH1AConfig.xml" "%ProjectPath%..\Development outputs\TOD\config\"
copy /Y "%ProjectPath%.\Config\CRH1AGuangZhouConfig.xml" "%ProjectPath%..\Development outputs\TOD\config\"
copy /Y "%ProjectPath%.\Config\CRH1AChengduConfig.xml" "%ProjectPath%..\Development outputs\TOD\config\"
copy /Y "%ProjectPath%.\Config\CRH1AWuHanConfig.xml" "%ProjectPath%..\Development outputs\TOD\config\"

copy /Y "%ProjectPath%.\Config\LogConfig.xml" "%ProjectPath%..\Development outputs\Config\"

copy /Y "%ProjectPath%..\..\..\Bin\Common\MMICommon\*.dll" "%ProjectPath%..\Development outputs\"