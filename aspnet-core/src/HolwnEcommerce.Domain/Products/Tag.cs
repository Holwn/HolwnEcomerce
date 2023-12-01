using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace HolwnEcommerce.Products
{
    public class Tag : Entity<string>
    {
        public string Name { get; set; }
    }
}