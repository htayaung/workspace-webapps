using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using System.Reflection;

namespace Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IUser _user;
    private readonly IIdentityService _identityService;

    public AuthorizationBehaviour(IUser user, IIdentityService identityService)
    {
        _user = user;
        _identityService = identityService;
    }

    public async Task<TResponse> Handle(
        TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();
        if (authorizeAttributes.Any())
        {
            // Must be authenticated user
            if (_user.Id == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }

            // Role-based authorization
            var authorizeAttributeWithRoles = authorizeAttributes
                .Where(x => !string.IsNullOrWhiteSpace(x.Roles));
            if (authorizeAttributeWithRoles.Any())
            {
                var authorized = false;
                foreach (var roles in authorizeAttributeWithRoles.Select(x => x.Roles.Split(',')))
                {
                    foreach (var role in roles)
                    {
                        var isInRole = await _identityService.IsInRoleAsync(_user.Id, role.Trim());
                        if (isInRole)
                        {
                            authorized = true;
                            break;
                        }
                    }
                }

                if (!authorized)
                {
                    throw new ForbiddenAccessException();
                }
            }

            // Policy-based authorization
            var authorizeAttributesWithPolicies = authorizeAttributes
                .Where(x => !string.IsNullOrWhiteSpace(x.Policy));
            if (authorizeAttributesWithPolicies.Any())
            {
                foreach (var policy in authorizeAttributesWithPolicies.Select(x => x.Policy))
                {
                    var authorized = await _identityService.AuthorizeAsync(_user.Id, policy);
                    if (!authorized)
                    {
                        throw new ForbiddenAccessException();
                    }
                }
            }
        }

        // User is authorized / authorization not required
        return await next();
    }
}
