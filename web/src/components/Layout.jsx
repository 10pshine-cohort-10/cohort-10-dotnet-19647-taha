import { NavLink, Outlet } from 'react-router'

const links = [
  { to: '/', label: 'Dashboard', end: true },
  { to: '/tasks', label: 'Tasks', end: true },
  { to: '/tasks/new', label: 'New Task' },
  { to: '/profile', label: 'Profile' },
]

export default function Layout() {
  return (
    <div>
      <header className="header">
        <strong>Task Manager</strong>
        <nav className="nav">
          {links.map(({ to, label, end }) => (
            <NavLink key={to} to={to} end={end}>
              {label}
            </NavLink>
          ))}
        </nav>
      </header>
      <main className="content">
        <Outlet />
      </main>
    </div>
  )
}