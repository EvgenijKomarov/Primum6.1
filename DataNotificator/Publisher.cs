using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNotificator
{
    public interface IPublisher<T> where T : class
    {
        void Publish(T message);
        Task PublishAsync(T message);
    }
}
