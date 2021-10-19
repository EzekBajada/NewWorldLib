using NewWorldLib.Datasheets.Internal;

namespace NewWorldLib.Datasheets.Internals
{
    public class DatasheetColumn
    {
        public int Unknown1 { get; set; }
        public int ColumnNameOffset { get; set; }
        public DatasheetColumnType ColumnType { get; set; }
        public string ColumnName { get; set; }
    }
}