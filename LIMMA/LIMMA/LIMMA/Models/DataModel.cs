using System;
using LIMMA.Interfaces;
using LIMMA.QuerySettings;

namespace LIMMA.Models {

    public class DataModel : IDataSource
    {
        public DataModel(IDataSource datasource/*, DatasourceDataResponse responseData*/) {
            //DatasourceID = datasource.ID;
            SourceTypeID = datasource.SourceTypeID;
            ProviderID = datasource.ProviderID;
            Configuration = datasource.Configuration;
            AngularService = datasource.AngularService;
            //Data = responseData.With(d => d.Data);
            //QuerySettingsResponse = responseData.With(d => d.QuerySettingsResponse);
        }

        public Guid DatasourceID { get; private set; }
        public Guid SourceTypeID { get; private set; }
        public Guid ProviderID { get; private set; }
        public string AngularService { get; private set; }
        public object Configuration { get; private set; }
        public object Data { get; private set; }
        public QuerySettingsResponse QuerySettingsResponse { get; private set; }
    }
}