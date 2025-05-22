using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace BlazorApp.Client.Services
{
    public class UserInfo
    {
        [JsonPropertyName("clientPrincipal")]
        public ClientPrincipal? ClientPrincipal { get; set; }
    }

    public class ClientPrincipal
    {
        [JsonPropertyName("identityProvider")]
        public string? IdentityProvider { get; set; }

        [JsonPropertyName("userId")]
        public string? UserId { get; set; }

        [JsonPropertyName("userDetails")]
        public string? UserDetails { get; set; }

        [JsonPropertyName("userRoles")]
        public List<string> UserRoles { get; set; } = new List<string>();

        [JsonIgnore]
        public bool IsAuthenticated => !string.IsNullOrEmpty(UserId);
    }

    public class StaticWebAppAuthenticationService
    {
        private readonly HttpClient _http;

        public StaticWebAppAuthenticationService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ClientPrincipal> GetUserInfoAsync()
        {
            try
            {
                var response = await _http.GetFromJsonAsync<UserInfo>("/.auth/me");
                return response?.ClientPrincipal ?? new ClientPrincipal();
            }
            catch
            {
                return new ClientPrincipal();
            }
        }
    }
}