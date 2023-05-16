using ApplicationCore.Entities;
using ApplicationCore.Models.Common;
using ApplicationCore.Models.Identity;

namespace Infrastructure.Interfaces;

internal interface ISessionSecurityValidator
{
    ValidationResult Validate(Session session, ClientIdentificationData identificationData);
}