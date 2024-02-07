﻿using AssetManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace AssetManagement.Web.Pages
{
    public class QueryBase : ComponentBase
    {
        [Inject]
        public IMachineService MachineService { get; set; }
        public string? AssetName { get; set; }
        public List<string>? MachineNames { get; set; }
        protected async void GetMachinesByAssetButtonHandler()
        {
            try
            {
                if (!string.IsNullOrEmpty(AssetName))
                {
                    MachineNames = (await MachineService.GetMachinesByAssetName(AssetName)).ToList();
                }
            }
            catch (Exception)
            {
                MachineNames = new();
            }
            finally
            {
                StateHasChanged();
            }
        }
    }
}
