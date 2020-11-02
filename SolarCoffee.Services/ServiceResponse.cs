using System;

namespace SolarCoffee.Services.Product {
    public class ServiceReponse<T>{
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public T Data { get; set; }
    }
}