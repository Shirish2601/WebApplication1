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
        public Dictionary<string, bool> CheckIfButtonIsClicked { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            Machines = await MachineService.GetMachines();
            foreach (var machine in Machines)
            {
                CheckIfButtonIsClicked.Add(machine.MachineName, false);
            }
        }

        protected void ButtonClickHandler(int currentIndex)
        {
            if (!CheckIfButtonIsClicked[Machines[currentIndex].MachineName])
            {
                CheckIfButtonIsClicked[Machines[currentIndex].MachineName] = true;
            }
            else
            {
                CheckIfButtonIsClicked[Machines[currentIndex].MachineName] = false;
            }
        }
    }
}
