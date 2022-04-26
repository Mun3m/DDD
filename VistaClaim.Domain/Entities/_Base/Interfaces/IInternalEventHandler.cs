using System;
using System.Collections.Generic;
using System.Text;

namespace VistaClaim.Domain.Entities._Base.Interfaces
{
    public interface IInternalEventHandler
    {
        void Handle(object @event);
    }
}
