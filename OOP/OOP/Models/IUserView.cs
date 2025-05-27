using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Models
{
    public interface IUserView
    {
        int CurrentUserID { get; }
        void DisplayActivityHistory(string[] activities);
        void DisplayUser(string username, string email, byte[] avatar);
        void DisplayProjects(string[] projectNames);
        string OldPassword { get; }
        string NewPassword { get; }
        string ConfirmPassword { get; }
        void ShowMessage(string message);
    }
}
