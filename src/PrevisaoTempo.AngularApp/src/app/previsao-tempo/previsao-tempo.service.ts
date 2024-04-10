import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, catchError, map, of, 
        switchMap, take, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Cidade, AppState as AppState, Previsao } from './previsao-tempo.types';

@Injectable({
    providedIn: 'root'
})
export class PrevisaoTempoService
{
    private _initialState: AppState = {
        isLoading: false,
        isSaving: false,
        cidades: [],
        error: ''
    }

    private _state: BehaviorSubject<AppState> = new BehaviorSubject(this._initialState);

    constructor(private _httpClient: HttpClient) {
        
    }

    get state$(): Observable<AppState> {
        return this._state.asObservable();
    }

    public listaCidades() : Observable<Cidade[]> {
        return this._state.pipe(
            take(1),
            tap(state => this._state.next({...state, isLoading: true})),
            switchMap(state => this._httpClient.get<Cidade[]>(`${environment.apiUrl}/api/cidades/listacidades`).pipe(
                map(cidades => { 
                    this._state.next({...state, isLoading: false, cidades: cidades, error: ''});
                    return cidades;
                }),
                catchError(error => {
                    this._state.next({...state, isLoading: false, error: error.message});
                    return of(error);
                })
            )),
            
        )
    }

    public criaOuAtualiza(cidade: Cidade) : Observable<Cidade> {
        const isCreating = cidade.id === 0;
        const action = isCreating ? 'CriaCidade' : 'AlteraCidade';
        return this._state.pipe(
            take(1),
            tap(state => this._state.next({...state, isSaving: true})),
            switchMap(state => this._httpClient.post<Cidade>(`${environment.apiUrl}/api/cidades/${action}`, cidade).pipe(
                map(cidade => { 
                    // Quando criando uma nova cidade, adiciona no array
                    // Quando atualizado uma cidade, sobrescreve o cidade antiga pela nova
                    const novaCidade = isCreating
                        ? [...state.cidades, cidade] 
                        : state.cidades.map(n => n.id === cidade.id ? cidade : n);
                    this._state.next({...state, isSaving: false, cidades: novaCidade, error: ''});
                    return cidade;
                }),
                catchError(error => {
                    console.log(error);
                    this._state.next({...state, isSaving: false, error: error.error});
                    return of(error);
                })
            )),

        );
    }

    public excluiCidade(id: number) : Observable<any> {
        return this._state.pipe(
            take(1),
            tap(state => this._state.next({...state, isSaving: true})),
            switchMap(state => this._httpClient.delete(`${environment.apiUrl}/api/cidades/excluicidade?id=${id}`).pipe(
                map(() => { 
                    this._state.next({...state, isSaving: false, 
                            cidades: state.cidades.filter(n => n.id != id), error: ''});
                    return null;
                }),
                catchError(error => {
                    this._state.next({...state, isSaving: false, error: error.message});
                    return of(error);
                })
            )),
        );
    }

    public consultaPrevisaoTempo(id: number): Observable<{previsaoTempo: Previsao}>
    {
        const endpoint = `${environment.apiUrl}/api/cidades/consultaprevisaotempo?id=${id}`;
        return this._httpClient.get<{previsaoTempo: Previsao}>(endpoint)
    }
}