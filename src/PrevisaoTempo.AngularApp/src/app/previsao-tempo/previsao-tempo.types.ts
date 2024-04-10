export interface Cidade
{
    id?: string;
    nome?: string;
    estado?: string;
    latitude?: number;
    longitude?: number;
}

export interface AppState
{
    isLoading: boolean;
    isSaving: boolean;
    cidades: Cidade[];
    error: string;
}
