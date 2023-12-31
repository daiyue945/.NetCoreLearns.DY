﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgr.Domain.ValueObjects;

namespace UserMgr.Domain.Entities
{
    public record UserLoginHistory:IAggregateRoot
    {
        public long Id { get; init; }
        public Guid? UserId { get; set; }
        public PhoneNumber PhoneNumber { get; init; }
        public DateTime CreatedDateTime { get; init; }
        public string Message { get; init; }
        private UserLoginHistory() { }
        public UserLoginHistory(Guid? userId,PhoneNumber phoneNumber,string message)
        {
            this.UserId = userId;
            this.PhoneNumber = phoneNumber;
            this.CreatedDateTime = DateTime.Now;
            this.Message = message;
        }

    }
}
