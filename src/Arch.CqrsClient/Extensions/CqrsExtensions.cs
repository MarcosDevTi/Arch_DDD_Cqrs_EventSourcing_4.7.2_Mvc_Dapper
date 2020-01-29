using Arch.CqrsClient.Attributes;
using Arch.CqrsClient.Models.SearchModel;
using System;
using System.Reflection;
using System.Text;

namespace Arch.CqrsClient.Extensions
{
    public static class CqrsExtensions
    {
        public static string GetWhereSql(this CustomerSearchAbstract obj, string alias = null)
        {
            var sb = new StringBuilder();
            sb.AppendLine("where");
            var props = obj.GetType().GetProperties();

            foreach (var prop in props)
            {
                var pro = prop.GetCustomAttribute<ColumnName>()?.Nom ?? prop.Name;

                var valeurProp = prop.GetValue(obj);
                var al = alias == null ? string.Empty : alias + ".";
                if (valeurProp != null)
                    sb.AppendLine($"{al}{pro} {valeurProp.FormatSqlValeur()} and ");
            }

            var result = sb.ToString();
            if (result != string.Empty)
            {
                result = result.Substring(0, result.Length - 6);
            }

            return result;
        }

        public static string GetWhereSql(this object obj, string alias = null)
        {
            var sb = new StringBuilder();
            sb.AppendLine("where");
            var props = obj.GetType().GetProperties();

            foreach (var prop in props)
            {
                var pro = prop.GetCustomAttribute<ColumnName>()?.Nom ?? prop.Name;

                var valeurProp = prop.GetValue(obj);
                var al = alias == null ? string.Empty : alias + ".";
                if (valeurProp != null)
                    sb.AppendLine($"{al}{pro} {valeurProp.FormatSqlValeur()} and ");
            }

            var result = sb.ToString();
            if (result != string.Empty)
            {
                result = result.Substring(0, result.Length - 6);
            }

            return result;
        }

        public static string FormatSqlValeur(this object valeur)
        {
            var result = "";

            var valProperty = ((dynamic)valeur).Property;
            var comparator = (Comparateur)((dynamic)valeur).Comparateur;

            switch (valProperty)
            {
                case string s:
                    {
                        var valProp = (string)valProperty;
                        result = valProp.ApplyComparatorString(comparator);
                    }

                    break;
                case DateTime d:
                    result = $" = '{valProperty}'";
                    break;
                default:
                    result = " = " + valProperty.ToString();
                    break;
            }

            return result;
        }

        public static string ApplyComparatorDateTime(this DateTime value, Comparateur comparator)
        {
            var comparatorsDateTime = new BuilderComparator($"'{value}'").Add(
                Comparateur.Equals, Comparateur.LessThen, Comparateur.LessThenOrEquals,
                Comparateur.GreaterThen, Comparateur.GreaterThenOrEquals);

            return "";
        }
        public static string ApplyComparatorString(this string value, Comparateur comparator)
        {
            var result = "";
            switch (comparator)
            {
                case Comparateur.Equals:
                    result = $" = '{value}'";
                    break;
                case Comparateur.StartWith:
                    result = $" like '%{value}'";
                    break;
                case Comparateur.EndWith:
                    result = $" like '{value}%'";
                    break;
                case Comparateur.Contains:
                    result = $" like '%{value}%'";
                    break;
                default:
                    result = $" = '{value}'";
                    break;
            }
            return result;
        }
    }
}

