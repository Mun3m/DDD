using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VistaClaim.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
