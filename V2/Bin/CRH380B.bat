set CurrentDir=%cd%

call Common.bat CRH380B CRH380B

rd /s /q "%CurrentDir%\MMI\1D_HMI_L_CRH3C"
rd /s /q "%CurrentDir%\MMI\3D_HMI_R_CRH3C"
rd /s /q "%CurrentDir%\MMI\1D_HMI_L_380BL"
rd /s /q "%CurrentDir%\MMI\3D_HMI_R_380BL"

call CopyCRH380Config.bat CRH380B

