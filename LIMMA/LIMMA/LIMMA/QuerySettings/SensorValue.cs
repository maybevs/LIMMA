using System;

namespace LIMMA.QuerySettings {
    public class SensorValue<T> : SensorValue {
        public SensorValue(string value, DateTime timestamp, IDeviceIdentifier deviceIdentifier, DeviceSensorIdentifier sensorId, T typedValue)
            : base(value, timestamp, deviceIdentifier, sensorId) {
            TypedValue = typedValue;
        }
        public T TypedValue { get; private set; }

        protected override object GetTypedValue() {
            return TypedValue;
        }
    }

    public class SensorValue {
        public SensorValue(string value, DateTime timestamp, IDeviceIdentifier deviceIdentifier, DeviceSensorIdentifier sensorId) {
            Value = value;
            DeviceIdentifier = new DeviceIdentifier(deviceIdentifier.ID);
            SetOccurredUniversalTime(timestamp);
            SensorIdentifier = new DeviceSensorIdentifier(sensorId.ID);
        }

        public string Value { get; private set; }
        public DateTime Timestamp { get; private set; }

        protected virtual object GetTypedValue() {
            return Value;
        }

        public object TypedValue => GetTypedValue();

        public DeviceSensorIdentifier SensorIdentifier { get; private set; }
        public IDeviceIdentifier DeviceIdentifier { get; private set; }

        private void SetOccurredUniversalTime(DateTime timestamp) {
            if (timestamp.Kind != DateTimeKind.Utc)
                throw new ArgumentException("Datetime must be universal time", "occurredOn");

            Timestamp = timestamp;
        }
    }

    public class AggregatedSensorValue<T> : SensorValue<T> {
        public AggregatedSensorValue(string value, DateTime timestamp, 
            IDeviceIdentifier deviceIdentifier, DeviceSensorIdentifier sensorId, 
            T typedValue, DateTime periodStart, DateTime periodEnd, 
            DateTime openTimestamp, T open,
            DateTime closeTimestamp, T close,
            DateTime highTimestamp, T high,
            DateTime lowTimestamp, T low,
            T sum, T average, long count) : base(value, timestamp, deviceIdentifier, sensorId, typedValue) {
            PeriodStart = periodStart;
            PeriodEnd = periodEnd;
            OpenTimestamp = openTimestamp;
            Open = open;
            Close = close;
            Low = low;
            High = high;
            Sum = sum;
            Average = average;
            CloseTimestamp = closeTimestamp;
            LowTimestamp = lowTimestamp;
            HighTimestamp = highTimestamp;
            Count = count;
        }

        public DateTime PeriodStart { get; private set; }
        public DateTime PeriodEnd { get; private set; }
        public DateTime OpenTimestamp { get; private set; }
        public T Open { get; private set; }
        public T Close { get; private set; }
        public T Low { get; private set; }
        public T High { get; private set; }
        public T Sum { get; private set; }
        public T Average { get; private set; }

        

        public DateTime CloseTimestamp { get; private set; }

        public DateTime LowTimestamp { get; private set; }
        

        public DateTime HighTimestamp { get; private set; }
        

        public long Count { get; private set; }
        
    }

}