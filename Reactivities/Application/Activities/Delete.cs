using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Delete
    {
        public class Command : IRequest<Unit>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {


                var existingactivity = await _context.Activities.FindAsync(request.Id);

                if (existingactivity != null)
                {
                    _context.Activities.Remove(existingactivity);

                    var isSuccess = await _context.SaveChangesAsync() > 0;

                    if (isSuccess)
                    {
                        return Unit.Value;
                    }
                    else
                    {
                        throw new Exception("Problem saving changes after delete...");
                    }
                }
                else
                {
                    throw new Exception("Entity not found for delete...");
                }
            }
        }
    }
}