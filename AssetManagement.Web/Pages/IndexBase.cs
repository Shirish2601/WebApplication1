﻿using AssetManagement.Models;
using AssetManagement.Web.Services;
using Microsoft.AspNetCore.Components;

namespace AssetManagement.Web.Pages
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