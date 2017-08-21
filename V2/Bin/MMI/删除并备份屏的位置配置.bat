echo off
set CurrentDir=%cd%
set Target=ActureFormConfig.xml


for /R "%CurrentDir%" %%i in (*.xml) do ( 
::===这里是记录文件名,根据要求记录吧 
set filename=%%~i

call :check %%~nxi

) 

pause

:check 
for %%i in (%Target%) do ( 
::===判断后输出噢,根据自己喜欢输出吧 
if "%1"=="%%i" (
echo on
echo %filename% deleted!
move /Y "%filename%" "%filename%.bak" 
echo off
)
)


