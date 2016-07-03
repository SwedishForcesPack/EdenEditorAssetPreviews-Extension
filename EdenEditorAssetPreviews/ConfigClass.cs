namespace EdenEditorAssetPreviews
{
    class ConfigClass
    {
        public string Name { get; private set; }
        public string Inherits { get; private set; }

        public ConfigClass(string name, string inherits)
        {
            Name = name;
            Inherits = inherits;
        }

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj.GetType() == typeof(ConfigClass) && ((ConfigClass)obj).Name == Name;
        }
    }
}
