namespace api.ViewModel
{
    public class CinemaCreateRequest
    {
        public string Name { get; set; }
        public long CityId { get; set; }
    }
    
    public class CinemaUpdateRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CityId { get; set; }
    }
    
    public class CinemaGetResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CityId { get; set; }
    }

    public class CinemaCreateResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}