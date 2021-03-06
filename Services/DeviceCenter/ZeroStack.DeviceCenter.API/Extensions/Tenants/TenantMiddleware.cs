﻿using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.API.Constants;
using ZeroStack.DeviceCenter.Domain.Aggregates.TenantAggregate;

namespace ZeroStack.DeviceCenter.API.Extensions.Tenants
{
    public class TenantMiddleware : IMiddleware
    {
        private readonly ICurrentTenant _currentTenant;

        public TenantMiddleware(ICurrentTenant currentTenant) => _currentTenant = currentTenant;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            string? tenantIdString = null;

            if (context.Request.Headers.TryGetValue(TenantConstants.TenantId, out var headerTenantIds))
            {
                tenantIdString = headerTenantIds.First();
            }

            if (context.Request.Query.TryGetValue(TenantConstants.TenantId, out var queryTenantIds))
            {
                tenantIdString = queryTenantIds.First();
            }

            if (context.Request.Cookies.TryGetValue(TenantConstants.TenantId, out var cookieTenantId))
            {
                tenantIdString = cookieTenantId;
            }

            if (context.Request.RouteValues.TryGetValue(TenantConstants.TenantId, out var routeTenantId))
            {
                tenantIdString = routeTenantId?.ToString();
            }

            tenantIdString ??= context.User.FindFirst(TenantConstants.TenantId)?.Value;

            Guid? currentTenantId = null;

            if (!string.IsNullOrWhiteSpace(tenantIdString))
            {
                currentTenantId = Guid.Parse(tenantIdString);
            }

            using (_currentTenant.Change(currentTenantId))
            {
                await next(context);
            }
        }
    }
}
