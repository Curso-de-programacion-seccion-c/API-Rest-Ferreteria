using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRestFerreteria.Models
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T data { get; set; }
    }

}