import { NavLink } from 'react-router-dom'

const navItems = [
  { to: '/', label: 'Dashboard' },
  { to: '/eventos', label: 'Eventos' },
  { to: '/invitados', label: 'Invitados' },
  { to: '/entradas', label: 'Entradas' },
  { to: '/validaciones', label: 'Validación' },
]

const Navbar = () => (
  <header className="navbar-shell">
    <div className="navbar-top">
      <div className="brand-center">
        <span className="brand-title">MEMORA WEDDINGS</span>
        <span className="brand-subtitle">Sistema de Gestión de Eventos</span>
      </div>
      <nav className="navbar-links" aria-label="Secciones del sitio">
        {navItems.map((item) => (
          <NavLink
            key={item.to}
            to={item.to}
            className={({ isActive }) =>
              `navbar-link${isActive ? ' navbar-link-active' : ''}`
            }
          >
            {item.label}
          </NavLink>
        ))}
      </nav>
    </div>
  </header>
)

export default Navbar
