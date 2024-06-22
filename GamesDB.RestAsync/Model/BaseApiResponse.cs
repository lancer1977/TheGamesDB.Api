namespace GamesDB.RestAsync.Model
{
    public class BaseApiResponse
    {
        public int Code { get; set; }
        public string Status { get; set; }
        [JsonPropertyName("remaining_monthly_allowance")]
        public int RemainingMonthlyAllowance { get; set; }
        public int ExtraAllowance { get; set; }
    }
}