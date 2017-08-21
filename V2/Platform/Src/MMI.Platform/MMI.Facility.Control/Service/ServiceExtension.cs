using MMI.Facility.Interface.Service;

namespace MMI.Facility.Control.Service
{
    public static class ServiceExtension
    {
        public static IService Regist<T>(this IService service) where T : IService
        {
            ServiceManager.Instance.RegistService<T>(service);
            return service;
        }
    }
}