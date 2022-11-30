import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { IReserva } from '../../shared/reserva';
import { IVivienda } from '../../shared/vivienda';
import { ViviendaService } from '../vivienda.service';

@Component({
  templateUrl: './vivienda-detail.component.html'
})
export class ViviendaDetailComponent implements OnInit {
  pageTitle: string = 'Reservas Vivienda';
  vivienda: IVivienda | any;
  reservas: IReserva[] = [];
  sub!: Subscription;
  errorMessage: string = '';
  postError = false;
  postErrorMessage = '';
  id: number = 0;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private viviendaService: ViviendaService) { }

  cargarReservas(): void {
    this.sub = this.viviendaService.loadVivienda(this.id)
      .subscribe({
        next: vivienda => {
          this.vivienda = vivienda;
          this.reservas = vivienda.reservas;
        },
        error: err => this.errorMessage = err
      });
  }

  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    this.pageTitle += `: ${this.id}`;
    this.cargarReservas();
  }

  onBack(): void {
    this.router.navigate(['/viviendas']);
  }



  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  onDelete(idVivienda: number, idReserva: number): void {
    console.log('in onDelete: vivienda', idVivienda, ' reserva:', idReserva);

    this.viviendaService.deleteReserva(idVivienda, idReserva).subscribe(
      result => console.log('success: ', result),
      error => this.onHttpError(error)
    );

    this.cargarReservas();
  }

  onHttpError(errorResponse: any) {
    console.log('error: ', errorResponse);
    this.postError = true;
    this.postErrorMessage = errorResponse.error.errorMessage;
  }

}
