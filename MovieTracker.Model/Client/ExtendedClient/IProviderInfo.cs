﻿using MovieTracker.Model.ModelEnums;
using MovieTracker.Model.ModelObjects;
using System.Threading;
using System.Threading.Tasks;

namespace MovieTracker.Model.Client.ExtendedClient
{
    public interface IProviderInfo
    {
        Task<ProviderList> GetProvidersAsync(int id, MediaType mediaType, CancellationToken cancellationToken = default(CancellationToken));
    }
}
