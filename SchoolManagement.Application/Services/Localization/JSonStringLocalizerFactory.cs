using Ayadty.Data;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

namespace presentationLayer;

public class JSonStringLocalizerFactory:IStringLocalizerFactory
{
    private readonly IDistributedCache _cache;

    public JSonStringLocalizerFactory(IDistributedCache cache)
    {
        _cache = cache;
    }

    public IStringLocalizer Create(Type resourceSource)
    {
        return new JsonStringLocalizer(_cache); 
    }

    public IStringLocalizer Create(string baseName, string location)
    {
        return new JsonStringLocalizer(_cache); 
    }
}