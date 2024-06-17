﻿namespace Application.Abstractions.Services;
public interface IRedisService
{
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
            where T : class;

    Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default)
        where T : class;

    Task RemoveAsync(string key, CancellationToken cancellationToken = default);

    Task RemoveByPrefixAsync(string prefix, CancellationToken cancellationToken = default);
}
