export interface EntradaDto {
  id: number
  eventoId: number
  invitadoId: number
  codigoQR?: string | null
  usada: boolean
}

export interface EventoDto {
  id: number
  nombre?: string | null
  fecha: string
  lugar?: string | null
}

export interface InvitadoDto {
  id: number
  nombre?: string | null
  cedula?: string | null
  correo?: string | null
}

export interface ValidacionDto {
  id: number
  entradaId: number
  fechaValidacion: string
  accesoPermitido: boolean
}
