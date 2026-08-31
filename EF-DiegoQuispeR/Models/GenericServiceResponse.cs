using Microsoft.OpenApi.Any;
using System;

namespace EF_DiegoQuispeR.Models
{
    public class GenericServiceResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int Code {  get; set; }
        public object? ThisObject {  get; set; }
    }
}
