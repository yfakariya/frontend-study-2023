// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

#pragma warning disable CA1852

using System.Reflection;
using AspNet.Security.OAuth.GitHub;
using GitHubViewer;
using GitHubViewer.Authentication;
using GitHubViewer.Identity.Data;
using GitHubViewer.Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("GitHubViewerUserContextConnection") ?? throw new InvalidOperationException("Connection string 'GitHubViewerUserContextConnection' not found.");

builder.Services.AddDbContext<GitHubViewerUserContext>(options => options.UseSqlite(connectionString));

// Based on IdentityServiceCollectionUIExtensions.AddDefaultIdentity of Microsoft.AspNetCore.Identity.UI package.
// Register services without UI to avoid unexpected direct access to UIs which we do not support.
builder.Services.AddAuthentication(
	options =>
	{
		options.DefaultScheme = GitHubAuthenticationDefaults.AuthenticationScheme;
		options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
	}
).AddGitHub(
	// With github registration
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
).AddIdentityCookies();

// Additional registration based on AddDefaultIdentity
builder.Services.AddIdentityCore<IdentityUser>(
	options =>
	{
		options.Stores.MaxLengthForKeys = 128;
	}
).AddDefaultTokenProviders()
.AddSignInManager()
// And register user store
.AddEntityFrameworkStores<GitHubViewerUserContext>();

builder.Services.AddLocalization();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

ApplicationSetUp.RegisterServices(
	builder.Services,
	builder.Configuration,
	Array.Empty<Assembly>(),
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
		"ja-JP",
		"ja",
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
