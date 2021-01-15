# RXN.AspNetCore.QuickBookWCSoap2Rest

A simple library was written by C# using with ASP.Net Core that supported communicates with QuickBook Web Connector XML. A product made in RXN Viet Nam.

Once upon ago, QBWC XML only support communication with Web method via SOAP. But now we support the way of converting SOAP to REST for ASP .Net Core server API.

## Installation

Use `Package Manager` to install it.

```bash
Install-Package RXN.AspNetCore.QuickBookWCSoap2Rest -Version 3.0.0
```

Or `.NET cli`

```bash
dotnet add package RXN.AspNetCore.QuickBookWCSoap2Rest --version 3.0.0
```

Or `PackageReference `

```bash
<PackageReference Include="RXN.AspNetCore.QuickBookWCSoap2Rest" Version="3.0.0" />
```

Or `Paket CLI`

```bash
paket add RXN.AspNetCore.QuickBookWCSoap2Rest --version 3.0.0
```

## Usage

There are two interfaces for handling `Request` of .Net core API that `IWCWebMethod` and `IWCWebMethodAsync`.

- Implement from `IWCWebMethod` if you just want the synchronous function.  
- Implement from `IWCWebMethodAsync`if you just want the asynchronous function.  

Example

+ Implement `IWCWebMethod`

```csharp
using RXN.AspNetCore.QuickBookWCSoap2Rest.Interfaces;

// ...

// The handle class take care all business when QBWC requested to server
public class MyRequestHandler : IWCWebMethod
{
    // must implement all methods supporting WC Connector
    // In each method, you can write you business and return the type method need

    public string ServerVersion(string strVersion)
    {
        return _config.GetValue<string>("App:Version");
    }

    public string ClientVersion(string strVersion)
    {
        // maybe save client version or check for update
        return null;
    }

    // ...
}
```

+ Implement `IWCWebMethodAsync`

```csharp
using RXN.AspNetCore.QuickBookWCSoap2Rest.Interfaces;

// ...

// The handle class take care all business when QBWC requested to server
public class MyRequestHandlerAsync : IWCWebMethodAsync
{
    // must implement all methods supporting WC Connector
    // In each method, you can write you business and return the type method need

    public async Task<string> ServerVersionAsync(string strVersion)
    {
        var assembly = System.Reflection.Assembly.GetExecutingAssembly();
        var serviceVersion = assembly.GetName().Version.ToString();

        return serviceVersion;
    }

    public async Task<string> ClientVersionAsync(string strVersion)
    {
        // maybe save client version or check for update
        return null;
    }

    // ...
}
```

Now in your Api Controller call `WCController` and passing WC handler to it.

Example:

```csharp

using Microsoft.AspNetCore.Mvc;
using RXN.AspNetCore.QuickBookWCSoap2Rest;
using RXN.AspNetCore.QuickBookWCSoap2Rest.Interfaces;

[Route("api")]
[ApiController]
[Produces("text/xml")]
public class ApiController
{
    [HttpPost]
    public Task<XElement> Handle()
    {
        var wcController = new WCController(new MyRequestHandlerAsync());

        // Request from AspNetCore <Microsoft.AspNetCore.Http.HttpRequest> Request
        // a property of Controller Base
        // Now all QBWC request will be handle and return at here
        return wcController.HandleAsync(Request);
    }
}


```


## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)

