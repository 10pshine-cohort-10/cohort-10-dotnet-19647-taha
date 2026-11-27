const statuses = ['Completed', 'In Progress', 'Pending']

export default function DashboardPage() {
  return (
    <section>
      <h1>Dashboard</h1>
      <div className="cards">
        {statuses.map((status) => (
          <div key={status} className="card">
            <span>{status}</span>
            <strong>–</strong>
          </div>
        ))}
      </div>
    </section>
  )
}
