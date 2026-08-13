import { useEffect, useState } from 'react'
import type { EntradaDto, EventoDto, InvitadoDto } from '../types/api'
import {
  createEntrada,
  deleteEntrada,
  getEntradas,
  updateEntrada,
} from '../services/entradasService'
import { getEventos } from '../services/eventosService'
import { getInvitados } from '../services/invitadosService'
import '../App.css'

const EntradasPage = () => {
  const [entradas, setEntradas] = useState<EntradaDto[]>([])
  const [eventos, setEventos] = useState<EventoDto[]>([])
  const [invitados, setInvitados] = useState<InvitadoDto[]>([])
  const [selectedId, setSelectedId] = useState<number | null>(null)
  const [eventoId, setEventoId] = useState<number | null>(null)
  const [invitadoId, setInvitadoId] = useState<number | null>(null)
  const [codigoQR, setCodigoQR] = useState('')
  const [usada, setUsada] = useState(false)
  const [loading, setLoading] = useState(true)
  const [saving, setSaving] = useState(false)
  const [error, setError] = useState<string | null>(null)
  const [mensaje, setMensaje] = useState<string | null>(null)

  const loadData = async () => {
    setLoading(true)
    setError(null)
    try {
      const [entradasData, eventosData, invitadosData] = await Promise.all([
        getEntradas(),
        getEventos(),
        getInvitados(),
      ])
      setEntradas(entradasData)
      setEventos(eventosData)
      setInvitados(invitadosData)
    } catch (e) {
      setError('No se pudieron cargar las entradas o datos relacionados.')
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    void loadData()
  }, [])

  const resetForm = () => {
    setSelectedId(null)
    setEventoId(null)
    setInvitadoId(null)
    setCodigoQR('')
    setUsada(false)
    setMensaje(null)
    setError(null)
  }

  const handleGuardar = async () => {
    if (!eventoId || !invitadoId || !codigoQR.trim()) {
      setError('Evento, invitado y código QR son obligatorios.')
      setMensaje(null)
      return
    }

    setSaving(true)
    setError(null)
    try {
      const payload = {
        eventoId,
        invitadoId,
        codigoQR: codigoQR.trim(),
        usada,
      }

      if (selectedId) {
        await updateEntrada(selectedId, payload)
        setMensaje('Entrada actualizada correctamente.')
      } else {
        await createEntrada(payload)
        setMensaje('Entrada creada correctamente.')
      }

      await loadData()
      resetForm()
    } catch (e) {
      setError('Error al guardar la entrada en el backend.')
    } finally {
      setSaving(false)
    }
  }

  const handleEditar = (item: EntradaDto) => {
    setSelectedId(item.id)
    setEventoId(item.eventoId)
    setInvitadoId(item.invitadoId)
    setCodigoQR(item.codigoQR ?? '')
    setUsada(item.usada)
    setMensaje(null)
    setError(null)
  }

  const handleEliminar = async (id: number) => {
    if (!window.confirm('¿Deseas eliminar esta entrada?')) {
      return
    }

    setError(null)
    try {
      await deleteEntrada(id)
      setMensaje('Entrada eliminada correctamente.')
      await loadData()
      if (selectedId === id) {
        resetForm()
      }
    } catch (e) {
      setError('No se pudo eliminar la entrada en el backend.')
    }
  }

  const getEventName = (id: number | null) => eventos.find((event) => event.id === id)?.nombre ?? 'No disponible'
  const getInvitadoName = (id: number | null) => invitados.find((guest) => guest.id === id)?.nombre ?? 'No disponible'

  return (
    <div className="page-section">
      <div className="page-header">
        <div>
          <p className="eyebrow">Entradas</p>
          <h1>Control de entradas</h1>
          <p className="page-description">
            Gestiona las entradas emitidas y registra su estado 
          </p>
        </div>
      </div>

      <div className="message-row">
        {mensaje && <p className="message success">{mensaje}</p>}
        {error && <p className="message error">{error}</p>}
      </div>

      <div className="grid-layout">
        <section className="section-card">
          <h2>{selectedId ? 'Editar entrada' : 'Nueva entrada'}</h2>
          <div className="form-grid">
            <div className="field-group">
              <label>Evento</label>
              <select value={eventoId ?? ''} onChange={(e) => setEventoId(Number(e.target.value) || null)}>
                <option value="">Seleccione un evento</option>
                {eventos.map((item) => (
                  <option key={item.id} value={item.id}>
                    {item.nombre}
                  </option>
                ))}
              </select>
            </div>
            <div className="field-group">
              <label>Invitado</label>
              <select
                value={invitadoId ?? ''}
                onChange={(e) => setInvitadoId(Number(e.target.value) || null)}
              >
                <option value="">Seleccione un invitado</option>
                {invitados.map((item) => (
                  <option key={item.id} value={item.id}>
                    {item.nombre}
                  </option>
                ))}
              </select>
            </div>
            <div className="field-group">
              <label>Código QR</label>
              <input
                value={codigoQR}
                onChange={(e) => setCodigoQR(e.target.value)}
                placeholder="Código QR de la entrada"
              />
            </div>
            <div className="field-group">
              <label>
                <input
                  type="checkbox"
                  checked={usada}
                  onChange={(e) => setUsada(e.target.checked)}
                />
                Entrada usada
              </label>
            </div>
          </div>
          <button
            type="button"
            className="btn-primary"
            onClick={handleGuardar}
            disabled={saving}
          >
            {selectedId ? 'Actualizar entrada' : 'Crear entrada'}
          </button>
        </section>

        <section className="section-card">
          <h2>Entradas registradas</h2>
          {loading ? (
            <p className="empty-state">Cargando entradas...</p>
          ) : entradas.length === 0 ? (
            <p className="empty-state">No hay entradas registradas.</p>
          ) : (
            <div className="table-shell">
              <table>
                <thead>
                  <tr>
                    <th>Código QR</th>
                    <th>Evento</th>
                    <th>Invitado</th>
                    <th>Usada</th>
                    <th>Acciones</th>
                  </tr>
                </thead>
                <tbody>
                  {entradas.map((item) => (
                    <tr key={item.id}>
                      <td>{item.codigoQR}</td>
                      <td>{getEventName(item.eventoId)}</td>
                      <td>{getInvitadoName(item.invitadoId)}</td>
                      <td>{item.usada ? 'Sí' : 'No'}</td>
                      <td className="actions-cell">
                        <button
                          type="button"
                          className="link-button"
                          onClick={() => handleEditar(item)}
                        >
                          Editar
                        </button>
                        <button
                          type="button"
                          className="link-button danger"
                          onClick={() => void handleEliminar(item.id)}
                        >
                          Eliminar
                        </button>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          )}
        </section>
      </div>
    </div>
  )
}

export default EntradasPage
