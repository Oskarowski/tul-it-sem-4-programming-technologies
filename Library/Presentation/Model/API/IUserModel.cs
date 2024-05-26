﻿namespace PresentationLayer.Model.API
{
    public interface IUserModel
    {
        string Guid { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        double Balance { get; set; }
        string PhoneNumber { get; set; }
    }
}
