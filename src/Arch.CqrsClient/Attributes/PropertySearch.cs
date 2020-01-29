namespace Arch.CqrsClient.Attributes
{
    public class PropertySearch<T>
    {
        public PropertySearch(T property)
        {
            Property = property;
        }

        public PropertySearch(T property, Comparateur comparateur)
        {
            Property = property;
            Comparateur = comparateur;
        }
        public T Property { get; set; }
        public Comparateur Comparateur { get; set; } = Comparateur.Equals;
    }
}
