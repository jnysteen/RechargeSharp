﻿using System;
using Newtonsoft.Json;
using RechargeSharp.Entities.Customers;

namespace RechargeSharp.Entities.WebhookResponses.Customers
{
    public class CustomerCreatedResponse : IEquatable<CustomerCreatedResponse>
    {
        public bool Equals(CustomerCreatedResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Customer, other.Customer);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CustomerCreatedResponse) obj);
        }

        public override int GetHashCode()
        {
            return (Customer != null ? Customer.GetHashCode() : 0);
        }

        public static bool operator ==(CustomerCreatedResponse left, CustomerCreatedResponse right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CustomerCreatedResponse left, CustomerCreatedResponse right)
        {
            return !Equals(left, right);
        }

        [JsonProperty("customer")]
        public Customer Customer { get; set; }
    }
}
