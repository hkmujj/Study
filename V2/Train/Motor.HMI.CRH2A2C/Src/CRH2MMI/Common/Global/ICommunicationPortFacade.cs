using System.Collections.Generic;
using CRH2MMI.Config.ConfigModel;

namespace CRH2MMI.Common.Global
{
    internal interface ICommunicationPortFacade
    {
        List<int> GetInBoolIndexs(IEnumerable<string> rowNames);

        List<int> GetOutBoolIndexs(IEnumerable<string> rowNames);

        List<int> GetInFloatIndexs(IEnumerable<string> rowNames);

        List<int> GetOutFloatIndexs(IEnumerable<string> rowNames);

        List<int> GetInBoolIndexs(CRH2CommunicationPortModel configModel);

        List<int> GetOutBoolIndexs(CRH2CommunicationPortModel configModel);

        List<int> GetInFloatIndexs(CRH2CommunicationPortModel configModel);

        List<int> GetOutFloatIndexs(CRH2CommunicationPortModel configModel);

        int GetInBoolIndex(string name);

        int GetOutBoolIndex(string name);

        int GetInFloatIndex(string name);

        int GetOutFloatIndex(string name);
    }
}