using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAM.Application.Common.Interfaces;
using Curseforge = WAM.Application.Common.Models.Curseforge;

namespace WAM.Application.Addons.Queries.SearchAddons
{
    public class SearchAddonsQuery : IRequest<IEnumerable<Curseforge.AddonDto>>
    {
        private String _searchFilter;

        public String SearchFilter
        {
            get => this._searchFilter;
            set => this._searchFilter = value.Replace(" ", "%20");
        }
    }

    public class SearchAddonsQueryHandler : IRequestHandler<SearchAddonsQuery, IEnumerable<Curseforge.AddonDto>>
    {
        private readonly ICurseforgeService _curseforgeService;

        public SearchAddonsQueryHandler(ICurseforgeService curseforgeService)
        {
            this._curseforgeService = curseforgeService;
        }

        public async Task<IEnumerable<Curseforge.AddonDto>> Handle(SearchAddonsQuery request, CancellationToken cancellationToken)
        {
            if (String.IsNullOrEmpty(request.SearchFilter))
            {
                return Enumerable.Empty<Curseforge.AddonDto>();
            }

            var curseforgeAddons = await this._curseforgeService.SearchAddonsAsync(request.SearchFilter);

            return curseforgeAddons;
        }
    }
}