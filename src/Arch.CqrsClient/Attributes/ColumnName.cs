using System;

namespace Arch.CqrsClient.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnName : Attribute
    {
        public string Nom { get; set; }
        public Comparateur Comparateur { get; set; }

        public ColumnName(string nom) => Nom = nom;
        public ColumnName(string nom, Comparateur comparateur)
        {
            Nom = nom;
            Comparateur = comparateur;
        }
    }
}
