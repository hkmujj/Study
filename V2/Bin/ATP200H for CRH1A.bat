set CurrentDir=%cd%

call Common.bat ATP200H ATP200H

call "Copy Mmi.Common.DateTimeInterpreter.bat"

rd /s /q "%CurrentDir%\MMI\ATP_200H_CRH2A"

move "%CurrentDir%\MMI\Config\IndexDescriptionConfig.ATP200HForCRH1A.xml~" "%CurrentDir%\MMI\Config\IndexDescriptionConfig.ATP200HForCRH1A.xml"
