import { NgModule } from '@angular/core';
import { ViviendaListComponent } from './vivienda-list.component';
import { RouterModule } from '@angular/router';
import { ViviendaDetailGuard } from './vivienda/vivienda-detail.guard';
import { ViviendaDetailComponent } from './vivienda/vivienda-detail.component';
import { ViviendaCreateFormComponent } from './createVivienda/vivienda-create-form.component';
import { ReservaCreateFormComponent } from './vivienda/createReserva/reserva-create-form.component';
import { ViviendaUpdateFormComponent } from './updateVivienda/vivienda-update-form.component';

@NgModule({
  declarations: [
    
  ],
  imports: [
    RouterModule.forChild([
      { path: 'viviendas', component: ViviendaListComponent },
      {
        path: 'viviendas/create',
        component: ViviendaCreateFormComponent
      },
      {
        path: 'viviendas/:id/reservaCreate',
        component: ReservaCreateFormComponent
      },
      {
        path: 'viviendas/update/:id',
        component: ViviendaUpdateFormComponent
      },
      {
        path: 'viviendas/:id',
        canActivate: [ViviendaDetailGuard],
        component: ViviendaDetailComponent
      }

    ]),
   
  ]
})
export class ViviendaModule { }
