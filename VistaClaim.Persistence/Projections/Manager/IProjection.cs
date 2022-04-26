using System.Threading.Tasks;

namespace VistaClaim.Persistence.Subscriptions.Manager
{
    public interface IProjection
    {
        Task Project(object @event);
    }
}
