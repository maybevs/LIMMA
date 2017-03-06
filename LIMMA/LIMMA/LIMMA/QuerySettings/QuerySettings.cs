using System.Collections.Generic;

namespace LIMMA.QuerySettings {
    public class QuerySettings {
        /// <summary>
        /// URL parameter
        /// </summary>
        public Dictionary<string, List<string>> Parameter { get; set; }

        /// <summary>
        /// Filter conditions for Datasources
        /// </summary>
        public IEnumerable<FilterCondition> Filter { get; set; }

        /// <summary>
        /// Sort direction
        /// </summary>
        public Sorting Sorting { get; set; }

        /// <summary>
        /// If Paging, then the page size
        /// </summary>
        public long? PageSize { get; set; }

        /// <summary>
        /// If Paging, then the current page
        /// </summary>
        public long? CurrentPage { get; set; }

        /// <summary>
        /// Requires the query historical data or just a snapshot
        /// </summary>
        public bool IsQueryHistoricalData { get; set; }

        /// <summary>
        /// Custom datasource filter
        /// </summary>
        public string CustomFilter { get; set; }

        /// <summary>
        /// The timezone offset of the UI in case of Datetime computations are required
        /// </summary>
        public int TimezoneOffset { get; set; }
    }
}