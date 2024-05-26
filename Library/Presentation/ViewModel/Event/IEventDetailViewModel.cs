using Presentation.Model.API;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.ViewModel
{
    public interface IEventDetailViewModel
    {
        static IEventDetailViewModel CreateViewModel(string guid, string stateGuid, string userGuid, DateTime CreatedAt, string type, IEventModelOperation model, IErrorInformer informer)
        {
            return new EventDetailViewModel(guid, stateGuid, userGuid, CreatedAt, type, model, informer);
        }

        ICommand UpdateEvent { get; set; }

        string Guid { get; set; }

        string StateGuid { get; set; }

        string UserGuid { get; set; }

        DateTime CreatedAt { get; set; }

        string Type { get; set; }
    }
}
