using Patrimonio.Models;

namespace Patrimonio.Repositories;

public interface IAuditoriaRepository
{
    Task RegistrarLogAsync(Auditoria auditoria, CancellationToken cancellationToken);
}
