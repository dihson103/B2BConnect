﻿using Contract.Abstractions.Messages;

namespace Contract.Services.Tests;
public record TestDomainEvent(Guid Id, string Message) : IDomainEvent;
