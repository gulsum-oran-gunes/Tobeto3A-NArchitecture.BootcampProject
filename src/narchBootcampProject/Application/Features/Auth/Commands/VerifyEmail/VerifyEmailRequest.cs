using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.VerifyEmail;
public class VerifyEmailRequest
{
    public string AuthenticatorCode { get; set; }
}
