set CurrentDir=%cd%

call Common.bat CRH380B CRH3C

call "Copy Mmi.Common.DateTimeInterpreter.bat"

rd /s /q "%CurrentDir%\MMI\1D_HMI_L_380B"
rd /s /q "%CurrentDir%\MMI\3D_HMI_R_380B"
rd /s /q "%CurrentDir%\MMI\1D_HMI_L_380BL"
rd /s /q "%CurrentDir%\MMI\3D_HMI_R_380BL"

call CopyCRH380Config.bat CRH3C


