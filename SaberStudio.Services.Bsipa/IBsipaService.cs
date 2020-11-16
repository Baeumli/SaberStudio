using System.Threading.Tasks;

namespace SaberStudio.Services.Bsipa
{
    public interface IBsipaService
    {
        public Task<bool> IsBsipaInstalled();
    }
}