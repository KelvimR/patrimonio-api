using Patrimonio.Models;

namespace Patrimonio.Repositories.Implementations;

public interface IAuditoriaRepository
{
    Task RegistrarLogAsync(Auditoria auditoria, CancellationToken cancellationToken);
}
