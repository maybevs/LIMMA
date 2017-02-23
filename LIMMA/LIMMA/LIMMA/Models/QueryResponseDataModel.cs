using System;
using LIMMA.Interfaces;

namespace LIMMA.Models {
    public class QueryResponseDataModel : DataModel {
        public QueryResponseDataModel(Guid widgetID, Guid targetID, IDataSource datasource/*, DatasourceDataResponse responseData*/)
            : base(datasource/*, responseData*/) {
            TargetID = targetID;
            WidgetID = widgetID;
        }

        public Guid TargetID { get; private set; }
        public Guid WidgetID { get; private set; }
    }
}