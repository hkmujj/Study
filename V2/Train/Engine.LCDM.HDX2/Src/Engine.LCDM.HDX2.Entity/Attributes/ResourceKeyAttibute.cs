using System;
using System.Runtime.InteropServices;

namespace Engine.LCDM.HDX2.Entity.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class ResourceKeyAttribute : Attribute
    {
        public string ResourceKey { private set; get; }

        public ResourceKeyAttribute(string resourceKey)
        {
            ResourceKey = resourceKey;
        }
    }
}