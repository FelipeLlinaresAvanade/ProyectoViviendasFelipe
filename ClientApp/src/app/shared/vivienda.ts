import { IReserva } from "./reserva";

export interface IVivienda {
  id: number;
  name: string;
  description: string;
  direccion: string;
  propietario: string;
  reservas: IReserva[];
}

export interface IViviendaCreate {
  description: string;
  direccion: string;
  propietario: string;
  name: string;
}
