using System;
using LIMMA.QuerySettings;

namespace LIMMA.Interfaces
{
    public interface IDataSource
    {
        Guid DatasourceID { get; }
        Guid SourceTypeID { get; }
        Guid ProviderID { get; }
        string AngularService { get; }
        object Configuration { get; }
        object Data { get; }
        QuerySettingsResponse QuerySettingsResponse { get; }
    }
}