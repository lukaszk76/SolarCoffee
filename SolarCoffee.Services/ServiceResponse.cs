using System;

namespace SolarCoffe.Services {
    public class ServiceReponse{
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public T Data { get; set; }
    }
}