echo off
set CurrentDir=%cd%
set Target=ActureFormConfig.xml


for /R "%CurrentDir%" %%i in (*.xml) do ( 
::===�����Ǽ�¼�ļ���,����Ҫ���¼�� 
set filename=%%~i

call :check %%~nxi

) 

pause

:check 
for %%i in (%Target%) do ( 
::===�жϺ������,�����Լ�ϲ������� 
if "%1"=="%%i" (
echo on
echo %filename% deleted!
move /Y "%filename%" "%filename%.bak" 
echo off
)
)


