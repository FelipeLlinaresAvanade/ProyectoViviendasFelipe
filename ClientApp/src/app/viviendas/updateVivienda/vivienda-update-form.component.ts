import { Component, OnInit } from '@angular/core';
import { NgForm, NgModel } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { IVivienda, IViviendaCreate } from '../../shared/vivienda';
import { ViviendaService } from '../vivienda.service';

@Component({
  selector: 'app-vivienda-update-form',
  templateUrl: './vivienda-update-form.component.html',
  styleUrls: ['./vivienda-update-form.component.css']
})
export class ViviendaUpdateFormComponent implements OnInit {

  vivienda: IViviendaCreate = {
    name: '',
    description: '',
    direccion: '',
    propietario:''
  };

  id: number = 0;
  pageTitle: string = "Modificar Vivienda";

  postError = false;
  postErrorMessage = '';
  sub!: Subscription;
  errorMessage: string = '';
  
  constructor(private router: Router,
              private route: ActivatedRoute,
              private viviendaService: ViviendaService) { }

  ngOnInit() {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    this.pageTitle += `: ${this.id}`;

    this.sub = this.viviendaService.loadViviendaSinReservas(this.id)
        .subscribe({
          next: vivienda => {
            this.vivienda = vivienda;
          },
          error: err => this.errorMessage = err
        });

    }

  onBlur(field: NgModel) {
    console.log('in onBlur: ', field.valid);
  }

  onHttpError(errorResponse: any) {
    console.log('error: ', errorResponse);
    this.postError = true;
    this.postErrorMessage = errorResponse.error.errorMessage;
  }

  onSubmit(form: NgForm) {
    console.log('in onSubmit: ', form.value);

    if (form.valid) {
      this.viviendaService.putViviendaUpdateForm(this.id,this.vivienda).subscribe(
         result => console.log('success: ', result),
         error => this.onHttpError(error)
      );
      this.router.navigate(['/viviendas']);
      alert('Vivienda modificada con éxito');
    }
    else {
       this.postError = true;
      this.postErrorMessage = "Por favor arregla los errores de arriba";
    }
    
  }

  onBack(): void {
    
    this.router.navigate(['/viviendas']);
    
  }


}
