﻿using System;
using System.Collections.Generic;
using System.Threading;
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

        public async Task<Customer> GetCustomerAsync(string id)
        {
            var response = await GetAsync($"/customers/{id}").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<CustomerResponse>(
                await response.Content.ReadAsStringAsync().ConfigureAwait(false)).Customer;
        }

        private async Task<IEnumerable<Customer>> GetCustomersAsync(string queryParams)
        {
            var response = await GetAsync($"/customers?{queryParams}").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<CustomerListResponse>(
                await response.Content.ReadAsStringAsync().ConfigureAwait(false)).Customers;
        }

        public Task<IEnumerable<Customer>> GetCustomersAsync(int page = 1, int limit = 50, string email = null, string status = null, string shopifyCustomerId = null, DateTime? createdAtMin = null, DateTime? createAtMax = null, DateTime? updatedAtMin = null, DateTime? updatedAtMax = null, string hash = null)
        {
            var queryParams = $"page={page}&limit={limit}";
            queryParams += email != null ? $"&email={email}" : ""; 
            queryParams += status != null ? $"&status={status}" : "";
            queryParams += shopifyCustomerId != null ? $"&shopify_customer_id={shopifyCustomerId}" : ""; 
            queryParams += createdAtMin != null ? $"&created_at_min={createdAtMin?.ToString("s")}" : ""; 
            queryParams += createAtMax != null ? $"&created_at_max={createAtMax?.ToString("s")}" : ""; 
            queryParams += updatedAtMin != null ? $"&updated_at_min={updatedAtMin?.ToString("s")}" : ""; 
            queryParams += updatedAtMax != null ? $"&updated_at_max={updatedAtMax?.ToString("s")}" : ""; 
            queryParams += hash != null ? $"&hash={hash}" : "";


            return GetCustomersAsync(queryParams);
        }

        public Task<IEnumerable<Customer>> GetAllCustomersWithParamsAsync(string email = null, string status = null, string shopifyCustomerId = null, DateTime? createdAtMin = null, DateTime? createAtMax = null, DateTime? updatedAtMin = null, DateTime? updatedAtMax = null, string hash = null)
        {
            var queryParams = "";
            queryParams += email != null ? $"&email={email}" : "";
            queryParams += status != null ? $"&status={status}" : "";
            queryParams += shopifyCustomerId != null ? $"&shopify_customer_id={shopifyCustomerId}" : "";
            queryParams += createdAtMin != null ? $"&created_at_min={createdAtMin?.ToString("s")}" : "";
            queryParams += createAtMax != null ? $"&created_at_max={createAtMax?.ToString("s")}" : "";
            queryParams += updatedAtMin != null ? $"&updated_at_min={updatedAtMin?.ToString("s")}" : "";
            queryParams += updatedAtMax != null ? $"&updated_at_max={updatedAtMax?.ToString("s")}" : "";
            queryParams += hash != null ? $"&hash={hash}" : "";

            return GetAllCustomersAsync(queryParams);
        }

        private async Task<IEnumerable<Customer>> GetAllCustomersAsync(string queryParams)
        {
            var count = await CountCustomersAsync();

            var taskList = new List<Task<IEnumerable<Customer>>>();

            var pages = Math.Ceiling(Convert.ToDouble(count) / 250d);

            for (int i = 1; i <= Convert.ToInt32(pages); i++)
            {
                taskList.Add(GetCustomersAsync($"page={i}&limit=250" + queryParams));
            }

            var computed = await Task.WhenAll(taskList);

            var result = new List<Customer>();

            foreach (var customers in computed)
            {
                result.AddRange(customers);
            }

            return result;
        }

        public async Task<long> CountCustomersAsync()
        {
            var response = await GetAsync("/customers/count").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<CountResponse>(
                await response.Content.ReadAsStringAsync().ConfigureAwait(false)).Count;
        }

        public async Task<Customer> CreateCustomerAsync(CreateCustomerRequest createCustomerRequest)
        {
            var response = await PostAsync("/customers", JsonConvert.SerializeObject(createCustomerRequest)).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<CustomerResponse>(
                await response.Content.ReadAsStringAsync().ConfigureAwait(false)).Customer;
        }

        public async Task<Customer> UpdateCustomerAsync(string id, UpdateCustomerRequest updateCustomerRequest)
        {
            var response = await PutAsync($"/customers/{id}", JsonConvert.SerializeObject(updateCustomerRequest)).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<CustomerResponse>(
                await response.Content.ReadAsStringAsync().ConfigureAwait(false)).Customer;
        }

        public async Task<Customer> UpdateCustomerPaymentTokenAsync(string id, UpdateCustomerPaymentTokenRequest customerPaymentTokenRequest)
        {
            var response = await PutAsync($"/customers/{id}", JsonConvert.SerializeObject(customerPaymentTokenRequest)).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<CustomerResponse>(
                await response.Content.ReadAsStringAsync().ConfigureAwait(false)).Customer;
        }

        public async Task DeleteCustomerAsync(string id)
        {
            var response = await DeleteAsync($"/customers/{id}").ConfigureAwait(false);
        }

        public async Task<IEnumerable<PaymentSource>> GetCustomerPaymentSourcesAsync(string id)
        {
            var response = await GetAsync($"/customers/{id}/payment_sources").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<PaymentSourceListResponse>(
                await response.Content.ReadAsStringAsync().ConfigureAwait(false)).PaymentSources;
        }
    }
}
