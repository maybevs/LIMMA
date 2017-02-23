using System;
using System.Collections.Generic;

namespace LIMMA.Models {
    public class NodeBindings {
        public NodeBindings(IDictionary<Guid, DataModel> dataModels, IDictionary<Guid, WidgetBinding> widgetBindings) {
            DataModels = dataModels;
            WidgetBindings = widgetBindings;
        }

        public IDictionary<Guid, DataModel> DataModels { get; private set; }
        public IDictionary<Guid, WidgetBinding> WidgetBindings { get; private set; }
    }
}