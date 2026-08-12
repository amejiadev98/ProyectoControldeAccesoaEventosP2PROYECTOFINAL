import { request } from './api'
import type { EventoDto } from '../types/api'

const endpoint = '/api/eventos'

export const getEventos = async () => {
  return await request<EventoDto[]>(endpoint)
}

export const getEvento = async (id: number) => {
  return await request<EventoDto>(`${endpoint}/${id}`)
}

export const createEvento = async (evento: Omit<EventoDto, 'id'>) => {
  return await request<EventoDto>(endpoint, {
    method: 'POST',
    body: JSON.stringify(evento),
  })
}

export const updateEvento = async (id: number, evento: Omit<EventoDto, 'id'>) => {
  return await request<void>(`${endpoint}/${id}`, {
    method: 'PUT',
    body: JSON.stringify(evento),
  })
}

export const deleteEvento = async (id: number) => {
  await request<void>(`${endpoint}/${id}`, { method: 'DELETE' })
}
