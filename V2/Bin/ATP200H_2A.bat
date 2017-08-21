set CurrentDir=%cd%

call Common.bat ATP200H ATP200H

call "Copy Mmi.Common.DateTimeInterpreter.bat"

rd /s /q "%CurrentDir%\MMI\ATP_200H"

move "%CurrentDir%\MMI\ATP_200H_CRH2A" "%CurrentDir%\MMI\ATP_200H"


move "%CurrentDir%\MMI\Config\IndexDescriptionConfig.ATP200HForCRH2A.xml~" "%CurrentDir%\MMI\Config\IndexDescriptionConfig.ATP200HForCRH2A.xml"

