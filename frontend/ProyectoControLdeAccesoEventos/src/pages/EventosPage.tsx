import { useEffect, useMemo, useState } from 'react'
import type { EventoDto } from '../types/api'
import {
  createEvento,
  deleteEvento,
  getEventos,
  updateEvento,
} from '../services/eventosService'
import '../App.css'

const EventosPage = () => {
  const [eventos, setEventos] = useState<EventoDto[]>([])
  const [selectedEventoId, setSelectedEventoId] = useState<number | null>(null)
  const [nombre, setNombre] = useState('')
  const [fecha, setFecha] = useState('')
  const [lugar, setLugar] = useState('')
  const [mensaje, setMensaje] = useState<string | null>(null)
  const [error, setError] = useState<string | null>(null)
  const [loading, setLoading] = useState(true)
  const [saving, setSaving] = useState(false)

  const loadEventos = async () => {
    setLoading(true)
    setError(null)
    try {
      const items = await getEventos()
      setEventos(items)
    } catch (e) {
      setError('No se pudieron cargar los eventos desde el backend.')
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    void loadEventos()
  }, [])

  const resetForm = () => {
    setSelectedEventoId(null)
    setNombre('')
    setFecha('')
    setLugar('')
    setMensaje(null)
    setError(null)
  }

  const handleGuardarEvento = async () => {
    setError(null)
    setMensaje(null)

    if (!nombre.trim() || !fecha || !lugar.trim()) {
      setError('Nombre, fecha y lugar son obligatorios.')
      return
    }

    setSaving(true)
    try {
      if (selectedEventoId) {
        await updateEvento(selectedEventoId, {
          nombre: nombre.trim(),
          fecha,
          lugar: lugar.trim(),
        })
        setMensaje('Evento actualizado correctamente.')
      } else {
        await createEvento({
          nombre: nombre.trim(),
          fecha,
          lugar: lugar.trim(),
        })
        setMensaje('Evento creado correctamente.')
      }
      await loadEventos()
      resetForm()
    } catch (e) {
      setError('Error al guardar el evento en el backend.')
    } finally {
      setSaving(false)
    }
  }

  const handleEditarEvento = (evento: EventoDto) => {
    setSelectedEventoId(evento.id)
    setNombre(evento.nombre ?? '')
    setFecha(evento.fecha.slice(0, 10))
    setLugar(evento.lugar ?? '')
    setMensaje(null)
    setError(null)
  }

  const handleEliminarEvento = async (id: number) => {
    if (!window.confirm('¿Deseas eliminar este evento?')) {
      return
    }

    setError(null)
    try {
      await deleteEvento(id)
      setMensaje('Evento eliminado correctamente.')
      await loadEventos()
      if (selectedEventoId === id) {
        resetForm()
      }
    } catch (e) {
      setError('No se pudo eliminar el evento en el backend.')
    }
  }

  const eventosTabla = useMemo(
    () =>
      eventos.map((evento) => ({
        ...evento,
        fechaFormato: evento.fecha.slice(0, 10),
      })),
    [eventos],
  )

  return (
    <div className="page-section">
      <div className="page-header">
        <div>
          <p className="eyebrow">Eventos</p>
          <h1>Gestión de eventos</h1>
          <p className="page-description">
            Crea, edita y elimina eventos conectados directamente con tu API.
          </p>
        </div>
      </div>

      <div className="message-row">
        {mensaje && <p className="message success">{mensaje}</p>}
        {error && <p className="message error">{error}</p>}
      </div>

      <div className="grid-layout">
        <section className="section-card">
          <h2>{selectedEventoId ? 'Editar evento' : 'Nuevo evento'}</h2>
          <div className="form-grid">
            <div className="field-group">
              <label>Nombre del evento</label>
              <input
                value={nombre}
                onChange={(e) => setNombre(e.target.value)}
                placeholder="Ej. Fiesta corporativa"
              />
            </div>
            <div className="field-group">
              <label>Fecha</label>
              <input
                type="date"
                value={fecha}
                onChange={(e) => setFecha(e.target.value)}
              />
            </div>
            <div className="field-group">
              <label>Lugar</label>
              <input
                value={lugar}
                onChange={(e) => setLugar(e.target.value)}
                placeholder="Ej. Hotel Jaragua"
              />
            </div>
          </div>
          <button
            type="button"
            className="btn-primary"
            onClick={handleGuardarEvento}
            disabled={saving}
          >
            {selectedEventoId ? 'Actualizar evento' : 'Crear evento'}
          </button>
        </section>

        <section className="section-card">
          <h2>Eventos existentes</h2>
          {loading ? (
            <p className="empty-state">Cargando eventos...</p>
          ) : eventosTabla.length === 0 ? (
            <p className="empty-state">No hay eventos registrados.</p>
          ) : (
            <div className="table-shell">
              <table>
                <thead>
                  <tr>
                    <th>Nombre</th>
                    <th>Fecha</th>
                    <th>Lugar</th>
                    <th>Acciones</th>
                  </tr>
                </thead>
                <tbody>
                  {eventosTabla.map((evento) => (
                    <tr key={evento.id}>
                      <td>{evento.nombre}</td>
                      <td>{evento.fechaFormato}</td>
                      <td>{evento.lugar}</td>
                      <td className="actions-cell">
                        <button
                          type="button"
                          className="link-button"
                          onClick={() => handleEditarEvento(evento)}
                        >
                          Editar
                        </button>
                        <button
                          type="button"
                          className="link-button danger"
                          onClick={() => void handleEliminarEvento(evento.id)}
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

export default EventosPage
