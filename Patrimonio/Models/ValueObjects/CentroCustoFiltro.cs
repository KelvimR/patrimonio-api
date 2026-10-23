namespace Patrimonio.Models.ValueObjects;

public sealed record ObterCentroCustoFiltro(
    string Empresa, 
    string EmpresaFilial, 
    int Inventario
);

public sealed record ValidarCentroCustoFiltro(
    string Empresa, 
    string EmpresaFilial, 
    string CentroCusto
);
