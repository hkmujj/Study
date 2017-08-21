set CurrentDir=%cd%

rem 车辆屏来自  \V1\Train\Motor.HMI.CRH1A


call CopyToOut.bat "Other.ContactLine"
call CopyToOut.bat "ATP200H"
call CopyToOut.bat "ATP200C"


rd /s /q "%CurrentDir%\MMI\ATP_200H"
move "%CurrentDir%\MMI\ATP_200H_CRH2A" "%CurrentDir%\MMI\ATP_200H"