﻿using Newtonsoft.Json;

namespace RechargeSharp.Entities.PaymentMethods
{
    public partial class PaymentMethodsResponse
    {
        [JsonProperty("payment_methods")]
        public List<PaymentMethod>? PaymentMethodsPaymentMethods { get; set; }
    }
}
