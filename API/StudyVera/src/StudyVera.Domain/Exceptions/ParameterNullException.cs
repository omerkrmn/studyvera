using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Exceptions
{
    public class ParameterNullException : Exception
    {
        public ParameterNullException(string? message) : base(message)
        {
        }
    }

}
