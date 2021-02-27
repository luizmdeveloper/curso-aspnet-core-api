﻿using Domain.Infraestructure.Notifications;
using Domain.Infraestructure.Repository;
using LuizMario.Domain.Core.Entity;
using LuizMario.Dto.Input;
using System.Collections.Generic;
using System.Data;
using Domain.Infraestructure.Helpers;
using Dapper;
using System.Linq;

namespace LuizMario.Domain.Core.Repository
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(IDbConnection connection, INotification notification) : base(connection, notification)
        {
        }

        public override User FindById(int Id)
        {
            var sql = @" SELECT * FROM users 
                         INNER JOIN profiles ON profiles.Id = users.profileId 
                         WHERE users.Id = @Id ";

            var paramters = new Dictionary<string, object>();
            paramters.Add("id", Id);

            return _connection.Query<User, Profile, User>(
                sql,
                (user, profile) =>
                {
                    user.Profile = profile;
                    return user;
                },
                splitOn: "id",
                param: paramters).FirstOrDefault();
        }

        public User FindByEmailAndPassword(UserInputDto userInput)
        {
            var sql = @" SELECT * FROM users 
                         INNER JOIN profiles ON profiles.Id = users.profileId
                         WHERE UPPER(users.email) = UPPER(@email)
                           AND password = @password ";
            var paramters = new Dictionary<string, object>();
            paramters.Add("email", userInput.Email);
            paramters.Add("password", userInput.Password.GetSHA1());

            return _connection.Query<User, Profile, User>(
                sql, 
                (user, profile) => 
                {
                    user.Profile = profile;
                    return user;
                }, 
                splitOn: "id", 
                param: paramters).FirstOrDefault();
        }
    }
}
