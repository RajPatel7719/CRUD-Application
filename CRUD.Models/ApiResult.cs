using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Models
{
    public class ApiResult<T>
    {
        public int StatusCode { get; set; }

        public string? ErrorMessage { get; set; }

        public T? Result { get; set; }
    }
}
