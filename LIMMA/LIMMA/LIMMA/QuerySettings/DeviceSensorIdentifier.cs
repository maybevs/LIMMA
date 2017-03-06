using System;

namespace LIMMA.QuerySettings {

   
    
    public class DeviceSensorIdentifier {
        public DeviceSensorIdentifier(Guid id) { }

        public static implicit operator DeviceSensorIdentifier(Guid id) {
            return new DeviceSensorIdentifier(id);
        }

        public static DeviceSensorIdentifier New() {
            return From(Guid.NewGuid());
        }

        public static DeviceSensorIdentifier From(Guid id) {
            return (DeviceSensorIdentifier)id;
        }

        public static implicit operator DeviceSensorIdentifier(string id) {
            return new DeviceSensorIdentifier(Guid.Parse(id));
        }

        public static DeviceSensorIdentifier From(string id) {
            return (DeviceSensorIdentifier)id;
        }

        public Guid ID { get; set; }
    }

    //public class DeviceSensorKeyIdentifier : IDeviceSensorIdentifier {
    //    public Guid ID { get; private set; }
    //}
}