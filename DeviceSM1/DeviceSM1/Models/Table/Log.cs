using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DeviceSM1.Models.Table
{
    [Table("Log")]
    public class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string IMEI { get; set; }
        public double Strength { get; set; }
        public double Voltage { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double SensorValue1 { get; set; }
        public double SensorValue2 { get; set; }
        public double SensorValue3 { get; set; }
        public double SensorValue4 { get; set; }
        public double SensorValue5 { get; set; }
        public double SensorValue6 { get; set; }
        public int SensorStatus1 { get; set; }
        public int SensorStatus2 { get; set; }
        public int SensorStatus3 { get; set; }
        public int SensorStatus4 { get; set; }
        public int SensorStatus5 { get; set; }
        public int SensorStatus6 { get; set; }

        public IList<Alert> Alerts { get; set; }

        [NotMapped]
        public bool IsValid { get; set; }

        public bool ParseString(string data)
        {
            //TODO: Check the length of string

            // Split data into words by delimiter comma
            string[] words = data.Split(",");

            //TODO: Check the count of packet words

            // Check header
            if (words[0] != "Now")
            {
                IsValid = false;
                return false;
            }
            // Parse timestamp
            Timestamp = DateTime.ParseExact(words[1] + words[2], "yyMMddHH:mm", null);
            // Parse IMEI
            IMEI = words[3];
            // Parse signal strength
            string StrengthString = Regex.Match(words[4], @"\d*\.?\d+").Value;
            Strength = Double.Parse(StrengthString);
            // Parse voltage
            string VoltageString = Regex.Match(words[5], @"\d*\.?\d+").Value;
            Voltage = Double.Parse(VoltageString);
            // Parse latitude
            Latitude = Double.Parse(words[6]);
            // Parse longitude
            Longitude = Double.Parse(words[7]);
            // Parse first sensor value
            SensorValue1 = Double.Parse(words[8]);
            // Parse second sensor value
            SensorValue2 = Double.Parse(words[9]);
            // Parse third sensor value
            SensorValue3 = Double.Parse(words[10]);
            // Parse forth sensor value
            SensorValue4 = Double.Parse(words[11]);
            // Parse fifth sensor value
            SensorValue5 = Double.Parse(words[12]);
            // Parse sixth sensor value
            SensorValue6 = Double.Parse(words[13]);
            // Parse first sensor status
            SensorStatus1 = words[14] == "GPIO1_ON" ? 1 : 0;
            // Parse first sensor status
            SensorStatus1 = words[15] == "GPIO2_ON" ? 1 : 0;
            // Parse first sensor status
            SensorStatus1 = words[16] == "GPIO3_ON" ? 1 : 0;
            // Parse first sensor status
            SensorStatus1 = words[17] == "GPIO4_ON" ? 1 : 0;
            // Parse first sensor status
            SensorStatus1 = words[18] == "GPIO5_ON" ? 1 : 0;
            // Parse first sensor status
            SensorStatus1 = words[19] == "GPIO6_ON" ? 1 : 0;
            // Check footer
            if (words[20] != "$")
            {
                IsValid = false;
                return false;
            }

            IsValid = true;
            return true;
        }
    }
}
