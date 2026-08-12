export const apiBase = import.meta.env.VITE_API_URL || 'https://localhost:50340'

export type ApiRequestInit = RequestInit & { query?: Record<string, string | number | boolean> }

export async function request<T = any>(path: string, init?: ApiRequestInit): Promise<T> {
  const { query, headers, ...rest } = init ?? {}

  const url = `${apiBase}${path}`
  if (query && Object.keys(query).length > 0) {
    const params = new URLSearchParams()
    for (const [k, v] of Object.entries(query)) {
      if (v !== undefined && v !== null) params.append(k, String(v))
    }
    url += `?${params.toString()}`
  }

  const res = await fetch(url, {
    headers: {
      'Content-Type': 'application/json',
      ...(headers as Record<string, string> | undefined),
    },
    ...rest,
  })

  if (!res.ok) {
    const text = await res.text().catch(() => '')
    throw new Error(text || res.statusText || `HTTP ${res.status}`)
  }

  if (res.status === 204) return null as unknown as T

  const data = await res.json().catch(() => null)
  return data as T
}

export default { apiBase, request }
