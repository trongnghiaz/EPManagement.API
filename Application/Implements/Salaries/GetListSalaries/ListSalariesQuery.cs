
using Application.Common.Model;
using MediatR;

namespace Application.Implements.Salaries.GetListSalaries
{
    public record ListSalariesQuery : IRequest<List<ListSalariesResponse>>
    {
        public void SetId(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }
    }
    
    
}
