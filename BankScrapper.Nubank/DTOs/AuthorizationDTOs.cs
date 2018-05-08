using Newtonsoft.Json;
using System;

namespace BankScrapper.Nubank.DTOs
{
    [JsonObject(IsReference = false)]
    public class AuthorizationLinksDTO
    {
        [JsonProperty("account")]
        public LinkDTO Account { get; set; }

        [JsonProperty("account_simple")]
        public LinkDTO AccountSimple { get; set; }

        [JsonProperty("app_flows")]
        public LinkDTO AppFlows { get; set; }

        [JsonProperty("bills_summary")]
        public LinkDTO BillsSummary { get; set; }

        [JsonProperty("change_password")]
        public LinkDTO ChangePassword { get; set; }

        [JsonProperty("customer")]
        public LinkDTO Customer { get; set; }

        [JsonProperty("dropman")]
        public LinkDTO Dropman { get; set; }

        [JsonProperty("enabled_features")]
        public LinkDTO EnabledFeatures { get; set; }

        [JsonProperty("events")]
        public LinkDTO Events { get; set; }

        [JsonProperty("events_page")]
        public LinkDTO EventsPage { get; set; }

        [JsonProperty("faq_push_back_state")]
        public LinkDTO FaqPushBackState { get; set; }

        [JsonProperty("features_map")]
        public LinkDTO FeaturesMap { get; set; }

        [JsonProperty("ghostflame")]
        public LinkDTO GhostFlame { get; set; }

        [JsonProperty("healthcheck")]
        public LinkDTO HealthCheck { get; set; }

        [JsonProperty("postcode")]
        public LinkDTO Postcode { get; set; }

        [JsonProperty("purchases")]
        public LinkDTO Purchases { get; set; }

        [JsonProperty("revoke_all")]
        public LinkDTO RevokeAll { get; set; }

        [JsonProperty("revoke_token")]
        public LinkDTO RevokeToken { get; set; }

        [JsonProperty("savings_account")]
        public LinkDTO SavingsAccount { get; set; }

        [JsonProperty("shore")]
        public LinkDTO Shore { get; set; }

        [JsonProperty("user_change_password")]
        public LinkDTO UserChangePassword { get; set; }

        [JsonProperty("userinfo")]
        public LinkDTO UserInfo { get; set; }
    }

    [JsonObject(IsReference = false)]
    public class AuthorizationResultDTO
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("_links")]
        public AuthorizationLinksDTO Links { get; set; }

        [JsonProperty("refresh_before")]
        public DateTime RefreshBefore { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}