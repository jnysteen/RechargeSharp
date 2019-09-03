﻿using System;
using Newtonsoft.Json;

namespace RechargeSharp.Entities.Discounts
{
    public class Discount
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("value")]
        public long Value { get; set; }

        [JsonProperty("ends_at")]
        public DateTimeOffset EndsAt { get; set; }

        [JsonProperty("starts_at")]
        public DateTimeOffset StartsAt { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("usage_limit")]
        public long UsageLimit { get; set; }

        [JsonProperty("applies_to_id")]
        public long AppliesToId { get; set; }

        [JsonProperty("discount_type")]
        public string DiscountType { get; set; }

        [JsonProperty("applies_to")]
        public string AppliesTo { get; set; }

        [JsonProperty("applies_to_resource")]
        public string AppliesToResource { get; set; }

        [JsonProperty("times_used")]
        public long TimesUsed { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("duration_usage_limit")]
        public long DurationUsageLimit { get; set; }

        [JsonProperty("applies_to_product_type")]
        public string AppliesToProductType { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("once_per_customer")]
        public bool OncePerCustomer { get; set; }
    }
}
