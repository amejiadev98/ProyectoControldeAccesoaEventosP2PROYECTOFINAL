import { request } from './api'
import type { ValidacionDto } from '../types/api'

const endpoint = '/api/validaciones'

export const getValidaciones = async () => {
  return await request<ValidacionDto[]>(endpoint)
}

export const createValidacion = async (validacion: Omit<ValidacionDto, 'id'>) => {
  return await request<ValidacionDto>(endpoint, {
    method: 'POST',
    body: JSON.stringify(validacion),
  })
}

export const getValidacion = async (id: number) => {
  return await request<ValidacionDto>(`${endpoint}/${id}`)
}

export const updateValidacion = async (id: number, validacion: Omit<ValidacionDto, 'id'>) => {
  return await request<void>(`${endpoint}/${id}`, {
    method: 'PUT',
    body: JSON.stringify(validacion),
  })
}

export const deleteValidacion = async (id: number) => {
  await request<void>(`${endpoint}/${id}`, { method: 'DELETE' })
}
