﻿using AssetManagement.Models;

namespace AssetManagement.Api.Repository
{
    public interface IMachineRepository
    {
        public List<Machine> GetMachines();
        public List<Asset> GetAsset(string? machineName);
        public List<string> GetMachinesByAssetName(string? assetName);
        public List<string> GetMachineThatUsesLatestAssets();
    }
}