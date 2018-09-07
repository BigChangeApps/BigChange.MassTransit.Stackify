# BigChange.MassTransit.Stackify
_Enables Stackify tracing for MassTransit_

This package allows you to wrap all received MassTransit messages in Stackify transactions so they can be tracked and profiled.

[![install from nuget](http://img.shields.io/nuget/v/BigChange.MassTransit.Stackify.svg?style=flat-square)](https://www.nuget.org/packages/BigChange.MassTransit.Stackify)
[![downloads](http://img.shields.io/nuget/dt/BigChange.MassTransit.Stackify.svg?style=flat-square)](https://www.nuget.org/packages/BigChange.MassTransit.Stackify)


### Getting Started
`BigChange.MassTransit.Stackify` can be installed via the package manager console by executing the following commandlet:

```powershell

PM> Install-Package BigChange.MassTransit.Stackify

```

or by using the dotnet CLI:
```bash

$ dotnet add package BigChange.MassTransit.Stackify

```

Once the package is installed, we can configure MassTransit

```csharp
var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
{    
    sbc.UseStackify();

    sbc.ReceiveEndpoint(host, "test_queue", ep =>
    {
        // or just configure a eeceive endpoint
        ep.UseStackify();
    });
});

```

### Troubleshooting

1) Remeber to always enable Retrace for non IIS applications (See - https://docs.stackify.com/docs/net-agent-installation-configure-windows-services)

## Contribute

1. Fork
1. Hack!
1. Pull Request
