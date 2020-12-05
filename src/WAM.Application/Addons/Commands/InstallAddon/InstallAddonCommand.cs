using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAM.Application.Common.Interfaces;
using WAM.Domain.Entities;
using WAM.Domain.Enums;
using WAM.Domain.ValueObjects;

namespace WAM.Application.Addons.Commands.InstallAddon
{
    public class InstallAddonCommand : IRequest<Boolean>
    {
        public String Name { get; set; }

        public String Version { get; set; }

        public GameVersionFlavor GameVersionFlavor { get; set; }

        public ReleaseType ReleaseType { get; set; }

        public Package Package { get; set; }

        public Int32 ExternalId { get; set; }

        public String DownloadUrl { get; set; }

        public String AddonsDirectory { get; set; }
    }

    public class InstallAddonCommandHandler : IRequestHandler<InstallAddonCommand, Boolean>
    {
        private readonly INetworkService _networkService;
        private readonly IFileSystemService _fileSystemService;
        private readonly IApplicationDbContext _applicationDbContext;

        public InstallAddonCommandHandler(INetworkService networkService,
            IFileSystemService fileSystemService,
            IApplicationDbContext applicationDbContext)
        {
            this._networkService = networkService;
            this._fileSystemService = fileSystemService;
            this._applicationDbContext = applicationDbContext;
        }

        public async Task<Boolean> Handle(InstallAddonCommand request, CancellationToken cancellationToken)
        {
            var addon = await this._applicationDbContext.Addons
                .Include(a => a.Package)
                .SingleOrDefaultAsync(a =>
                    a.GameVersionFlavor == request.GameVersionFlavor
                    && a.Package.ExternalId == request.ExternalId);

            if (addon != null)
            {
                return false;
            }

            var file = await this._networkService.DownloadFileAsync(request.DownloadUrl);

            var moduleNames = this._fileSystemService.ExtractZipFile(file, request.AddonsDirectory);
            var modules = moduleNames.Select(mn => new Module(mn));

            addon = new Addon(request.Name, request.Version, request.Package, request.GameVersionFlavor, request.ReleaseType);

            addon.AddModules(modules);

            _ = this._applicationDbContext.Addons.Add(addon);

            _ = await this._applicationDbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}