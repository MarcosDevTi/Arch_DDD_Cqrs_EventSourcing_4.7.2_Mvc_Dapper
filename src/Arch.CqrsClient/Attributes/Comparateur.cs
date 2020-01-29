using System.ComponentModel;

namespace Arch.CqrsClient.Attributes
{
    public enum Comparateur
    {
        [Description(" > ")]
        GreaterThen,
        [Description(" >= ")]
        GreaterThenOrEquals,
        [Description(" < ")]
        LessThen,
        [Description(" <= ")]
        LessThenOrEquals,
        [Description(" < ")]
        Contains,
        [Description(" in ")]
        Equals,
        [Description("not")]
        Different,
        [Description("start")]
        StartWith,
        [Description("end")]
        EndWith
    }
}
