using GkShp.Core.Messages;

namespace GkShp.Core.Bus
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T @event) where T : Event;
    }
}
