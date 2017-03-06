using System;

namespace LIMMA.QuerySettings {

    public interface IDeviceIdentifier
    {
        Guid ID { get; set; }
    }

    public class DeviceIdentifier : IDeviceIdentifier
    {
        public DeviceIdentifier(Guid id) { }

        public static implicit operator DeviceIdentifier(Guid id) {
            return new DeviceIdentifier(id);
        }

        public static IDeviceIdentifier New() {
            return From(Guid.NewGuid());
        }

        public static IDeviceIdentifier From(Guid id) {
            return (DeviceIdentifier)id;
        }

        public static implicit operator DeviceIdentifier(string id) {
            return new DeviceIdentifier(Guid.Parse(id));
        }

        public static IDeviceIdentifier From(string id) {
            return (DeviceIdentifier)id;
        }

        public Guid ID { get; set; }
    }
}