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
        Task <ActionResult> RegisterUserForTour([Body] RegisterControllerCommand request);

        [Post("/api/Register/RemoveRegistration")]
        Task<ActionResult> RemoveUserFromTour([Body] RegisterControllerCommand request);
    }
}
