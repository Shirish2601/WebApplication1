﻿using AssetManagement.Models;

namespace AssetManagement.Web.Services
{
    public interface IMachineService
    {
        Task<List<Asset>> GetAssets(string? machineName);
        Task<List<Machine>> GetMachines();
        Task<List<string>> GetMachineThatUsesLatestAsset();
        Task<List<string>> GetMachinesByAssetAndSeries(string? assetName, string? seriesNumber);
    }
}
