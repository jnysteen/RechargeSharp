﻿using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RechargeSharp.Entities.Addresses;
using RechargeSharp.Entities.Checkouts;
using RechargeSharp.Entities.Shared;

namespace RechargeSharp.Services.Checkouts
{
    public class CheckoutService : RechargeSharpService
    {
        protected CheckoutService(string apiKey) : base(apiKey)
        {
        }
        public async Task<CheckoutResponse> GetCheckoutAsync(string id)
        {
            var response = await GetAsync($"/checkouts/{id}");
            return JsonConvert.DeserializeObject<CheckoutResponse>(
                await response.Content.ReadAsStringAsync());
        }



        public async Task<CheckoutResponse> CreateCheckoutAsync(CreateCheckoutRequest createCheckoutRequest)
        {
            var response = await PostAsync("/checkouts", JsonConvert.SerializeObject(createCheckoutRequest));
            return JsonConvert.DeserializeObject<CheckoutResponse>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<CheckoutResponse> UpdateCheckoutAsync(string id, UpdateCheckoutRequest updateCheckoutRequest)
        {
            var response = await PutAsync($"/checkouts/{id}", JsonConvert.SerializeObject(updateCheckoutRequest));
            return JsonConvert.DeserializeObject<CheckoutResponse>(
                await response.Content.ReadAsStringAsync());
        }
        public async Task<ShippingRateListResponse> RetrieveShippingRatesAsync(string id, OverrideShippingLinesRequest overrideShippingLinesRequest)
        {
            var response = await GetAsync($"/checkouts/{id}/shipping_rates");
            return JsonConvert.DeserializeObject<ShippingRateListResponse>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<ProcessCheckoutResponse> ProcessCheckoutAsync(string token, ProcessCheckoutRequest processCheckoutRequest)
        {
            var response = await PostAsync("/checkouts/validate", JsonConvert.SerializeObject(processCheckoutRequest));
            return JsonConvert.DeserializeObject<ProcessCheckoutResponse>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task DeleteCheckoutAsync(string id)
        {
            var response = await DeleteAsync($"/checkouts/{id}");
        }
    }
}