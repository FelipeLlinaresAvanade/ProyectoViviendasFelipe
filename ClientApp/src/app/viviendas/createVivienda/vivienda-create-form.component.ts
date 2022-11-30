import { Component, OnInit } from '@angular/core';
import { NgForm, NgModel } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { IVivienda, IViviendaCreate } from '../../shared/vivienda';
import { ViviendaService } from '../vivienda.service';

@Component({
  selector: 'app-vivienda-create-form',
  templateUrl: './vivienda-create-form.component.html',
  styleUrls: ['./vivienda-create-form.component.css']
})
export class ViviendaCreateFormComponent implements OnInit {

  originalVivienda: IViviendaCreate = {
    description: '',
    direccion: '',
    propietario: '',
    name: ''
  };

  postError = false;
  postErrorMessage = '';
  
  constructor(private router: Router,
              private viviendaService: ViviendaService) { }

  ngOnInit() {
  
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
      this.viviendaService.postCreateViviendaForm(this.originalVivienda).subscribe(
         result => console.log('success: ', result),
         error => this.onHttpError(error)
      );
      this.router.navigate(['/viviendas']);
      alert('Vivienda creada con éxito');
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
