using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface ICitiesService : ICrudService<City>
    {
        City GetByNameCity(string name);
    }
}