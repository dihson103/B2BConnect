﻿using Contract.Abstractions.Dtos.Search;
using Contract.Abstractions.Messages;
using Contract.Services.Business.Share;

namespace Contract.Services.Business.GetBusinesses;
public record GetBusinessesQuery(
    string? SearchTerm,
   List<Guid>? IndustryIds, 
    NumberOfEmployee? NumberOfEmployee,
    bool IsVerified = false,
    int PageIndex = 1,
    int PageSize = 10) : IQuery<SearchResponse<List<BusinessesResponse>>>;
