using AssetManagement.Models;
using AssetManagement.Web.Services;
using Microsoft.AspNetCore.Components;

namespace AssetManagement.Web.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        public IMachineService MachineService { get; set; }
        public List<Machine> Machines { get; set; } = new();
        public Dictionary<string, int> Dictionary { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            Machines = await MachineService.GetMachines();
            foreach (var machine in Machines)
            {
                Dictionary.Add(machine.MachineName, 0);
            }
        }

        protected void ButtonClickHandler(int currentIndex)
        {
            if (Dictionary[Machines[currentIndex].MachineName] == 0)
            {
                Dictionary[Machines[currentIndex].MachineName] = 1;
            }
            else
            {
                Dictionary[Machines[currentIndex].MachineName] = 0;
            }
        }
    }
}
