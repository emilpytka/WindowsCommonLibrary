
namespace WindowsCommonLibrary.PCL.Azure
{
    /// <summary>
    /// If you want to have azure mobile service into your PCL please implement this class as 
    /// it presents here: http://us3r.pl/latwiejsza-praca-z-baza-danych-azure-mobile-services/
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceRepository<T>
        where T : IServiceModel
    {

    }
}
