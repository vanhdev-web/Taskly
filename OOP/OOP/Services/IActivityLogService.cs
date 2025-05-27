using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Services
{
    public interface IActivityLogService
    {
        Task LogActivityAsync(
            int? userId,
            string objectType,
            int? objectId,
            string action,
            string details = null);
    }
}
