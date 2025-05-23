﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RO.DevTest.Application.Features.User.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.User.Queries.GetUsersQuery;

public class GetUsersQueryHandler(UserManager<Domain.Entities.User> userManager, bool test = false):IRequestHandler<GetUsersQuery, GetUsersQueryResult>
{
    public async Task<GetUsersQueryResult> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var query = userManager.Users.AsQueryable();

        if(!string.IsNullOrEmpty(request.SearchTerm))
        {
            query = query.Where(u => u.UserName.Contains(request.SearchTerm) ||
            u.Email.Contains(request.SearchTerm));
        }

        query = request.SortBy?.ToLower() switch
        {
            "UserName" => request.IsAscending ? query.OrderBy(u => u.UserName) : query.OrderByDescending(u => u.UserName),
            "Email" => request.IsAscending ? query.OrderBy(u => u.Email) : query.OrderByDescending(u => u.Email),
            _ => query.OrderBy(u => u.UserName)
        };

        int totalCount;
        List<UserDTO> items;

        if (!test){
            totalCount = await query.CountAsync(cancellationToken);

            items = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,

                })
                .ToListAsync(cancellationToken);
        }
        else
        {
            totalCount = query.Count();

            items = query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                })
                .ToList();
        }

            return new GetUsersQueryResult
            {
                TotalCount = totalCount,
                Users = items,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize),
            };
    }

}
