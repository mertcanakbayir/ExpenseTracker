﻿using System.Linq.Expressions;
using DAL.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DAL.Concrete
{
    public class ExpenseRepository : BaseRepository<Expense, ExpenseContext>, IExpenseRepository
    {
        public ExpenseRepository(ExpenseContext expenseContext) : base(expenseContext)
        {
        }

        public override List<Expense> GetAll(
           Expression<Func<Expense, bool>> filter = null,
           bool includeInactive = false)
        {
            var query = _expenseContext.Expenses
                .Include(e => e.Category)
                .AsQueryable();

           
            if (!includeInactive)
            {
                query = query.Where(e => e.IsActive);
            }

           
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }

        public List<Expense> GetByCategory(
            Expression<Func<Expense, bool>> filter,
            bool includeInactive = false)
        {
            var query = _expenseContext.Expenses
                .Include(e => e.Category)
                .AsQueryable();

            if (!includeInactive)
            {
                query = query.Where(e => e.IsActive);
            }

            return query.Where(filter).ToList();
        }

        public List<Expense> GetFiltered(Guid? categoryId = null, int? year = null, int? month = null, bool includeInactive = false)
        {
            var query=_expenseContext.Expenses.Include(e => e.Category).AsQueryable();

            if (!includeInactive) { 
            query=query.Where(e => e.IsActive);
            }

            if (categoryId.HasValue)
            {
                query=query.Where(e=>e.CategoryId == categoryId.Value);
            }

            if (year.HasValue) {
            query=query.Where(e=>e.ExpenseDate.Year == year.Value);
            }

            if (month.HasValue) {
            query=query.Where(e=>e.ExpenseDate.Month == month.Value);
            }

            return query.ToList();
        }
    }

}

