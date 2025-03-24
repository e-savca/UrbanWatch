namespace UrbanWatch.Worker.ConfigManager;

public class TranzyApiKeyManager
{
    private const string TRANZY_API_KEY_DEV01 = "TRANZY_API_KEY_DEV01";
    private const string TRANZY_API_KEY_PROD01 = "TRANZY_API_KEY_PROD01";
    private const string TRANZY_API_KEY_PROD02 = "TRANZY_API_KEY_PROD02";
    private const string TRANZY_API_KEY_PROD03 = "TRANZY_API_KEY_PROD03";
    private readonly IConfiguration _config;
    private readonly List<string> _apiKeys = new List<string>();
    private int _currentKeyIndex = 0;

    public TranzyApiKeyManager(IConfiguration config)
    {
        _config = config;
        Initialize();
    }

    private void Initialize()
    {
        if (IsDevelopment())
        {
            _apiKeys.Add(_config[TRANZY_API_KEY_DEV01]);
        }
        else
        {
            _apiKeys.Add(_config[TRANZY_API_KEY_PROD01]);
            _apiKeys.Add(_config[TRANZY_API_KEY_PROD02]);
            _apiKeys.Add(_config[TRANZY_API_KEY_PROD03]);
        }
        
    }

    private bool IsDevelopment() => _config["ASPNETCORE_ENVIRONMENT"] == "Development";
    
    public string GetCurrentKey() => _apiKeys[_currentKeyIndex];

    public bool TrySwitchKey()
    {
        if (IsDevelopment()) return false;
        if (_currentKeyIndex + 1 < _apiKeys.Count)
        {
            _currentKeyIndex++;
            return true;
        }
        else
        {
            _currentKeyIndex = 0;
            return true;
        }
    }
}
