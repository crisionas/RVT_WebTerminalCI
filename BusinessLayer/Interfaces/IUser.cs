﻿using RVTLibrary.Models.AuthUser;
using RVTLibrary.Models.UserIdentity;
using RVTLibrary.Models.Vote;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IUser
    {
        public Task<RegistrationResponse> Registration(RegistrationMessage registration);
        public Task<AuthResponse> Auth(AuthMessage auth);
        public Task<VoteResponse> Vote(VoteMessage vote);
    }
}
