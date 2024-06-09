using Presentation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayerTests
{
    internal interface ISeeder
    {
        void GenerateUserModels(IUserMasterViewModel viewModel);

        void GenerateProductModels(IProductMasterViewModel viewModel);

        void GenerateStateModels(IStateMasterViewModel viewModel);

        void GenerateEventModels(IEventMasterViewModel viewModel);
    }
}
