﻿using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RechargeSharp.Entities.Customers;
using RechargeSharp.Entities.Shared;

namespace RechargeSharp.Services.Customers
{
    public class CustomerService : RechargeSharpService
    {
        public CustomerService(string apiKey) : base(apiKey)
        {
        }

        public async Task<CustomerResponse> GetCustomerAsync(string id)
        {
            var response = await GetAsync($"/customers/{id}");
            return JsonConvert.DeserializeObject<CustomerResponse>(
                await response.Content.ReadAsStringAsync());
        }

        private async Task<CustomerListResponse> GetCustomersAsync(string queryParams)
        {
            var response = await GetAsync($"/customers?{queryParams}");
            return JsonConvert.DeserializeObject<CustomerListResponse>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<CustomerListResponse> GetCustomersAsync(int page = 1, int limit = 50, string email = null, string shopifyCustomerId = null, DateTime? createdAtMin = null, DateTime? createAtMax = null, DateTime? updatedAtMin = null, DateTime? updatedAtMax = null, string hash = null)
        {
            var queryParams = $"page={page}&limit={limit}";
            queryParams += email != null ? $"&email={email}" : ""; 
            queryParams += shopifyCustomerId != null ? $"&shopify_customer_id={shopifyCustomerId}" : ""; 
            queryParams += createdAtMin != null ? $"&created_at_min={createdAtMin?.ToString("s")}" : ""; 
            queryParams += createAtMax != null ? $"&created_at_max={createAtMax?.ToString("s")}" : ""; 
            queryParams += updatedAtMin != null ? $"&updated_at_min={updatedAtMin?.ToString("s")}" : ""; 
            queryParams += updatedAtMax != null ? $"&updated_at_max={updatedAtMax?.ToString("s")}" : ""; 
            queryParams += hash != null ? $"&hash={hash}" : "";


            return await GetCustomersAsync(queryParams);
        }

        public async Task<CountResponse> CountCustomersAsync()
        {
            var response = await GetAsync("/customers/count");
            return JsonConvert.DeserializeObject<CountResponse>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<CustomerResponse> CreateCustomerAsync(CreateCustomerRequest createCustomerRequest)
        {
            var response = await PostAsync("/customers", JsonConvert.SerializeObject(createCustomerRequest));
            return JsonConvert.DeserializeObject<CustomerResponse>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<CustomerResponse> UpdateCustomerAsync(string id, UpdateCustomerRequest updateCustomerRequest)
        {
            var response = await PutAsync($"/customers/{id}", JsonConvert.SerializeObject(updateCustomerRequest));
            return JsonConvert.DeserializeObject<CustomerResponse>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<CustomerResponse> UpdateCustomerPaymentTokenAsync(string id, UpdateCustomerPaymentTokenRequest customerPaymentTokenRequest)
        {
            var response = await PutAsync($"/customers/{id}", JsonConvert.SerializeObject(customerPaymentTokenRequest));
            return JsonConvert.DeserializeObject<CustomerResponse>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task DeleteCustomerAsync(string id)
        {
            var response = await DeleteAsync($"/customers/{id}");
        }

        public async Task<PaymentSourceListResponse> GetCustomerPaymentSourcesAsync(string id)
        {
            var response = await GetAsync($"/customers/{id}/payment_sources");
            return JsonConvert.DeserializeObject<PaymentSourceListResponse>(
                await response.Content.ReadAsStringAsync());
        }
    }
}