import { Link } from 'react-router'

export default function TaskListPage() {
  return (
    <section>
      <h1>Tasks</h1>
      <Link to="/tasks/new">+ New task</Link>
      <p>Task list with filters goes here.</p>
    </section>
  )
}
