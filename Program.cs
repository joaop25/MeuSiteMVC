using MeuSiteMVC.Configuration;
using MeuSiteMVC.Data;
using MeuSiteMVC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddMvcConfiguration()
    .AddIdentityConfiguration()
    .AddDependencyInjectionConfiguration();

var app = builder.Build();

app.UseMvcConfiguration();

app.Run();