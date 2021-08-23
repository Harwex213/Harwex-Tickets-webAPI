namespace Service.Models.City
{
    public class CreateCityModel
    {
        public string Name { get; set; }
        public string Region { get; set; }
    }

    public class CreateCityResponseModel : BaseResponseModel
    {
    }
}