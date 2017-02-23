namespace LIMMA.QuerySettings {
    public class FilterCondition {
        public FilterCondition() {}

        public FilterCondition(string columnKey, FilterComparer comparer, string columnValue) {
            ColumnKey = columnKey;
            Comparer = comparer;
            ColumnValue = columnValue;
        }

        /// <summary>
        /// Mainly used in the UI for widgets to handle their own Filters
        /// </summary>
        public string Label { get; set; }
        public string ColumnKey { get; set; }
        public FilterComparer Comparer { get; set; }
        public string ColumnValue { get; set; }
    }
}