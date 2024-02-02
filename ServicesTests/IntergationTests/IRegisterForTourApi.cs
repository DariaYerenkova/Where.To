using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Refit;
using ServicesTests.IntergationTests.Models;

namespace ServicesTests.IntergationTests
{
    public interface IRegisterForTourApi
    {
        [Post("/api/Register")]
        Task RegisterUserForTour([Body] RegisterControllerCommand request);

        [Post("/api/Register/RemoveRegistration")]
        Task RemoveUserFromTour([Body] RegisterControllerCommand request);
    }
}
