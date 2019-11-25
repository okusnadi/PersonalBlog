using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalBlog.App.Services
{
    public interface IJsonWebTokenService
    {
        string GetJsonWebToken();
    }
}
