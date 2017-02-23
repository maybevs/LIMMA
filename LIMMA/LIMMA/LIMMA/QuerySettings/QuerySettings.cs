using System.Collections.Generic;

namespace LIMMA.QuerySettings {
    public class QuerySettings {
        public Dictionary<string, List<string>> Parameter { get; set; }
        public IEnumerable<FilterCondition> Filter { get; set; }
        public Sorting Sorting { get; set; }

        public long? PageSize { get; set; }
        public long? CurrentPage { get; set; }

        public bool IsQueryHistoricalData { get; set; }
        public string CustomFilter { get; set; }

        public int TimezoneOffset { get; set; }
    }
}