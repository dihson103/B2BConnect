﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.Abstractions.Dtos.Search;
using Contract.Abstractions.Messages;
using Contract.Services.Business.Share;

namespace Contract.Services.Business.GetBusinesses;
public record GetBusinessesQuery(
    string? SearchTerm,
    bool IsVerified = true,
    int PageIndex = 1,
    int PageSize = 10) : IQuery<SearchResponse<List<BusinessResponse>>>;
