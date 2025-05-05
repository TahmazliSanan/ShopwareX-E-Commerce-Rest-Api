namespace ShopwareX.Dtos.GeneralResponse
{
    public class ApiResponse<TResponse>
    {
        public string Message { get; set; } = null!;
        public TResponse? Response { get; set; }
    }
}
