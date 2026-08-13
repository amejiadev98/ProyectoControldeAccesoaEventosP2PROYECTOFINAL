import { useEffect, useState } from 'react'
import type { EventoDto, EntradaDto, InvitadoDto } from '../types/api'
import { getEntradas } from '../services/entradasService'
import { getEventos } from '../services/eventosService'
import { getInvitados } from '../services/invitadosService'

const DashboardPage = () => {
  const [eventos, setEventos] = useState<EventoDto[]>([])
  const [invitados, setInvitados] = useState<InvitadoDto[]>([])
  const [entradas, setEntradas] = useState<EntradaDto[]>([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    const loadData = async () => {
      setError(null)
      setLoading(true)
      try {
        const [eventosData, invitadosData, entradasData] = await Promise.all([
          getEventos(),
          getInvitados(),
          getEntradas(),
        ])
        setEventos(eventosData)
        setInvitados(invitadosData)
        setEntradas(entradasData)
      } catch (e) {
        setError('No se pudo cargar el panel. Verifica el backend.')
      } finally {
        setLoading(false)
      }
    }

    loadData()
  }, [])

  const entradasUsadas = entradas.filter((entrada) => entrada.usada).length
  const entradasPendientes = entradas.length - entradasUsadas

  return (
    <div className="page-section">
      <div className="page-header">
        <div>
          <p className="eyebrow">Dashboard</p>
          <h1>Resumen en tiempo real</h1>
          <p className="page-description">
            Totales de eventos, invitados y entradas conectados directamente
          </p>
        </div>
      </div>

      {error && <p className="message error">{error}</p>}
      {loading ? (
        <p className="empty-state">Cargando métricas...</p>
      ) : (
        <div className="grid-layout dashboard-grid">
          <div className="metric-card dashboard-card">
            <span className="metric-value">{eventos.length}</span>
            <span className="metric-label">Eventos</span>
          </div>
          <div className="metric-card dashboard-card">
            <span className="metric-value">{invitados.length}</span>
            <span className="metric-label">Invitados</span>
          </div>
          <div className="metric-card dashboard-card">
            <span className="metric-value">{entradas.length}</span>
            <span className="metric-label">Entradas emitidas</span>
          </div>
          <div className="metric-card dashboard-card">
            <span className="metric-value">{entradasUsadas} / {entradasPendientes}</span>
            <span className="metric-label">Usadas / Pendientes</span>
          </div>
        </div>
      )}
    </div>
  )
}

export default DashboardPage
