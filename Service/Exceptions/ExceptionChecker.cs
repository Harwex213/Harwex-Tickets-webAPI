using Domain.Base;

namespace Service.Exceptions
{
    public static class ExceptionChecker
    {
        public static void CheckEntityOnNull(IEntityBase entity)
        {
            if (entity == null)
            {
                throw new NotFoundException();
            }
        }
    }
}