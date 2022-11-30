import { Component, OnInit } from '@angular/core';
import { NgForm, NgModel } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { IReservaCreate } from '../../../shared/reserva';
import { ViviendaService } from '../../vivienda.service';

@Component({
  selector: 'app-reserva-create-form',
  templateUrl: './reserva-create-form.component.html',
  styleUrls: ['./reserva-create-form.component.css']
})
export class ReservaCreateFormComponent implements OnInit {

  originalReserva: IReservaCreate = {
    fechaInicio:'',
    fechaFin: '',
    nameUsuario: ''
  };
  id: number = 0;

  reservaCreate: IReservaCreate = { ...this.originalReserva };
  postError = false;
  postErrorMessage = '';
  
  constructor(private router: Router,
              private route: ActivatedRoute,
              private viviendaService: ViviendaService) { }

  ngOnInit() {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
  }

  onBlur(field: NgModel) {
    console.log('in onBlur: ', field.valid);
  }

  onHttpError(errorResponse: any) {
    console.log('error: ', errorResponse);
    this.postError = true;
    this.postErrorMessage = errorResponse.error.errorMessage;
  }

  onSubmitReserva(form: NgForm) {
    console.log('in onSubmitReserva: ', form.value);

    if (form.valid) {
      this.viviendaService.postCreateReservaForm(this.id,this.originalReserva).subscribe(
        result => console.log('success: ', result),
        error => this.onHttpError(error)
      );
      this.router.navigate(['/viviendas/' + this.id]);
      alert('Reserva creada con éxito');
    }
    else {
      this.postError = true;
      this.postErrorMessage = "Por favor arregla los errores de arriba"
    }
    
  }

  onBack(): void {
    this.router.navigate(['/viviendas/' + this.id]);
  }


}
