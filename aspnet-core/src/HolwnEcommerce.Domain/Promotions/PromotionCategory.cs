using System;
using Volo.Abp.Domain.Entities;

namespace HolwnEcommerce.Promotions
{
    public class PromotionCategory : Entity<Guid>
    {
        public Guid CategoryId { get; set; }
        public Guid PromotionId { get; set; }
    }
}