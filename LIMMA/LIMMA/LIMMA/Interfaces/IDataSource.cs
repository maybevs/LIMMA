using System;
using LIMMA.QuerySettings;

namespace LIMMA.Interfaces
{
    public interface IDataSource
    {
        Guid DatasourceID { get; set; }
        Guid SourceTypeID { get; set; }
        Guid ProviderID { get; set; }
        string AngularService { get; set; }
        object Configuration { get; set; }
        object Data { get; set; }
        QuerySettingsResponse QuerySettingsResponse { get; set; }
    }
}