using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAM.Application.Common.Interfaces;
using WAM.Application.Common.Models;
using WAM.Domain.Entities;
using WAM.Domain.Enums;
using WAM.Domain.ValueObjects;
using Curseforge = WAM.Application.Common.Models.Curseforge;

namespace WAM.Application.Addons.Commands.BulkInstallAddon
{
    public class BlukInstallAddonCommand : IRequest<Result>
    {
        public IEnumerable<String> PackageNames { get; set; }

        public GameVersionFlavor GameVersionFlavor { get; set; }

        public ReleaseType ReleaseType { get; set; }

        public String AddonsDirectory { get; set; }
    }

    public class InstallAddonsCommandHandler : IRequestHandler<BlukInstallAddonCommand, Result>
    {
        private readonly ICurseforgeService _curseforgeService;
        private readonly INetworkService _networkService;
        private readonly IFileSystemService _fileSystemService;
        private readonly IApplicationDbContext _applicationDbContext;

        public InstallAddonsCommandHandler(ICurseforgeService curseforgeService,
            INetworkService networkService,
            IFileSystemService fileSystemService,
            IApplicationDbContext applicationDbContext)
        {
            this._curseforgeService = curseforgeService;
            this._networkService = networkService;
            this._fileSystemService = fileSystemService;
            this._applicationDbContext = applicationDbContext;
        }

        public async Task<Result> Handle(BlukInstallAddonCommand request, CancellationToken cancellationToken)
        {
            var packages = this._applicationDbContext.Packages.Where(p => request.PackageNames.Contains(p.Name));

            var notFoundPackages = request.PackageNames.Except(packages.Select(p => p.Name));

            var getCurseforgeAddonDtos = this._curseforgeService.GetAddonsAsync(packages.Select(p => p.ExternalId));
            var getAddons = this._applicationDbContext.Addons.Include(a => a.Package).ToListAsync(cancellationToken);

            var curseforgeAddonDtos = await getCurseforgeAddonDtos;
            var installedAddons = await getAddons;

            var notInstalledCurseforgeAddonDtos = new List<Curseforge.AddonDto>();
            var alreadyInstalledAddonPacakgeNames = new List<String>();
            var notFoundAddons = new List<String>();

            foreach (var curseforgeAddonDto in curseforgeAddonDtos)
            {
                if (installedAddons.Any(a => a.Package.ExternalId == curseforgeAddonDto.Id && a.GameVersionFlavor == request.GameVersionFlavor))
                {
                    alreadyInstalledAddonPacakgeNames.Add(curseforgeAddonDto.Slug);
                }
                else
                {
                    notInstalledCurseforgeAddonDtos.Add(curseforgeAddonDto);
                }
            }

            var installAddons = new List<Task<Addon>>();

            foreach (var notInstalledCurseforgeAddonDto in notInstalledCurseforgeAddonDtos)
            {
                var fileDto = notInstalledCurseforgeAddonDto.LatestFiles
                    .Where(lf => lf.GameVersionFlavor == request.GameVersionFlavor.ToCurseforgeCode()
                        && lf.ReleaseType <= request.ReleaseType.ToCurseforgeCode())
                    .OrderByDescending(lf => lf.ReleaseType)
                    .FirstOrDefault();

                if (fileDto == null)
                {
                    notFoundAddons.Add(notInstalledCurseforgeAddonDto.Slug);
                }
                else
                {
                    installAddons.Add(this.InstallAddonAsync(fileDto,
                        notInstalledCurseforgeAddonDto.Name,
                        fileDto.Version,
                        packages.Single(p => p.ExternalId == notInstalledCurseforgeAddonDto.Id),
                        request.GameVersionFlavor,
                        request.ReleaseType,
                        request.AddonsDirectory));
                }
            }

            var addons = await Task.WhenAll(installAddons);

            await this._applicationDbContext.Addons.AddRangeAsync(addons);
            _ = await this._applicationDbContext.SaveChangesAsync(cancellationToken);

            return null;
        }

        private async Task<Addon> InstallAddonAsync(Curseforge.FileDto fileDto, String name, String version, Package package, GameVersionFlavor gameVersionFlavor, ReleaseType releaseType, String addonsDirectory)
        {
            var file = await this._networkService.DownloadFileAsync(fileDto.DownloadUrl);

            var moduleNames = this._fileSystemService.ExtractZipFile(file, addonsDirectory);
            var modules = moduleNames.Select(mn => new Module(mn));

            var addon = new Addon(name, version, package, gameVersionFlavor, releaseType);

            addon.AddModules(modules);

            return addon;
        }
    }
}