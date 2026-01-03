using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace FastFood.PayStream.Api.Config.Auth;

/// <summary>
/// Filtro que adiciona automaticamente os esquemas de segurança ao Swagger baseado nos atributos [Authorize]
/// </summary>
public class AuthorizeBySchemeOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Verificar se o endpoint tem [AllowAnonymous]
        var hasAllowAnonymous = context.MethodInfo.GetCustomAttributes(true)
            .OfType<AllowAnonymousAttribute>()
            .Any() ||
            context.MethodInfo.DeclaringType?.GetCustomAttributes(true)
            .OfType<AllowAnonymousAttribute>()
            .Any() == true;

        if (hasAllowAnonymous)
        {
            return; // Não adicionar autenticação para endpoints anônimos
        }

        // Verificar se o endpoint tem [Authorize]
        var authorizeAttributes = context.MethodInfo.GetCustomAttributes(true)
            .OfType<AuthorizeAttribute>()
            .ToList();

        // Se não tiver no método, verificar na classe
        if (!authorizeAttributes.Any())
        {
            authorizeAttributes = context.MethodInfo.DeclaringType?
                .GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>()
                .ToList() ?? new List<AuthorizeAttribute>();
        }

        if (!authorizeAttributes.Any())
        {
            return; // Não tem [Authorize], não precisa adicionar segurança
        }

        // Adicionar esquemas de segurança baseado nos atributos [Authorize]
        foreach (var authorizeAttribute in authorizeAttributes)
        {
            var schemes = authorizeAttribute.AuthenticationSchemes?.Split(',')
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrEmpty(s))
                .ToList() ?? new List<string>();

            // Se não especificou esquema, usar os padrões
            if (!schemes.Any())
            {
                schemes.Add("CustomerBearer");
                schemes.Add("Cognito");
            }

            // Adicionar cada esquema à operação
            foreach (var scheme in schemes)
            {
                if (!operation.Security.Any(s => s.Keys.Any(k => k.Reference.Id == scheme)))
                {
                    operation.Security.Add(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = scheme
                                }
                            },
                            Array.Empty<string>()
                        }
                    });
                }
            }
        }
    }
}
