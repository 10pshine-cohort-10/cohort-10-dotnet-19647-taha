import { Link, useParams } from 'react-router'

export default function TaskDetailPage() {
  const { id } = useParams()

  return (
    <section>
      <h1>Task #{id}</h1>
      <p>Task details go here.</p>
      <Link to="/tasks">Back to tasks</Link>
    </section>
  )
}
