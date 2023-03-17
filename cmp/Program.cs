using EM.Domain.Conexao;
using EM.Domain.Entidades;
using EM.Domain.Enums;
using EM.Domain.Services;
using EM.Repository;
using EM.Web.Controllers;
using EM.Web.Models;
using System.Collections.Generic;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<RepositorioAluno>();

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Alunos}/{action=Index}/{matricula?}");

app.Run();
