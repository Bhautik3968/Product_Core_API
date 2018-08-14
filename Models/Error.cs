using System;

namespace ProductCoreAPI.Models
{
    public class Error
    {
        public int ID { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDescription { get; set; }
        public int StatusCode {get;set;}
        public string URL {get;set;}
    }
}