using AssetManagement.Models;
using AssetManagement.Web.Services;
using Microsoft.AspNetCore.Components;

namespace AssetManagement.Web.Pages
{
    public partial class Index : ComponentBase
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

        public void ButtonClickHandler(int currentIndex)
        {
            CheckIfButtonIsClicked[Machines[currentIndex].MachineName] = !CheckIfButtonIsClicked[Machines[currentIndex].MachineName];
        }
    }
}
