using EventStore.ClientAPI;
using System.Threading.Tasks;

namespace VistaClaim.Persistence.EventStore
{
    public interface ICheckpointStore
    {
        Task<Position> GetCheckpoint();
        Task SotreCheckpoint(Position checkpoint);
    }
}
