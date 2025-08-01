﻿using ELECON.Domain.Entities.User;
using ELECON.Domain.Interface.ISharedRepository;

namespace ELECON.Domain.Interface.IUserRepository;

public interface IUserLoginHistoryRepository:ISharedRepository<UserLoginHistory>
{
    
}