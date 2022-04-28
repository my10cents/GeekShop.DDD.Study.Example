using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GkShp.Core.Messages
{
    public class Command : Message, IRequest<bool> 
    {
        public DateTime Timestamp { get; set; }
        public ValidationResult ValidationResult { get; set; } = null!;

        public Command()
        {
            Timestamp = DateTime.UtcNow; //utc to global time;
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
