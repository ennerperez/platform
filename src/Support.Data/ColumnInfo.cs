using Platform.Support.Data.Attributes;

namespace Platform.Support.Data
{
    public struct ColumnInfo
    {
        [Column("name")]
        public string Name { get; set; }

        public int NotNull { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}