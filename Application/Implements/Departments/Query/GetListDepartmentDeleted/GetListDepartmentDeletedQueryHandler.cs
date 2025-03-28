using Application.Common.Handler;
using Application.Common.Interface;
using Application.Common.Model;
using Domain.Entities;
using MediatR;

namespace Application.Implements.Departments.Query.GetListDepartmentDeleted
{
    public class GetListDepartmentDeletedQueryHandler : BaseHandler<GetListDepartmentDeletedQuery, PagedList<DeletedDepartmentModel>>
    {
        private readonly ISender _mediator;
        public GetListDepartmentDeletedQueryHandler(ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
        }
        public override async Task<PagedList<DeletedDepartmentModel>> Handle(GetListDepartmentDeletedQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Department> departmentQuery = _readDbcontext.Department;
            if (!string.IsNullOrWhiteSpace(request.searchTerm))
            {
                departmentQuery = departmentQuery.Where(c => c.DepartmentName.Contains(request.searchTerm));
            }
            var department = departmentQuery
                .Where(d => d.DepartmentId != Guid.Parse("ad75d6f4-7362-4731-8775-bf1a2adeaa0a"))
                .Where(d => d.IsDeleted == true)
                .Select(c => new DeletedDepartmentModel
                {
                    DepartmentId = c.DepartmentId,
                    DepartmentName = c.DepartmentName,
                    Address = c.Address,
                    EstablishDate = c.EstablishDate
                });

            var list = await PagedList<DeletedDepartmentModel>.CreateAsync(department, request.page, request.pageSize);
            return list;
        }
    }
}
