namespace SpaceFlightNews.DTO
{
    public class ResponseHttp<T>
    {
        public T Item { get; set; }
        public bool IsSuccessStatusCode { get; set; }
    }
}