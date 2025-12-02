using AppHost;
using Scalar.Aspire;

var builder = DistributedApplication.CreateBuilder(args);

#region Ambient Services
var postgres = builder.AddPostgres("postgres")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithImage("postgres:17.5"); // You can use "custom" images too.


var identity = builder.AddMockOidcDevelopmentServer();

#endregion

#region DevTools

var scalarApis = builder.AddScalarApiReference("scalar-apis", 9561, options =>
    {
        options.DisableDefaultProxy();
        options.PreferHttpsEndpoint();
        options.PersistentAuthentication = true;
        options.AllowSelfSignedCertificates();
        options.AddPreferredSecuritySchemes("oauth2")
            .AddAuthorizationCodeFlow("oauth2",
                flow =>
                {
                    flow.WithClientId("aspire-client")
                        .WithClientSecret("super-secret")
                        .WithSelectedScopes("openid", "profile", "email", "roles");
                });

        options.WithOpenApiRoutePattern("/openapi/{documentName}.json");
    })
    .WaitFor(identity);
#endregion

#region Services 

#region Orders.Api

var ordersDb = postgres.AddDatabase("orders");

var ordersApi = builder.AddProject<Projects.Orders_Api>("ordersapi")
    .WithReference(ordersDb)
    .WithEnvironment("identity", () => identity.GetEndpoint("http").Url)
    .WithIdentityOpenIdAuthority(identity)
    .WithIdentityOpenIdBearer(identity)
    .WaitFor(ordersDb)
    .WaitFor(identity);
scalarApis.WithApiReference(ordersApi, options => { options.AddDocument("orders.v1", "Order Processing API"); });
#endregion

#endregion
builder.Build().Run();
