import { ChangeDetectorRef, Component, Input, OnInit } from "@angular/core";
import { Cidade, Previsao } from "../previsao-tempo.types";
import { PrevisaoTempoService } from "../previsao-tempo.service";
import { BehaviorSubject, tap } from "rxjs";

@Component({
    selector: 'widget-previsao',
    templateUrl: './widget-previsao.component.html'
})
export class WidgetPrevisaoComponent implements OnInit
{
    @Input() cidade?: Cidade

    previsaoTempo?: Previsao;
    isLoading$: BehaviorSubject<boolean> = new BehaviorSubject(false);
        
    /**
     *
     */
    constructor(private _previsaoTempoService: PrevisaoTempoService) {

    }

    ngOnInit(): void {

        if (this.cidade?.id == null)
            return;

        this.isLoading$.next(true);
        this._previsaoTempoService.consultaPrevisaoTempo(this.cidade.id)
           .subscribe(response => { 
                this.previsaoTempo = response.previsaoTempo
                this.isLoading$.next(false);
            });
    }
}