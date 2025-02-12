﻿using MovieNetLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MovieNetWCF
{
	//singleton
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class UserService : IUserService
	{
		public static List<UserEntity> GetAll()
		{
			DataModelContainer ctx = new DataModelContainer();
			return ctx.UserEntitySet.ToList();
		}

		public User GetUser(int id)
		{
            UserEntity userEntity = null;

            try
            {
                DataModelContainer ctx = new DataModelContainer();
                userEntity = (from u in ctx.UserEntitySet where u.Id == id select u).FirstOrDefault();
                if (userEntity == null)
                    return null;
            }
            catch (SqlException ex)
            {
                throw new FaultException("Erreur GetUser: " + ex.Errors);
            }
            return TranslateUser(userEntity);
        }

		public User CreateUser(string login, string password)
		{
            UserEntity user = null;

            try
            {
                DataModelContainer ctx = new DataModelContainer();
                var count = (from u in ctx.UserEntitySet where u.Login == login select u).FirstOrDefault();
                if (count != null)
                    return null;
                user = new UserEntity
                {
                    Login = login,
                    Password = password
                };
                ctx.UserEntitySet.Add(user);
                ctx.SaveChanges();
            }
            catch (SqlException ex)
            {
                throw new FaultException("Erreur CreateUser: " + ex.Errors);
            }
			return TranslateUser(user);
		}

		public User UpdateUser(int id, string login, string password)
		{
            UserEntity userEntity = null;

            try
            {
                DataModelContainer ctx = new DataModelContainer();
                userEntity = (from u in ctx.UserEntitySet where u.Id == id select u).FirstOrDefault();
                if (userEntity == null)
                    return null;
                userEntity.Password = password;

                ctx.SaveChanges();
            }
            catch (SqlException ex)
            {
                throw new FaultException("Erreur UpdateUser: " + ex.Errors);
            }

			return TranslateUser(userEntity);
		}

		public Boolean DeleteUser(int id)
		{
            try
            {
                DataModelContainer ctx = new DataModelContainer();
                var userEntity = (from u in ctx.UserEntitySet where u.Id == id select u).FirstOrDefault();
                if (userEntity == null)
                    return false;

                
				foreach (var n in ctx.NoteEntitySet.Where(n=>n.UserEntity.Id == id))
				{
					ctx.NoteEntitySet.Remove(n);
				}
				ctx.UserEntitySet.Remove(userEntity);
				ctx.SaveChanges();
            }
            catch (SqlException ex)
            {
                throw new FaultException("Erreur DeleteUser: " + ex.Errors);
            }

			return true;
		}

		public User LoginUser (string login, string password)
		{
            UserEntity user = null;

            try
            {
				using (DataModelContainer ctx = new DataModelContainer())
				{
					user = (from u in ctx.UserEntitySet where u.Login == login && u.Password == password select u).SingleOrDefault();
                if (user == null)
                    return null;
				}
			}
			catch (SqlException ex)
            {
                throw new FaultException("Erreur LoginUser: " + ex.Errors);
            }

            return TranslateUser(user);
        }
		private User TranslateUser(UserEntity userEntity)
		{
			User user = new User
			{
				Id = userEntity.Id,
				Login = userEntity.Login,
				Password = userEntity.Password
			};
			return user;
		}

		public UserEntity ReverseTranslateUser(User user)
		{
			UserEntity userEntity = new UserEntity
			{
				Id = user.Id,
				Login = user.Login,
				Password = user.Password
			};
			return userEntity;
		}
	}
}
