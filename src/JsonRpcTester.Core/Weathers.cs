using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonRpcTester.Core
{
    public class Weathers
    {
        public Weathers(
            string city,
            int temperature,
            int humidity,
            string weather)
        {
            City = city;
            Temperature = temperature;
            Humidity = humidity;
            Weather = weather;
        }

        /// <summary>
        /// 都市
        /// </summary>
        public string City { get; }
        /// <summary>
        /// 温度
        /// </summary>
        public int Temperature { get; }
        /// <summary>
        /// 湿度
        /// </summary>
        public int Humidity { get; }
        /// <summary>
        /// 天気
        /// </summary>
        public string Weather { get; }
    }
}
