using System;
using System.Collections.Generic;
using System.Linq;

namespace Arch.CqrsClient.Attributes
{
    public class BuilderComparator
    {
        public HashSet<(Comparateur Comparator, string Value)> Comparators { get; set; }
        private string Value { get; set; }
        public BuilderComparator(string value)
        {
            Comparators = new HashSet<(Comparateur Comparator, string Value)>();
            Value = value;
        }

        public BuilderComparator Add(Comparateur comparateur, string value)
        {
            Comparators.Add((comparateur, value));
            return this;
        }

        public BuilderComparator Add(params Comparateur[] comparateurs)
        {
            foreach (var comparator in comparateurs)
                Comparators.Add((comparator, Value));
            return this;
        }

        public string Build(Comparateur comparator)
        {
            var comp = Comparators.FirstOrDefault(_ => _.Comparator == comparator);
            return Comparators.FirstOrDefault(_ => _.Comparator == comparator).Value ?? $" = " + Value;
        }

        public string DictionaryComparators(string value, Func<(Comparateur Comparator, string Value), bool> predicate)
        {
            var dic = new HashSet<(Comparateur Comparator, string Value)>
            {
                (Comparateur.Equals, $" = " + value),
                (Comparateur.LessThen, $" < {value}"),
                (Comparateur.LessThenOrEquals, $" <= {value}"),
                (Comparateur.GreaterThen, $" > {value}"),
                (Comparateur.GreaterThenOrEquals, $" >= {value}")
            };

            return dic.FirstOrDefault(predicate).Value;
        }
    }
}
