using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.Abstractions.Messages;
using Contract.Services.Business.Share;
using Contract.Services.Event.GetEvents;

namespace Contract.Services.Business.GetById;
public record GetByIdQuery(Guid Id) : IQuery<BusinessResponse>;
