using AssetManagement.Web.Services;
using Microsoft.AspNetCore.Components;

namespace AssetManagement.Web.Pages
{
    public partial class Query
    {
        [Inject]
        public IMachineService MachineService { get; set; }
        public string? AssetName { get; set; }
        public List<string>? MachineNames { get; set; }
        protected async void GetMachinesByAssetButtonHandler()
        {
            try
            {
                if (AssetName != null)
                {
                    AssetName = AssetName.Trim();
                }
                MachineNames = (await MachineService.GetMachinesByAssetName(AssetName)).ToList();
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
