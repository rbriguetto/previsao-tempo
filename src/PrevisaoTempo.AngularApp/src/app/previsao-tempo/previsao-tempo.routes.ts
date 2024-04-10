import { Routes } from '@angular/router';
import { PrevisaoTempoListComponent } from './list/previsao-tempo-list.component';

export const previsaoTempoRoutes: Routes = [
    {
        path: '',
        component: PrevisaoTempoListComponent,
        pathMatch: 'full'
    }
];