using MicroEcomWeb.Entities;
using System.Threading.Tasks;

namespace MicroEcomWeb.Repositories
{
    public interface IContactRepository
    {
        Task<Contact> SendMessage(Contact contact);
        Task<Contact> Subscribe(string address);
    }
}
