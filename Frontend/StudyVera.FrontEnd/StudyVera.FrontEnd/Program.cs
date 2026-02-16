using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StudyVera.FrontEnd;
using StudyVera.FrontEnd.Providers;
using StudyVera.FrontEnd.Services;
using StudyVera.FrontEnd.Services.Concrats;
using Microsoft.AspNetCore.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient
{
    //BaseAddress = new Uri("https://api.studyvera.tech/api/")
    //BaseAddress = new Uri("https://localhost:7126/api/")
});






// 🔹 Authentication ve LocalStorage servisleri
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<CustomAuthStateProvider>();

// 🔹 Diğer servisler
builder.Services.AddScoped<TopicService>();
builder.Services.AddScoped<IUserHistoryService, UserHistoryService>();
builder.Services.AddScoped<IUserLessonProgressService, UserLessonProgressService>();
builder.Services.AddScoped<IUserQuestionStatsService, UserQuestionStatsService>();
builder.Services.AddScoped<ILessonScheduleService, LessonScheduleService>();    
builder.Services.AddScoped<IQuestionStatDetailService,QuestionStatDetailService>();
builder.Services.AddScoped<IProfileSummaryService, ProfileSummaryService>();
builder.Services.AddScoped<IProfileStatService, ProfileStatService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<IUserWeeklyGoalService, UserWeeklyGoalService>();
builder.Services.AddScoped<IAuthService, AuthService>();

await builder.Build().RunAsync();