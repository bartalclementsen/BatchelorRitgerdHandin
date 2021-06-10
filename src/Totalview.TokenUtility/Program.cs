using IdentityModel.OidcClient;
using System;
using System.Threading.Tasks;

namespace Totalview.TokenUtility
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            string authority = args?.Length > 0 ? args[0] : "https://localhost:5001";

            await SignIn(authority);
        }

        private static async Task SignIn(string authority)
        {
            try
            {
                Console.WriteLine($"Trying to signin with authority {authority}");

                SystemBrowser browser = new();
                string redirectUri = string.Format($"http://127.0.0.1:{browser.Port}");

                OidcClientOptions options = new()
                {
                    Authority = authority,
                    ClientId = "totalview-prototype-blazor-client",
                    ClientSecret = "secret",
                    RedirectUri = redirectUri,
                    Scope = "openid profile totalview-server",
                    Browser = browser
                };
                OidcClient oidcClient = new(options);

                Console.WriteLine($"Please login in the browser. Waiting for callback...");
                LoginResult result = await oidcClient.LoginAsync(new LoginRequest());

                if (result.IsError)
                {
                    Console.Error.WriteLine($"Could not login {result.Error}");
                    return;
                }

                Console.WriteLine($"Identity Token: {result.IdentityToken}");
                Console.WriteLine($"Access Token:   {result.AccessToken}");
                Console.WriteLine($"Refresh Token:  {result?.RefreshToken ?? "none"}");

                Console.WriteLine("\nClaims:");
                foreach (var claim in result.User.Claims)
                {
                    Console.WriteLine("    {0}: {1}", claim.Type, claim.Value);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Could not login. Ex {ex}");
            }

            Console.WriteLine("Press return to exit...");
            Console.ReadLine();
        }
    }
}
