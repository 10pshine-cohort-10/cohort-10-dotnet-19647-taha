import { Link } from 'react-router'

export default function LoginPage() {
  return (
    <section className="auth">
      <h1>Log in</h1>
      <p>Login form goes here.</p>
      <Link to="/signup">Create an account</Link>
    </section>
  )
}
