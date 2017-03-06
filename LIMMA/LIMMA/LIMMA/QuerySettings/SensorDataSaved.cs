using System;
using System.Collections.Generic;

namespace LIMMA.QuerySettings {
    public class SensorDataSaved {
        public SensorDataSaved(DeviceIdentifier deviceTypeIdentifier, IDeviceIdentifier deviceIdentifier, IEnumerable<SensorValueSeries> sensorValueSeries) {
            DeviceTypeIdentifier = deviceTypeIdentifier;
            DeviceIdentifier = deviceIdentifier;
            SensorValueSeries = sensorValueSeries;
            //EventID = new EventIdentifier(Guid.NewGuid());
            CurrentInstanceOnly = true;
        }

        //public IEventIdentifier EventID { get; private set; }
        public bool CurrentInstanceOnly { get; private set; }

        public DeviceIdentifier DeviceTypeIdentifier { get; private set; }
        public IDeviceIdentifier DeviceIdentifier { get; private set; }
        public IEnumerable<SensorValueSeries> SensorValueSeries { get; private set; }
    }
}