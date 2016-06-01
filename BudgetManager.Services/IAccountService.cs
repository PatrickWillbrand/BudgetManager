﻿using System.Threading.Tasks;
using BudgetManager.Domain;

namespace BudgetManager.Services
{
    public interface IAccountService
    {
        Task LoginAsync(Account account, string password);

        Task RegisterAsync(Account account, decimal startAmount);
    }
}