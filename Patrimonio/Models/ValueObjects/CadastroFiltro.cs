namespace Patrimonio.Models.ValueObjects;

public sealed record CadastroFiltro(
    string Empresa,
    string Filial,
    long NumeroPatrimonial,
    long TipoIncorporacao,
    long Incorporacao
);

public sealed record ValidaCadastroFiltro(
    string Empresa, 
    long NumeroPatrimonial
);