import { Routes, Route } from 'react-router-dom'
import Navbar from './components/Navbar'
import DashboardPage from './pages/DashboardPage'
import EventosPage from './pages/EventosPage'
import InvitadosPage from './pages/InvitadosPage'
import EntradasPage from './pages/EntradasPage'
import ValidacionesPage from './pages/ValidacionesPage'
import './App.css'

function App() {
  return (
    <div className="app-shell">
      <Navbar />
      <main className="layout-shell">
        <Routes>
          <Route path="/" element={<DashboardPage />} />
          <Route path="/eventos" element={<EventosPage />} />
          <Route path="/invitados" element={<InvitadosPage />} />
          <Route path="/entradas" element={<EntradasPage />} />
          <Route path="/validaciones" element={<ValidacionesPage />} />
        </Routes>
      </main>
    </div>
  )
}

export default App
