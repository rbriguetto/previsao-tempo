import { Component, Inject } from "@angular/core";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { Cidade } from "../previsao-tempo.types";
import { PrevisaoTempoService } from "../previsao-tempo.service";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Observable, map, take } from "rxjs";

@Component({
    selector: 'previsao-tempo-detail',
    templateUrl : './previsao-tempo-detail.component.html'
})
export class PrevisaoTempoDetailComponent
{
    form!: FormGroup;
    dialogTitle: string = "";
    isSaving$: Observable<boolean>;
    
    constructor( @Inject(MAT_DIALOG_DATA) data: Cidade, 
        private _dialogRef: MatDialogRef<PrevisaoTempoDetailComponent>,
        private _formBuilder: FormBuilder,
        private _previsaoTempoService: PrevisaoTempoService) {
        this.createForm(data);
        this.dialogTitle = data.id === 0 ? 'Nova Cidade' : `Alterando ${data.id}`;
        this.isSaving$ = this._previsaoTempoService.state$.pipe(map(state => state.isSaving));
    }

    createForm(cidade: Cidade) { this.form = this._formBuilder.group({
            id: cidade.id,
            nome: [cidade.nome, Validators.required],
            estado: [cidade.estado],
            latitude: [cidade.latitude],
            longitude: [cidade.longitude],
        }); }

    gravaCidade() {
        if (!this.form?.valid) {
            return;
        }

        this._previsaoTempoService.criaOuAtualiza(this.form?.value)
            .pipe(take(1))
            .subscribe(() => this.close());
    }

    excluiCidade() {
        const id = this.form.get('id')?.value;
        if (id == null || id === '') {
            return;
        }
        this._previsaoTempoService.excluiCidade(id)
            .pipe(take(1))
            .subscribe(() => this.close());
    }

    close() {
        this._dialogRef.close();
    }
}