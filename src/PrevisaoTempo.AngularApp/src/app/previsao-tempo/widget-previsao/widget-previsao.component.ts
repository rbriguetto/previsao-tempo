import { ChangeDetectorRef, Component, Input, OnInit } from "@angular/core";
import { Cidade, Previsao } from "../previsao-tempo.types";
import { PrevisaoTempoService } from "../previsao-tempo.service";
import { tap } from "rxjs";

@Component({
    selector: 'widget-previsao',
    templateUrl: './widget-previsao.component.html'
})
export class WidgetPrevisaoComponent implements OnInit
{
    @Input() cidade?: Cidade

    previsaoTempo?: Previsao;

    /**
     *
     */
    constructor(private _previsaoTempoService: PrevisaoTempoService, 
            private _cd: ChangeDetectorRef) {
        
    }

    ngOnInit(): void {

        if (this.cidade?.id == null)
            return;

        this._previsaoTempoService.consultaPrevisaoTempo(this.cidade.id)
            .subscribe(response => { 
                this.previsaoTempo = response.previsaoTempo
                this._cd.markForCheck();
            });
    }
}