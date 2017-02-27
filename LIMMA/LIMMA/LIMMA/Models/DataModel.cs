using System;
using LIMMA.Interfaces;
using LIMMA.QuerySettings;

namespace LIMMA.Models {

    public class DataModel : IDataSource
    {
        public DataModel(/*IDataSource datasource/* /*, DatasourceDataResponse responseData*/) {
            //DatasourceID = datasource.ID;
            //SourceTypeID = datasource.SourceTypeID;
            //ProviderID = datasource.ProviderID;
            //Configuration = datasource.Configuration;
            //AngularService = datasource.AngularService;
            //Data = responseData.With(d => d.Data);
            //QuerySettingsResponse = responseData.With(d => d.QuerySettingsResponse);
        }

        public Guid DatasourceID { get; set; }
        public Guid SourceTypeID { get; set; }
        public Guid ProviderID { get; set; }
        public string AngularService { get; set; }
        public object Configuration { get; set; }
        public object Data { get; set; }
        public QuerySettingsResponse QuerySettingsResponse { get; set; }
    }
}