import { useEffect, useState } from 'react'
import type { EntradaDto, ValidacionDto } from '../types/api'
import {
  createValidacion,
  deleteValidacion,
  getValidaciones,
  updateValidacion,
} from '../services/validacionesService'
import { getEntradas } from '../services/entradasService'
import '../App.css'

const ValidacionesPage = () => {
  const [validaciones, setValidaciones] = useState<ValidacionDto[]>([])
  const [entradas, setEntradas] = useState<EntradaDto[]>([])
  const [selectedId, setSelectedId] = useState<number | null>(null)
  const [entradaId, setEntradaId] = useState<number | null>(null)
  const [fechaValidacion, setFechaValidacion] = useState(
    new Date().toISOString().slice(0, 16),
  )
  const [accesoPermitido, setAccesoPermitido] = useState(true)
  const [loading, setLoading] = useState(true)
  const [saving, setSaving] = useState(false)
  const [error, setError] = useState<string | null>(null)
  const [mensaje, setMensaje] = useState<string | null>(null)

  const loadData = async () => {
    setLoading(true)
    setError(null)
    try {
      const [validacionesData, entradasData] = await Promise.all([
        getValidaciones(),
        getEntradas(),
      ])
      setValidaciones(validacionesData)
      setEntradas(entradasData)
    } catch (e) {
      setError('No se pudieron cargar las validaciones o entradas.')
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    void loadData()
  }, [])

  const resetForm = () => {
    setSelectedId(null)
    setEntradaId(null)
    setFechaValidacion(new Date().toISOString().slice(0, 16))
    setAccesoPermitido(true)
    setMensaje(null)
    setError(null)
  }

  const handleGuardar = async () => {
    if (!entradaId) {
      setError('Seleccione una entrada para la validación.')
      setMensaje(null)
      return
    }

    setSaving(true)
    setError(null)
    try {
      const payload = {
        entradaId,
        fechaValidacion: new Date(fechaValidacion).toISOString(),
        accesoPermitido,
      }

      if (selectedId) {
        await updateValidacion(selectedId, payload)
        setMensaje('Validación actualizada correctamente.')
      } else {
        await createValidacion(payload)
        setMensaje('Validación registrada correctamente.')
      }

      await loadData()
      resetForm()
    } catch (e) {
      setError('Error al guardar la validación en el backend.')
    } finally {
      setSaving(false)
    }
  }

  const handleEditar = (item: ValidacionDto) => {
    setSelectedId(item.id)
    setEntradaId(item.entradaId)
    setFechaValidacion(new Date(item.fechaValidacion).toISOString().slice(0, 16))
    setAccesoPermitido(item.accesoPermitido)
    setMensaje(null)
    setError(null)
  }

  const handleEliminar = async (id: number) => {
    if (!window.confirm('¿Deseas eliminar esta validación?')) {
      return
    }

    setError(null)
    try {
      await deleteValidacion(id)
      setMensaje('Validación eliminada correctamente.')
      await loadData()
      if (selectedId === id) {
        resetForm()
      }
    } catch (e) {
      setError('No se pudo eliminar la validación en el backend.')
    }
  }

  const getEntryLabel = (id: number | null) =>
    entradas.find((item) => item.id === id)?.codigoQR ?? 'No disponible'

  return (
    <div className="page-section">
      <div className="page-header">
        <div>
          <p className="eyebrow">Validación</p>
          <h1>Registro de validaciones</h1>
          <p className="page-description">
            Crea y administra validaciones de entradas de invitados
          </p>
        </div>
      </div>

      <div className="message-row">
        {mensaje && <p className="message success">{mensaje}</p>}
        {error && <p className="message error">{error}</p>}
      </div>

      <div className="grid-layout">
        <section className="section-card">
          <h2>{selectedId ? 'Editar validación' : 'Nueva validación'}</h2>
          <div className="form-grid">
            <div className="field-group">
              <label>Entrada</label>
              <select value={entradaId ?? ''} onChange={(e) => setEntradaId(Number(e.target.value) || null)}>
                <option value="">Seleccione una entrada</option>
                {entradas.map((item) => (
                  <option key={item.id} value={item.id}>
                    {item.codigoQR}
                  </option>
                ))}
              </select>
            </div>
            <div className="field-group">
              <label>Fecha y hora</label>
              <input
                type="datetime-local"
                value={fechaValidacion}
                onChange={(e) => setFechaValidacion(e.target.value)}
              />
            </div>
            <div className="field-group">
              <label>
                <input
                  type="checkbox"
                  checked={accesoPermitido}
                  onChange={(e) => setAccesoPermitido(e.target.checked)}
                />
                Acceso permitido
              </label>
            </div>
          </div>
          <button
            type="button"
            className="btn-primary"
            onClick={handleGuardar}
            disabled={saving}
          >
            {selectedId ? 'Actualizar validación' : 'Registrar validación'}
          </button>
        </section>

        <section className="section-card">
          <h2>Validaciones registradas</h2>
          {loading ? (
            <p className="empty-state">Cargando validaciones...</p>
          ) : validaciones.length === 0 ? (
            <p className="empty-state">No hay validaciones registradas.</p>
          ) : (
            <div className="table-shell">
              <table>
                <thead>
                  <tr>
                    <th>Entrada</th>
                    <th>Fecha validación</th>
                    <th>Acceso</th>
                    <th>Acciones</th>
                  </tr>
                </thead>
                <tbody>
                  {validaciones.map((item) => (
                    <tr key={item.id}>
                      <td>{getEntryLabel(item.entradaId)}</td>
                      <td>{new Date(item.fechaValidacion).toLocaleString()}</td>
                      <td>{item.accesoPermitido ? 'Permitido' : 'Denegado'}</td>
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

export default ValidacionesPage
