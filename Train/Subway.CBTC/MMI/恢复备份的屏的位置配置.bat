echo off
set CurrentDir=%cd%
set Target=ActureFormConfig.xml.bak


for /R "%CurrentDir%" %%i in (*.bak) do ( 
::===�����Ǽ�¼�ļ���,����Ҫ���¼�� 
set filename=%%~i
set targetFileName=%%~pi\ActureFormConfig.xml
call :check %%~nxi

) 

pause

:check 
for %%i in (%Target%) do ( 
::===�жϺ������,�����Լ�ϲ������� 
if "%1"=="%%i" (
echo on
echo %filename% resumed!
move /Y "%filename%" "%targetFileName%" 
echo off
)
) 


