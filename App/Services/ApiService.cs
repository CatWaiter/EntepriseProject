using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EnterpriseMarketplace.Models;
using Newtonsoft.Json;

namespace EnterpriseMarketplace.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://enterpriseapi183.azurewebsites.net/api/");
        }

        public async Task<User> SignInAsync(string username, string password)
        {
            var signInRequest = new { Username = username, Password = password };
            var response = await _httpClient.PostAsJsonAsync("Users/signin", signInRequest);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<User>(jsonString);
        }

        public async Task RegisterAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync("Users/register", user);
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<Listing>> GetListingsAsync()
        {
            var response = await _httpClient.GetAsync("Listings");
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Listing>>(jsonString);
        }

        public async Task<List<Listing>> GetListingsByUserAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"Listings/user/{userId}");
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Listing>>(jsonString);
        }

        public async Task CreateListingAsync(Listing listing)
        {
            var response = await _httpClient.PostAsJsonAsync("Listings", listing);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteListingAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Listings/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<SavedListing>> GetSavedListingsByUserAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"SavedListings/user/{userId}");
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<SavedListing>>(jsonString);
        }

        public async Task CreateSavedListingAsync(SavedListing savedListing)
        {
            var response = await _httpClient.PostAsJsonAsync("SavedListings", savedListing);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteSavedListingAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"SavedListings/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<User> GetUserAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"Users/{userId}");
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<User>(jsonString);
        }

        public async Task UpdateListingAsync(Listing listing)
        {
            var response = await _httpClient.PutAsJsonAsync($"Listings/{listing.ListingId}", listing);
            response.EnsureSuccessStatusCode();
        }
    }
}