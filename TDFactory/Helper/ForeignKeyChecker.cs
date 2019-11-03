namespace TDFactory
{
    public class ForeignKeyChecker
    {
        public string ForeignKeyName { get; set; }
        public string PrimaryTableName { get; set; }
        public string PrimaryColumnName { get; set; }
        public string PrimaryColumnType { get; set; }
        public string ForeignTableName { get; set; }
        public string ForeignColumnName { get; set; }
        public string ForeignColumnType { get; set; }
        public string PrimaryTableAndColumnName { get; set; }
        public string ForeignTableAndColumnName { get; set; }
    }
}
