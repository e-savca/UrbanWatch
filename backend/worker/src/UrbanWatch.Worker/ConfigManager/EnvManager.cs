using System;

namespace UrbanWatch.Worker.ConfigManager;

public class EnvManager
{
    public TranzyApiKeyManager TranzyApiKeyManager { get; }
    private IConfiguration Config { get; }

    public EnvManager(
        IConfiguration config
    )
    {
        TranzyApiKeyManager = new TranzyApiKeyManager(config);
        Config = config;
    }


    public bool IsDevelopment() => Config["ASPNETCORE_ENVIRONMENT"] == "Development";

}
