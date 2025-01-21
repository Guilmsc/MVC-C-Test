using System;

namespace WebMvc.Services.Exceptions
{
    public class DbConcurrencyException : ApplicationException
    {
        public DbConcurrencyException(string message) : base(message) { }
        
    }
}
