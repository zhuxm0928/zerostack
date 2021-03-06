﻿using System;
using ZeroStack.DeviceCenter.Domain.Entities;

namespace ZeroStack.DeviceCenter.Domain.Aggregates.TenantAggregate
{
    public class TenantConnectionString : BaseEntity
    {
        public virtual Guid TenantId { get; set; }

        public virtual string Name { get; set; } = null!;

        public virtual string Value { get; set; } = null!;

        public override object[] GetKeys()
        {
            return new object[] { TenantId, Name };
        }
    }
}
