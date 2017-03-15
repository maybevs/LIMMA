using System;
using System.Collections.Generic;

namespace LIMMA.Models {
    public class WidgetBinding {
        public WidgetBinding(Guid widgetID, IEnumerable<TargetBinding> targets) {
            WidgetID = widgetID;
            Targets = targets;
        }

        public Guid WidgetID { get; private set; }
        public IEnumerable<TargetBinding> Targets { get; private set; }
    }

    public class DeviceBinding
    {
        public Guid DeviceId { get; set; }
        public Guid PageId { get; set; }
        public Guid NodeId { get; set; }
        public Guid WidgetId { get; set; }

    }
}