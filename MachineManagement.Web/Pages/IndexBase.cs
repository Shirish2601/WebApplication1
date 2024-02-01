using MachineManagement.Models;
using MachineManagement.Web.Services;
using Microsoft.AspNetCore.Components;

namespace MachineManagement.Web.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        public IMachineService MachineService { get; set; }
        public Dictionary<string,List<Asset>> Machines { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            Machines = await MachineService.GetAssets();
        }
    }
}
