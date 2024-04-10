export interface Cidade
{
    id?: number;
    nome?: string;
    estado?: string;
    latitude?: number;
    longitude?: number;
}

export interface Previsao
{
    temperatura: number;
    sensacaoTermica: number;
    temperaturaMinima: number;
    temperaturaMaxima: number;
    umidade: number;
    nascerDoSolUtc: Date;
    porDoSolUtc: Date;
}

export interface AppState
{
    isLoading: boolean;
    isSaving: boolean;
    cidades: Cidade[];
    error: string;
}
