// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

#pragma warning disable CA1852

using AspNet.Security.OAuth.GitHub;
using GitHubViewer;
using GitHubViewer.Authentication;
using GitHubViewer.Data;
using GitHubViewer.Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("GitHubViewerUserContextConnection") ?? throw new InvalidOperationException("Connection string 'GitHubViewerUserContextConnection' not found.");

builder.Services.AddDbContext<GitHubViewerUserContext>(options => options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<GitHubViewerUserContext>();

builder.Services.AddAuthentication(
	options =>
	{
		options.DefaultScheme = GitHubAuthenticationDefaults.AuthenticationScheme;
	}
).AddGitHub(
	options =>
	{
		options.ClientId = builder.Configuration["GitHub:ClientId"] ?? String.Empty;
		options.ClientSecret = builder.Configuration["GitHub:ClientSecret"] ?? String.Empty;
		options.EnterpriseDomain = builder.Configuration["GitHub:EnterpriseDomain"] ?? String.Empty;

		options.CallbackPath = Uris.ServerCallbackPath;

		options.SaveTokens = true;

		options.Scope.Add("user:email"); // for ASP.NET Core Identity
		options.Scope.Add("public_repo");
	}
);

builder.Services.AddLocalization();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

ApplicationSetUp.RegisterServices(
	builder.Services,
	builder.Configuration,
	Registration<IBrowserLauncher>.Singleton<NullBrowserLauncher>(),
	Registration<IWindowTitleAccessor>.Singleton<NullWindowTitleAccessor>(),
	Registration<ICredentialsProvider>.Scoped<AspNetIdentityCredentialsProvider>(),
	Registration<IGitHubAuthenticator>.Nothing,
	Registration<AuthenticationStateProvider>.Nothing,
	GenericRegistration<ISessionStorage<ValueTuple>>.Scoped<ProtectedBrowserSessionStorage<ValueTuple>>()
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseStaticFiles();

var supportedCultures =
	new[]
	{
		"en-US",
		"ja-JP"
	};

app.UseRequestLocalization(
	new RequestLocalizationOptions()
	.SetDefaultCulture(supportedCultures.First())
	.AddSupportedCultures(supportedCultures)
	.AddSupportedUICultures(supportedCultures)
);

app.UseRouting();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
