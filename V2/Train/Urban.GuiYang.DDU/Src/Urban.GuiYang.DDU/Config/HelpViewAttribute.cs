using System;

namespace Urban.GuiYang.DDU.Config
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class HelpViewAttribute : Attribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        readonly string positionalString;

        // This is a positional argument
        public HelpViewAttribute(string positionalString)
        {
            this.positionalString = positionalString;

        }

        public HelpViewAttribute(Type type) : this(type.FullName)
        {
            Help = type;
        }
        public Type Help { get; set; }
        public string PositionalString { get; private set; }

        // This is a named argument
        public int NamedInt { get; set; }
    }
}
