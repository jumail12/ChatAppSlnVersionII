using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Shared.ExceptionHandler
{
    public class ExceHandlers
    {
        public class BadRequestException : Exception
        {
            public BadRequestException(string message) : base(message) { }
        }

        public class NoContentException : Exception
        {
            public NoContentException(string message) : base(message) { }
        }

        public class ValidationExceptionBase : Exception
        {
            public IDictionary<string, string[]> Errors { get; }

            public ValidationExceptionBase(string message) : base(message)
            {
                Errors = new Dictionary<string, string[]>();
            }

            public ValidationExceptionBase(IEnumerable<ValidationFailure> failures, string message) : base(message)
            {
                Errors = failures
                    .GroupBy(f => f.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(f => f.ErrorMessage).ToArray()
                    );
            }
        }

        public class _ValidationException<T> : ValidationExceptionBase
        {
            public _ValidationException(IEnumerable<ValidationFailure> failures)
                : base(failures, $"Validation failed for type {typeof(T).Name}")
            {
            }

            public _ValidationException(string message)
                : base(message)
            {
            }
        }



    }
}
