using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest<Unit>
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Category { get; set; }
            public string City { get; set; }
            public string Venue { get; set; }
            public DateTime Date { get; set; }
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
                    existingactivity.Title = request.Title;
                    existingactivity.Description = request.Description;
                    existingactivity.Category = request.Category;
                    existingactivity.City = request.City;
                    existingactivity.Venue = request.Venue;

                    var isSuccess = await _context.SaveChangesAsync() > 0;

                    if (isSuccess)
                    {
                        return Unit.Value;
                    }
                    else
                    {
                        throw new Exception("Problem saving changes after edit...");
                    }
                }
                else
                {
                    throw new Exception("Entity not found for edit...");
                }
            }
        }
    }
}