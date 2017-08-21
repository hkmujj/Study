using System.Linq;

namespace General.CIR.Extentions
{
    public static class StringExtention
    {
        //[CompilerGenerated]
        //[Serializable]
        //private sealed class <>c
        //{
        //	public static readonly StringExtention.<>c <>9 = new StringExtention.<>c();

        //	public static Func<string, char, string> <>9__0_1;

        //	internal string <RemoveAt>b__0_1(string current, char t)
        //	{
        //		return current + t.ToString();
        //	}
        //}

        public static string RemoveAt(this string value, int index)
        {
            string text = string.Empty;
            bool flag = value.Length > index && index >= 0;
            if (flag)
            {
                
                text = value.Where((char t, int i) => i != index).Aggregate(text, (current, source) => current + source);
            }
            else
            {
                text = value;
            }
            return text;
        }
    }
}
