using DeviceSM1.Data;
using DeviceSM1.Models.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceSM1.Models.ViewModel
{
    public class CreateDeviceViewModel
    {
        public Device Device { get; set; }
        public IList<Sensor> Sensors { get; set; }

        public CreateDeviceViewModel()
        {
            this.Sensors = new List<Sensor>(Constant.SENSOR_NUMBER);
            for (int i = 0; i < Constant.SENSOR_NUMBER; i++)
            {
                this.Sensors.Add(new Sensor() { SensorTypeId = i });
            }
        }
    }
}
