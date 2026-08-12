import { useEffect, useState } from 'react'
import type { InvitadoDto } from '../types/api'
import {
  createInvitado,
  deleteInvitado,
  getInvitados,
  updateInvitado,
} from '../services/invitadosService'
import '../App.css'

const InvitadosPage = () => {
  const [invitados, setInvitados] = useState<InvitadoDto[]>([])
  const [selectedId, setSelectedId] = useState<number | null>(null)
  const [nombre, setNombre] = useState('')
  const [cedula, setCedula] = useState('')
  const [correo, setCorreo] = useState('')
  const [loading, setLoading] = useState(true)
  const [saving, setSaving] = useState(false)
  const [error, setError] = useState<string | null>(null)
  const [mensaje, setMensaje] = useState<string | null>(null)

  const loadInvitados = async () => {
    setLoading(true)
    setError(null)
    try {
      const items = await getInvitados()
      setInvitados(items)
    } catch (e) {
      setError('No se pudieron cargar los invitados desde el backend.')
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    void loadInvitados()
  }, [])

  const resetForm = () => {
    setSelectedId(null)
    setNombre('')
    setCedula('')
    setCorreo('')
    setMensaje(null)
    setError(null)
  }

  const handleGuardar = async () => {
    if (!nombre.trim() || !cedula.trim() || !correo.trim()) {
      setError('Nombre, cédula y correo son obligatorios.')
      setMensaje(null)
      return
    }

    setSaving(true)
    setError(null)
    try {
      if (selectedId) {
        await updateInvitado(selectedId, {
          nombre: nombre.trim(),
          cedula: cedula.trim(),
          correo: correo.trim(),
        })
        setMensaje('Invitado actualizado correctamente.')
      } else {
        await createInvitado({
          nombre: nombre.trim(),
          cedula: cedula.trim(),
          correo: correo.trim(),
        })
        setMensaje('Invitado creado correctamente.')
      }
      await loadInvitados()
      resetForm()
    } catch (e) {
      setError('Error al guardar el invitado en el backend.')
    } finally {
      setSaving(false)
    }
  }

  const handleEditar = (item: InvitadoDto) => {
    setSelectedId(item.id)
    setNombre(item.nombre ?? '')
    setCedula(item.cedula ?? '')
    setCorreo(item.correo ?? '')
    setMensaje(null)
    setError(null)
  }

  const handleEliminar = async (id: number) => {
    if (!window.confirm('¿Deseas eliminar este invitado?')) {
      return
    }

    setError(null)
    try {
      await deleteInvitado(id)
      setMensaje('Invitado eliminado correctamente.')
      await loadInvitados()
      if (selectedId === id) {
        resetForm()
      }
    } catch (e) {
      setError('No se pudo eliminar el invitado en el backend.')
    }
  }

  return (
    <div className="page-section">
      <div className="page-header">
        <div>
          <p className="eyebrow">Invitados</p>
          <h1>Administrar invitados</h1>
          <p className="page-description">
            Registra invitados y actualiza sus datos directamente en el backend.
          </p>
        </div>
      </div>

      <div className="message-row">
        {mensaje && <p className="message success">{mensaje}</p>}
        {error && <p className="message error">{error}</p>}
      </div>

      <div className="grid-layout">
        <section className="section-card">
          <h2>{selectedId ? 'Editar invitado' : 'Nuevo invitado'}</h2>
          <div className="form-grid">
            <div className="field-group">
              <label>Nombre</label>
              <input
                value={nombre}
                onChange={(e) => setNombre(e.target.value)}
                placeholder="Nombre completo"
              />
            </div>
            <div className="field-group">
              <label>Cédula</label>
              <input
                value={cedula}
                onChange={(e) => setCedula(e.target.value)}
                placeholder="123456789"
              />
            </div>
            <div className="field-group">
              <label>Correo</label>
              <input
                type="email"
                value={correo}
                onChange={(e) => setCorreo(e.target.value)}
                placeholder="correo@ejemplo.com"
              />
            </div>
          </div>
          <button
            type="button"
            className="btn-primary"
            onClick={handleGuardar}
            disabled={saving}
          >
            {selectedId ? 'Actualizar invitado' : 'Crear invitado'}
          </button>
        </section>

        <section className="section-card">
          <h2>Lista de invitados</h2>
          {loading ? (
            <p className="empty-state">Cargando invitados...</p>
          ) : invitados.length === 0 ? (
            <p className="empty-state">Aún no hay invitados registrados.</p>
          ) : (
            <div className="table-shell">
              <table>
                <thead>
                  <tr>
                    <th>Nombre</th>
                    <th>Cédula</th>
                    <th>Correo</th>
                    <th>Acciones</th>
                  </tr>
                </thead>
                <tbody>
                  {invitados.map((item) => (
                    <tr key={item.id}>
                      <td>{item.nombre}</td>
                      <td>{item.cedula}</td>
                      <td>{item.correo}</td>
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

export default InvitadosPage
