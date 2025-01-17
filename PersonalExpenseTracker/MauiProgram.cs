﻿using Microsoft.Extensions.Logging;
using PersonalExpenseTracker.Services;
using Radzen;

namespace PersonalExpenseTracker
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<ITransactionService, TransactionService>();
            builder.Services.AddSingleton<IDebtService, DebtService>();
            builder.Services.AddSingleton<ITransactionHelperService, TransactionHelperService>();
            builder.Services.AddSingleton<AuthenticationStateService>();
            builder.Services.AddRadzenComponents();


#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
