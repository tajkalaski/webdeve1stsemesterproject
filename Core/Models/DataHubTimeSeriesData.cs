
using System;

namespace RespaunceV2.Core.Models
{
    public class DataHubTimeSeriesData
    {
        public string MeteringPoint { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public double Usage { get; set; }
        public string Unit { get; set; }
        public string Type { get; set; }
    }
}