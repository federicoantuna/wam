using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAM.Application.Common.Interfaces;
using WAM.Application.Common.Models;
using WAM.Domain.Entities;

namespace WAM.Application.Packages.Commands
{
    public class CreatePackagesCommand : IRequest<Int32>
    {
        public IEnumerable<PackageDto> PackageDtos { get; set; }
    }

    public class CreatePackagesCommandHandler : IRequestHandler<CreatePackagesCommand, Int32>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public CreatePackagesCommandHandler(IApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }

        public async Task<Int32> Handle(CreatePackagesCommand request, CancellationToken cancellationToken)
        {
            var packages = request.PackageDtos.Select(p => new Package(p.ExternalId, p.Name));

            this._applicationDbContext.Packages.AddRange(packages);

            return await this._applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}