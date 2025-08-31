using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNotificator
{
    public interface IPublisherFactory
    {
        IPublisher<T> GetPublisher<T>() where T : class;
    }
}
