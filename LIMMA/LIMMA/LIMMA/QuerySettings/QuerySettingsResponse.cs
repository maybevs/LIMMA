using System.Collections.Generic;

namespace LIMMA.QuerySettings {
    public class QuerySettingsResponse {
        public QuerySettingsResponse(bool backedPagingEnabled, long? pageSize, long? totalRecords, long? currentPage, Sorting sorting, IEnumerable<FilterCondition> filter) {
            BackedPagingEnabled = backedPagingEnabled;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            CurrentPage = currentPage;
            Sorting = sorting;
            Filter = filter;
        }

        public bool BackedPagingEnabled { get; private set; }

        public long? PageSize { get; private set; }

        public long? TotalRecords { get; private set; }

        public long? CurrentPage { get; private set; }

        public Sorting Sorting { get; private set; }

        public IEnumerable<FilterCondition> Filter { get; private set; }
    }
}