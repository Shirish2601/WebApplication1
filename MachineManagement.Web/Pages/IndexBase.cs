using MachineManagement.Models;
using MachineManagement.Web.Services;
using Microsoft.AspNetCore.Components;

namespace MachineManagement.Web.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        public IMachineService MachineService { get; set; }
        public List<Machine> Machines { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            Machines = await MachineService.GetMachines();
        }
        protected void SortHandler()
        {
        }
    }
}
