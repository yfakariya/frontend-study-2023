// Copyright (c) FUJIWARA, Yusuke and all contributors.
// This file is licensed under Apache2 license.
// See the LICENSE in the project root for more information.

using GitHubViewer;
using GitHubViewer.Areas.Identity.Data;
using GitHubViewer.Authentication;
using GitHubViewer.Data;
using GitHubViewer.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("GitHubViewerUserContextConnection") ?? throw new InvalidOperationException("Connection string 'GitHubViewerUserContextConnection' not found.");

builder.Services.AddDbContext<GitHubViewerUserContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<GitHubViewerUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<GitHubViewerUserContext>();

builder.Services.AddAuthentication(
	options =>
	{
		options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	}
).AddGitHub(
	options =>
	{
		options.ClientId = builder.Configuration["GitHub:ClientId"] ?? String.Empty;
		options.ClientSecret = builder.Configuration["GitHub:ClientSecret"] ?? String.Empty;
		options.EnterpriseDomain = builder.Configuration["GitHub:EnterpriseDomain"] ?? String.Empty;
		options.Scope.Add("user:email"); // for ASP.NET Core Identity
		options.Scope.Add("public_repo");
	}
);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

ApplicationSetUp.RegisterServices(
	builder.Services,
	Registration<IBrowserLauncher>.Singleton<NullBrowserLauncher>(),
	Registration<IWindowTitleAccessor>.Singleton<NullWindowTitleAccessor>(),
	Registration<ICredentialsProvider>.Singleton<AspNetIdentityCredentialsProvider>(),
	Registration<IGitHubAuthenticator>.Nothing,
	Registration<AuthenticationStateProvider>.Nothing,
	GenericRegistration<ISessionStorage<ValueTuple>>.Singleton<AspNetSessionStorage<ValueTuple>>()
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

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
