using System.Collections.Generic;
using MMI.Facility.Interface.Data;

namespace MMI.Facility.Control.Data
{
    public interface IUIObjectParser
    {
        List<IUIObject> Parser(string file);
    }

}
