using eShop.BackendServer.Data.Enums;

namespace eShop.BackendServer.Data.Interfaces
{
    public interface ISwitchable
    {
        Status Status { set; get; }
    }
}