import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import Login from './Pages//Login/Login';
import Register from './Pages/Login/Register';
import ForgotPassword from './Pages/Login/ForgotPassword';
import ResetPassword from './Pages/Login/ResetPassword';

function App() {
  return (
    <Router>
      <nav style={{ padding: '1rem', background: '#eee', marginBottom: '1rem' }}>
        <Link to="/login" style={{ marginRight: '1rem' }}>Login</Link>
        <Link to="/register" style={{ marginRight: '1rem' }}>Register</Link>
        <Link to="/forgot-password">Forgot Password</Link>
      </nav>

      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/forgot-password" element={<ForgotPassword />} />
        <Route path="/reset-password" element={<ResetPassword />} />
        <Route path="*" element={<Login />} />
      </Routes>
    </Router>
  );
}

export default App;
