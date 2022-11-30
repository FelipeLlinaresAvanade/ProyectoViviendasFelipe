import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { ViviendaModule } from './viviendas/vivienda.module';
import { ViviendaListComponent } from './viviendas/vivienda-list.component';
import { ViviendaDetailComponent } from './viviendas/vivienda/vivienda-detail.component';
import { ViviendaCreateFormComponent } from './viviendas/createVivienda/vivienda-create-form.component';
import { ReservaCreateFormComponent } from './viviendas/vivienda/createReserva/reserva-create-form.component';
import { ViviendaUpdateFormComponent } from './viviendas/updateVivienda/vivienda-update-form.component';
import { ContactoComponent } from './contacto/contact.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ContactoComponent,
    ViviendaListComponent,
    ViviendaDetailComponent,
    ViviendaCreateFormComponent,
    ReservaCreateFormComponent,
    ViviendaUpdateFormComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'welcome', component: HomeComponent},
      { path: '', redirectTo: 'welcome', pathMatch: 'full' },
      { path: 'contacto', component: ContactoComponent },
      { path: 'viviendas', component: ViviendaListComponent },
      { path: 'viviendas/:id', component: ViviendaDetailComponent },
      { path: 'viviendas/create', component: ViviendaCreateFormComponent },
      { path: 'viviendas/:id/reservaCreate', component: ReservaCreateFormComponent },
      { path: 'viviendas/update/:id' , component: ViviendaUpdateFormComponent }

    ]),
    ViviendaModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
