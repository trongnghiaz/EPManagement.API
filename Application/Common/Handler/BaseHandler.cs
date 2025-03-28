using Application.Common.Interface;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Handler
{
    public abstract class BaseHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        protected readonly IManageWriteDbContext _writeDbcontext;
        protected readonly IManageReadDbContext _readDbcontext;
        

        protected BaseHandler(IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext)
        {
            _writeDbcontext = writeDbcontext;
            _readDbcontext = readDbcontext;
            
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
        
    }
}
