using IdentityModel;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

using Movies.Client.Handlers;
using Movies.Client.Services;

using Refit;


var builder = WebApplication.CreateBuilder(args);

var movieApiAddress = new Uri(builder.Configuration["ApiSettings:MovieApiAddress"]!);
var identityServerAddress = new Uri(builder.Configuration["ApiSettings:IdentityServerAddress"]!);

builder.Services.AddScoped<MovieApiAuthHeaderHandler>();
builder.Services.AddScoped<AuthenticationDelegatingHandler>();
builder.Services.AddHttpContextAccessor();

builder.Services
    .AddRefitClient<IAuthTokenProvider>()
    .ConfigureHttpClient(c => c.BaseAddress = identityServerAddress);

builder.Services
    .AddRefitClient<IMovieApiService>()
    .ConfigureHttpClient(c => c.BaseAddress = movieApiAddress)
    .AddHttpMessageHandler<AuthenticationDelegatingHandler>();

builder.Services
    .AddHttpClient("IDPClient", client =>
    {
        client.BaseAddress = new Uri("https://localhost:5005/");
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
    });

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
    {
        options.Authority = "http://localhost:5005";
        options.RequireHttpsMetadata = false;

        options.ClientId = "movies_mvc_client";
        options.ClientSecret = "secret";
        options.ResponseType = "code id_token";

        //options.Scope.Add("openid");
        //options.Scope.Add("profile");
        options.Scope.Add("address");
        options.Scope.Add("email");
        options.Scope.Add("roles");

        options.ClaimActions.DeleteClaim("sid");
        options.ClaimActions.DeleteClaim("idp");
        options.ClaimActions.DeleteClaim("s_hash");
        options.ClaimActions.DeleteClaim("auth_time");
        options.ClaimActions.MapUniqueJsonKey("role", "role");

        options.Scope.Add("movieAPI");

        options.SaveTokens = true;
        options.GetClaimsFromUserInfoEndpoint = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = JwtClaimTypes.GivenName,
            RoleClaimType = JwtClaimTypes.Role
        };
    });

//builder.Services.Configure<CookiePolicyOptions>(options =>
//{
//    // Ensures all cookies have the SameSite policy
//    options.MinimumSameSitePolicy = SameSiteMode.None; // This allows cross-site cookies

//    // Automatically mark cookies as Secure if SameSite=None
//    options.Secure = CookieSecurePolicy.Always; // Enforce Secure attribute for cookies
//});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseCookiePolicy();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
