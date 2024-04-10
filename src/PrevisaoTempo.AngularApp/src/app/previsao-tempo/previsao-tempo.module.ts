import { NgModule } from "@angular/core";
import { PrevisaoTempoListComponent } from "./list/previsao-tempo-list.component";
import { RouterModule } from "@angular/router";
import { previsaoTempoRoutes } from "./previsao-tempo.routes";
import { MatDialogModule } from '@angular/material/dialog'
import { CommonModule } from "@angular/common";
import { PrevisaoTempoDetailComponent } from "./detail/previsao-tempo-detail.component";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { LoadingModule } from "../ui/loading/loading.module";
import { WidgetPrevisaoComponent } from "./widget-previsao/widget-previsao.component";

@NgModule({
    declarations: [
        PrevisaoTempoListComponent,
        PrevisaoTempoDetailComponent,
        WidgetPrevisaoComponent
    ],
    imports: [
        CommonModule,
        RouterModule.forChild(previsaoTempoRoutes),
        ReactiveFormsModule,
        FormsModule,
        MatDialogModule,
        LoadingModule
    ],
    exports: [
        RouterModule
    ],
})
export class PrevisaoTempoModule
{

}