using System;

namespace Arch.CqrsClient.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreColumn : Attribute
    {
    }
}
