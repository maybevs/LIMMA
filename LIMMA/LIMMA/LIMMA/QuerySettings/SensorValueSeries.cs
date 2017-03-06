namespace LIMMA.QuerySettings {
    public class SensorValueSeries {
        public SensorValueSeries(DeviceSensorIdentifier sensorIdentifier, SensorValue[] values) {
            SensorIdentifier = sensorIdentifier;
            Values = values;
        }

        public DeviceSensorIdentifier SensorIdentifier { get; private set; }
        public SensorValue[] Values { get; private set; }
    }
}