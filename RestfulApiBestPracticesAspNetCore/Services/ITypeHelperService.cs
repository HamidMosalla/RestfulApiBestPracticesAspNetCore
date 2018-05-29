namespace RestfulApiBestPracticesAspNetCore.Services
{
    public interface ITypeHelperService
    {
        bool TypeHasProperties<T>(string fields);
    }
}
