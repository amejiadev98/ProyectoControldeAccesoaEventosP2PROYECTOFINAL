import { request } from './api'
import type { InvitadoDto } from '../types/api'

const endpoint = '/api/invitados'

export const getInvitados = async () => {
  return await request<InvitadoDto[]>(endpoint)
}

export const getInvitado = async (id: number) => {
  return await request<InvitadoDto>(`${endpoint}/${id}`)
}

export const createInvitado = async (invitado: Omit<InvitadoDto, 'id'>) => {
  return await request<InvitadoDto>(endpoint, {
    method: 'POST',
    body: JSON.stringify(invitado),
  })
}

export const updateInvitado = async (id: number, invitado: Omit<InvitadoDto, 'id'>) => {
  return await request<void>(`${endpoint}/${id}`, {
    method: 'PUT',
    body: JSON.stringify(invitado),
  })
}

export const deleteInvitado = async (id: number) => {
  await request<void>(`${endpoint}/${id}`, { method: 'DELETE' })
}
