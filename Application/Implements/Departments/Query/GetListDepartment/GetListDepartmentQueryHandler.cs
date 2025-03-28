using Application.Common.Handler;
using Application.Common.Interface;
using Application.Common.Model;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Implements.Departments.Query.GetListDepartment
{
    public class GetListDepartmentQueryHandler : BaseHandler<GetListDepartmentQuery, PagedList<DepartmentQueryModel>>
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public GetListDepartmentQueryHandler(ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext, IMapper mapper) : base(writeDbcontext, readDbcontext)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        public override async Task<PagedList<DepartmentQueryModel>> Handle(GetListDepartmentQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Department> departmentQuery = _readDbcontext.Department;
            if (!string.IsNullOrWhiteSpace(request.searchTerm))
            {
                departmentQuery = departmentQuery.Where(c => c.DepartmentName.Contains(request.searchTerm));
            }
            if(request.sortOrder?.ToLower() == "desc")
            {
                departmentQuery = departmentQuery.OrderByDescending(GetSortProperty(request));
            }
            else
            {
                departmentQuery = departmentQuery.OrderBy(GetSortProperty(request));
            }
            var department = departmentQuery
                .Where(d => d.DepartmentId != Guid.Parse("ad75d6f4-7362-4731-8775-bf1a2adeaa0a"))
                .Where(d => d.IsDeleted == false)
                .Select(c => new DepartmentQueryModel
                {
                    DepartmentId = c.DepartmentId,
                    DepartmentName = c.DepartmentName,
                    Address = c.Address,
                    EstablishDate = c.EstablishDate
                });                                
            var list = await PagedList<DepartmentQueryModel>.CreateAsync(department, request.page, request.pageSize);
            return list;
        }
        private static Expression<Func<Department, object>> GetSortProperty(GetListDepartmentQuery request) =>
        request.sortColumn?.ToLower() switch
        {
            "departmentname" => department => department.DepartmentName,                        
            "establishdate" => department => department.EstablishDate,
            _ => department => department.EstablishDate
        };
    }
}
