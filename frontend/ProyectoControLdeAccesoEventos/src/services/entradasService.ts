import { request } from './api'
import type { EntradaDto } from '../types/api'

const endpoint = '/api/entradas'

export const getEntradas = async () => {
  return await request<EntradaDto[]>(endpoint)
}

export const getEntrada = async (id: number) => {
  return await request<EntradaDto>(`${endpoint}/${id}`)
}

export const createEntrada = async (entrada: Omit<EntradaDto, 'id'>) => {
  return await request<EntradaDto>(endpoint, {
    method: 'POST',
    body: JSON.stringify(entrada),
  })
}

export const updateEntrada = async (id: number, entrada: Omit<EntradaDto, 'id'>) => {
  return await request<void>(`${endpoint}/${id}`, {
    method: 'PUT',
    body: JSON.stringify(entrada),
  })
}

export const deleteEntrada = async (id: number) => {
  await request<void>(`${endpoint}/${id}`, { method: 'DELETE' })
}
