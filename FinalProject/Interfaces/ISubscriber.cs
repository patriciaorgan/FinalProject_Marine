using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinalProject.Subjects;

namespace FinalProject.Interfaces
{
    public interface ISubscriber
    {
        void update(DataSubject ds);
    }
}
