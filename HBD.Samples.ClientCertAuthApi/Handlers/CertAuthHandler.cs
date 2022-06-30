using HBD.Web.Auths.CertAuth;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.Extensions.Options;

namespace HBD.Samples.ClientCertAuthApi.Handlers;

public class CertAuthHandler : DefaultCertificateAuthenticationEvents
{
    public CertAuthHandler(IOptions<CertAuthOptions> certAuthOptions,
        ILogger<DefaultCertificateAuthenticationEvents> logger) : base(certAuthOptions, logger)
    {
    }

    public override Task AuthenticationFailed(CertificateAuthenticationFailedContext context)
    {
        return base.AuthenticationFailed(context);
    }
}