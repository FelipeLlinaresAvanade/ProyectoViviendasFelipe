export interface IReserva {
  id: number;
  fechaInicio: string;
  fechaFin: string;
  nameUsuario: string;
}

export interface IReservaCreate {
  fechaInicio: string;
  fechaFin: string;
  nameUsuario: string;
}
