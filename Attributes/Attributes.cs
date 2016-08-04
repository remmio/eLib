using System;
using System.ComponentModel;

namespace eLib.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class NotTested : Attribute
    {
        public string Message { get; private set; }

        public NotTested(string locale)
        {
            Message = locale;
        }
    }

    public sealed class ResumeAttribute : DescriptionAttribute
    {
        public ResumeAttribute(string description):base(description)
        {

        }
    }
}
