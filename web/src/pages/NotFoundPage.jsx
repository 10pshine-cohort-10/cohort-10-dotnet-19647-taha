import { Link } from 'react-router'

export default function NotFoundPage() {
  return (
    <section className="auth">
      <h1>Page not found</h1>
      <Link to="/">Go to dashboard</Link>
    </section>
  )
}
