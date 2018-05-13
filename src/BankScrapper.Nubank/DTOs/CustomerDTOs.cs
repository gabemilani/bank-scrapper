using Newtonsoft.Json;

namespace BankScrapper.Nubank.DTOs
{
    [JsonObject(IsReference = false)]
    public class CustomerDTO
    {
        [JsonProperty("address_city")]
        public string AddressCity { get; set; }

        [JsonProperty("address_country")]
        public string AddressCountry { get; set; }

        [JsonProperty("address_line1")]
        public string AddressLine1 { get; set; }

        [JsonProperty("address_line2")]
        public string AddressLine2 { get; set; }

        [JsonProperty("address_locality")]
        public string AddressLocality { get; set; }

        [JsonProperty("address_number")]
        public string AddressNumber { get; set; }

        [JsonProperty("address_postcode")]
        public string AddressPostcode { get; set; }

        [JsonProperty("address_state")]
        public string AddressState { get; set; }

        [JsonProperty("billing_address_city")]
        public string BillingAddressCity { get; set; }

        [JsonProperty("billing_address_country")]
        public string BillingAddressCountry { get; set; }

        [JsonProperty("billing_address_line1")]
        public string BillingAddressLine1 { get; set; }

        [JsonProperty("billing_address_line2")]
        public string BillingAddressLine2 { get; set; }

        [JsonProperty("billing_address_locality")]
        public string BillingAddressLocality { get; set; }

        [JsonProperty("billing_address_number")]
        public string BillingAddressNumber { get; set; }

        [JsonProperty("billing_address_postcode")]
        public string BillingAddressPostcode { get; set; }

        [JsonProperty("billing_address_state")]
        public string BillingAddressState { get; set; }

        [JsonProperty("channels")]
        public string[] Channels { get; set; }

        [JsonProperty("cpf")]
        public string CPF { get; set; }

        [JsonProperty("dob")]
        public string DateOfBirth { get; set; }

        [JsonProperty("devices")]
        public DeviceDTO[] Devices { get; set; }

        [JsonProperty("documents")]
        public DocumentDTO[] Documents { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("external_ids")]
        public ExternalIdsDTO ExternalIds { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("invitations")]
        public int Invitations { get; set; }

        [JsonProperty("_links")]
        public CustomerLinksDTO Links { get; set; }

        [JsonProperty("marital_status")]
        public string MaritalStatus { get; set; }

        [JsonProperty("mothers_name")]
        public string MothersName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nationality")]
        public string Nationality { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("preferred_name")]
        public string PreferredName { get; set; }

        [JsonProperty("primary_device")]
        public DeviceDTO PrimaryDevice { get; set; }

        [JsonProperty("printed_name")]
        public string PrintedName { get; set; }

        [JsonProperty("reported_income")]
        public string ReportedIncome { get; set; }
    }

    [JsonObject(IsReference = false)]
    public class CustomerLinksDTO
    {
        [JsonProperty("account_request")]
        public LinkDTO AccountRequest { get; set; }

        [JsonProperty("accounts")]
        public LinkDTO Accounts { get; set; }

        [JsonProperty("activate_card")]
        public LinkDTO ActivateCard { get; set; }

        [JsonProperty("active_certificates")]
        public LinkDTO ActiveCertificates { get; set; }

        [JsonProperty("confirm_mobile")]
        public LinkDTO ConfirmMobile { get; set; }

        [JsonProperty("create_payment_session")]
        public LinkDTO CreatePaymentSession { get; set; }

        [JsonProperty("google_credentials")]
        public LinkDTO GoogleCredentials { get; set; }

        [JsonProperty("invitations")]
        public LinkDTO Invitations { get; set; }

        [JsonProperty("nubank_contact")]
        public LinkDTO NubankContact { get; set; }

        [JsonProperty("primary_device")]
        public LinkDTO PrimaryDevice { get; set; }

        [JsonProperty("revoke_token")]
        public LinkDTO RevokeToken { get; set; }

        [JsonProperty("self")]
        public LinkDTO Self { get; set; }

        [JsonProperty("social_link")]
        public LinkDTO SocialLink { get; set; }

        [JsonProperty("update_address")]
        public LinkDTO UpdateAddress { get; set; }

        [JsonProperty("update_email")]
        public LinkDTO UpdateEmail { get; set; }

        [JsonProperty("update_phone")]
        public LinkDTO UpdatePhone { get; set; }

        [JsonProperty("verify_mobile")]
        public LinkDTO VerifyMobile { get; set; }
    }

    [JsonObject(IsReference = false)]
    public class CustomerResultDTO
    {
        [JsonProperty("customer")]
        public CustomerDTO Customer { get; set; }
    }

    [JsonObject(IsReference = false)]
    public class DeviceDTO
    {
        [JsonProperty("device_id")]
        public string DeviceId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("push_token")]
        public string PushToken { get; set; }

        [JsonProperty("user_agent")]
        public string UserAgent { get; set; }
    }

    [JsonObject(IsReference = false)]
    public class DocumentDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("issuer")]
        public string Issuer { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("uf")]
        public string UF { get; set; }
    }

    [JsonObject(IsReference = false)]
    public class ExternalIdsDTO
    {
        [JsonProperty("foursquare")]
        public string Foursquare { get; set; }
    }
}