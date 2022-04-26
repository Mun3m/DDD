using System.Threading.Tasks;
using VistaClaim.Domain.Entities._Base;

namespace VistaClaim.Application.Common.Interfaces
{
    public interface IAggregateStore
    {
        Task<bool> Exists<T, TId>(TId aggregateId);
        Task Save<T, TId>(T aggregate) where T : AggregateRoot;
        Task<T> Load<T, TId>(TId aggregateId) where T : AggregateRoot;
    }
}
