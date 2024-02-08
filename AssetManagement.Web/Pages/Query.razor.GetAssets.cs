using AssetManagement.Models;

namespace AssetManagement.Web.Pages
{
    public partial class Query
    {
        public string MachineName { get; set; }
        public List<Asset> Assets { get; set; }
        protected async void GetAssetsByMachineNameButtonHandler()
        {
            try
            {
                if (MachineName != null)
                {
                    MachineName = MachineName.Trim();
                }
                Assets = await MachineService.GetAssets(MachineName);
            }
            catch (Exception)
            {
                Assets = new();
            }
            finally
            {
                StateHasChanged();
            }
        }
    }
}
