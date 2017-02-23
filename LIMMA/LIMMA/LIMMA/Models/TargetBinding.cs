using System;

namespace LIMMA.Models {
    public class TargetBinding {
        public TargetBinding(Guid datasourceID, Guid targetID, string expression) {
            DatasourceID = datasourceID;
            Expression = expression;
            TargetID = targetID;
        }

        public Guid DatasourceID { get; private set; }
        public Guid TargetID { get; private set; }
        public string Expression { get; private set; }
    }
}