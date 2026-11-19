import { Link } from 'react-router'

export default function SignupPage() {
  return (
    <section className="auth">
      <h1>Sign up</h1>
      <p>Signup form goes here.</p>
      <Link to="/login">Already have an account? Log in</Link>
    </section>
  )
}
