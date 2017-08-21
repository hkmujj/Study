using System;

namespace Subway.ShenZhenLine11.ShenZhenAttribute
{
    public class TitleNameAttribute:Attribute
    {
        public TitleNameAttribute()
        {
            
        }
        public string Name { get; set; }
    }
}