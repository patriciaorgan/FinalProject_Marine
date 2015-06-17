using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.Interfaces
{
    public interface ISubject
    {
        void RemoveSubcriber(ISubscriber sub);
        void AddSubscriber(ISubscriber sub);
        void NotifySubscribers();
    }
}
