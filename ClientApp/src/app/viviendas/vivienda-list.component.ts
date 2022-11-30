import { Component, OnDestroy, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Subscription } from "rxjs";
import { IVivienda } from "../shared/vivienda";
import { ViviendaService } from "./vivienda.service";

@Component({
  templateUrl: './vivienda-list.component.html',
  providers: [ViviendaService]
})
export class ViviendaListComponent implements OnInit, OnDestroy {
  pageTitle: string = 'Lista de Viviendas';
  errorMessage: string = '';
  sub!: Subscription;

  postError = false;
  postErrorMessage = '';

  private _listFilter: string = '';
  get listFilter(): string {
    return this._listFilter;
  }
  set listFilter(value: string) {
    this._listFilter = value;
    console.log('In setter:', value);
    this.filteredViviendas = this.performFilter(value);
  }

  filteredViviendas: IVivienda[] = [];
  viviendas: IVivienda[] = [];

  constructor(private router: Router,
    private viviendaService: ViviendaService) { }

  performFilter(filterBy: string): IVivienda[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.viviendas.filter((vivienda: IVivienda) =>
      vivienda.direccion.toLocaleLowerCase().includes(filterBy));
  }


  ngOnInit(): void {
    this.cargarViviendas();
  }

  cargarViviendas(): void {
    this.sub = this.viviendaService.loadViviendas()
      .subscribe({
        next: viviendas => {
          this.viviendas = viviendas;
          this.filteredViviendas = viviendas;
        },
        error: err => this.errorMessage = err
      })
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  onDelete(id: number): void {
    console.log('in onDelete: ', id);

    this.viviendaService.deleteVivienda(id).subscribe(
      result => console.log('success: ', result),
      error => this.onHttpError(error)
    );

    this.cargarViviendas();

  }

  onModificar(id: number): void {
    console.log('in onDelete: ', id);

    this.viviendaService.deleteVivienda(id).subscribe(
      result => console.log('success: ', result),
      error => this.onHttpError(error)
    );

    this.cargarViviendas();
  }

  onHttpError(errorResponse: any) {
    console.log('error: ', errorResponse);
    this.postError = true;
    this.postErrorMessage = errorResponse.error.errorMessage;
  }
}
