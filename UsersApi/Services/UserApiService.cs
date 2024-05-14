using Microsoft.AspNetCore.Http;
using UsersApi.Models;
using UsersApiData;
using Crud;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;

namespace UsersApi.Services
{
	public class UserApiService : UserService.UserServiceBase
	{
		UserContext db;
		public UserApiService(UserContext db)
		{
			this.db = db;
		}
		// отправляем список пользователей
		public override Task<ListReply> ListUsers(Empty request, ServerCallContext context)
		{
			var listReply = new ListReply();    // определяем список
												// преобразуем каждый объект User в объект UserReply
			var userList = db.Users.Select(item => new UserReply { Id = item.Id, Name = item.Name, Phone = item.Phone, Address = item.Address }).ToList();
			listReply.Users.AddRange(userList);
			return Task.FromResult(listReply);
		}
		// отправляем одного пользователя по id
		public override async Task<UserReply> GetUser(GetUserRequest request, ServerCallContext context)
		{
			var user = await db.Users.FindAsync(request.Id);
			// если пользователь не найден, генерируем исключение
			if (user == null)
			{
				throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
			}
			UserReply userReply = new UserReply() { Id = user.Id, Name = user.Name, Phone = user.Phone, Address = user.Address };
			return await Task.FromResult(userReply);
		}
		// добавление пользователя
		public override async Task<UserReply> CreateUser(CreateUserRequest request, ServerCallContext context)
		{
			// формируем из данных объект User и добавляем его в список users
			var user = new User { Name = request.Name, Phone = request.Phone, Address = request.Address };
			await db.Users.AddAsync(user);
			await db.SaveChangesAsync();
			var reply = new UserReply() { Id = user.Id, Name = user.Name, Phone = user.Phone, Address = user.Address };
			return await Task.FromResult(reply);
		}
		// обновление пользователя
		public override async Task<UserReply> UpdateUser(UpdateUserRequest request, ServerCallContext context)
		{
			var user = await db.Users.FindAsync(request.Id);
			if (user == null)
			{
				throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
			}
			// обновляем даннные
			user.Name = request.Name;
			user.Phone = request.Phone;
			user.Address = request.Address;
			await db.SaveChangesAsync();
			var reply = new UserReply() { Id = user.Id, Name = user.Name, Phone = user.Phone, Address = user.Address };
			return await Task.FromResult(reply);
		}
		// удаление пользователя
		public override async Task<UserReply> DeleteUser(DeleteUserRequest request, ServerCallContext context)
		{
			var user = await db.Users.FindAsync(request.Id);
			if (user == null)
			{
				throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
			}
			// удаляем пользователя из бд
			db.Users.Remove(user);
			await db.SaveChangesAsync();
			var reply = new UserReply() { Id = user.Id, Name = user.Name, Phone = user.Phone, Address = user.Address };
			return await Task.FromResult(reply);
		}
	}
}
