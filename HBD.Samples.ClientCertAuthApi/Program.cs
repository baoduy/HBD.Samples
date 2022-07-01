using HBD.Samples.ClientCertAuthApi.Handlers;
using HBD.Web.Auths.CertAuth;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Server.Kestrel.Https;

var builder = WebApplication.CreateBuilder(args);

//Cert Auth
builder.Services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
    .AddCertAuth<CertAuthHandler>(builder.Configuration,
        new CertAuthConfig
        {
            ClientCertificateMode = ClientCertificateMode.AllowCertificate,
            CertificateForwardingHeader = "x-forwarded-client-cert",
            ConfigureOptions = o =>
            {
                o.AllowedCertificateTypes = CertificateTypes.All;
                //Self-signed cert may not valid this
                o.ValidateCertificateUse = false;
            },
        });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCertAuth();
app.MapControllers();
app.Run();