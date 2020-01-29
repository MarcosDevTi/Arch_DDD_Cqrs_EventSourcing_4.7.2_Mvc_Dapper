namespace Arch.CqrsClient.Attributes
{
    public class PropertyComparable<T>
    {
        public PropertyComparable(T property)
        {
            Property = property;
        }

        public PropertyComparable(T property, Comparateur comparateur)
        {
            Property = property;
            Comparateur = comparateur;
        }
        public T Property { get; set; }
        public Comparateur Comparateur { get; set; } = Comparateur.Equals;
    }
}
