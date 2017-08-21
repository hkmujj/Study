using System.Collections.Generic;
using System.Linq;

namespace Engine.TAX2.SS7C.Model.ViewSource
{
    public class KeybordStrings
    {
        public List<string> Numbers { private set; get; }

        public static readonly KeybordStrings Instance = new KeybordStrings();

        public const int CountPerLine = 5;

        private KeybordStrings()
        {
            Numbers = Enumerable.Range(0, 10).Select(s => s.ToString()).ToList();
        }
    }
}