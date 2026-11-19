import { Route, Routes } from 'react-router'
import Layout from './components/Layout.jsx'
import DashboardPage from './pages/DashboardPage.jsx'
import LoginPage from './pages/LoginPage.jsx'
import NewTaskPage from './pages/NewTaskPage.jsx'
import NotFoundPage from './pages/NotFoundPage.jsx'
import ProfilePage from './pages/ProfilePage.jsx'
import SignupPage from './pages/SignupPage.jsx'
import TaskDetailPage from './pages/TaskDetailPage.jsx'
import TaskListPage from './pages/TaskListPage.jsx'

export default function App() {
  return (
    <Routes>
      <Route path="/login" element={<LoginPage />} />
      <Route path="/signup" element={<SignupPage />} />
      <Route element={<Layout />}>
        <Route index element={<DashboardPage />} />
        <Route path="tasks" element={<TaskListPage />} />
        <Route path="tasks/new" element={<NewTaskPage />} />
        <Route path="tasks/:id" element={<TaskDetailPage />} />
        <Route path="profile" element={<ProfilePage />} />
      </Route>
      <Route path="\*" element={<NotFoundPage />} />
    </Routes>
  )
}
