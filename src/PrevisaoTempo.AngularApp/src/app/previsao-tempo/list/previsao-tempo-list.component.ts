import { Component, OnInit } from "@angular/core";
import { Observable, take } from "rxjs";
import { Cidade, AppState } from "../previsao-tempo.types";
import { PrevisaoTempoService } from "../previsao-tempo.service";
import { MatDialog } from "@angular/material/dialog";
import { PrevisaoTempoDetailComponent } from "../detail/previsao-tempo-detail.component";

@Component({
    selector: 'previsao-tempo-list',
    templateUrl: './previsao-tempo-list.component.html'
})
export class PrevisaoTempoListComponent implements OnInit
{
    state$: Observable<AppState>;

    /**
     *
     */
    constructor(private _previsaoTempoService: PrevisaoTempoService, 
            private _matDialog: MatDialog) 
    {
        this.state$ = this._previsaoTempoService.state$;
    }

    ngOnInit(): void {
        this._previsaoTempoService.listaCidades().pipe(take(1))
            .subscribe();
    }

    incluirCidade()
    {
        this.showDetail({
                id: 0,
                nome: '',
                estado: ''
            });
    }

    alterarCidade(cidade: Cidade)
    {
        this.showDetail(cidade);
    }

    showDetail(cidade: Cidade)
    {
        this._matDialog.open(PrevisaoTempoDetailComponent, {
            width: '800px',
            height: '500px',
            data: cidade
        });
    }
}