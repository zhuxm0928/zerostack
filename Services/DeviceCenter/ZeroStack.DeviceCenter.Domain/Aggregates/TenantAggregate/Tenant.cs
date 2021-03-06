﻿using System;
using System.Collections.Generic;
using ZeroStack.DeviceCenter.Domain.Entities;

namespace ZeroStack.DeviceCenter.Domain.Aggregates.TenantAggregate
{
    public class Tenant : BaseAggregateRoot<Guid>
    {
        public string Name { get; set; } = null!;

        public List<TenantConnectionString> ConnectionStrings { get; protected set; } = new List<TenantConnectionString>();
    }
}
