import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', pathMatch : 'full', redirectTo: 'previsao-tempo'},
  {
      path: 'previsao-tempo',
      loadChildren: () => import('./previsao-tempo/previsao-tempo.module').then(m => m.PrevisaoTempoModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
